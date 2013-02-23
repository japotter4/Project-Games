using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeGame
{
    public class Grid
    {
        Game1 g;
        public int gridHeight;
        public int gridWidth;
        public int numCellsHigh;
        public int numCellsLong;
        public double cellWidth;
        public double cellHeight;
        public bool[,] grid;
        public char[,] charGrid;
        public List<List<Brick> >grid1;
        StreamReader streamReader;

        public Grid() { }

        public Grid(Game1 g, String s)
        {
            //Get input from the imported file
            streamReader = new StreamReader(s);

            //Allow access to the Game1 object for screen sizes and ContentPipline
            this.g = g;

            //Get the size of the screen
            gridHeight = g.GraphicsDevice.Viewport.Bounds.Height;
            gridWidth = g.GraphicsDevice.Viewport.Bounds.Width;

            //Calc the number of cells long and wide
            numCellsLong = int.Parse(streamReader.ReadLine());
            numCellsHigh = int.Parse(streamReader.ReadLine());

            //Calc the width and height of each cell
            cellWidth = (double)gridWidth / numCellsLong;
            cellHeight = (double)gridHeight / numCellsHigh;

            //Initialze the grid array
            grid = new bool[numCellsHigh, numCellsLong];
            charGrid = new char[numCellsHigh, numCellsLong];

            //Initialize the grid
            initGrid();
        }

        public Grid(Game1 g, int numCellsHigh, int numCellsLong)
        {
            //Allow access to the Game1 object for screen sizes and ContentPipline
            this.g = g;

            //Get the size of the screen
            gridHeight = g.GraphicsDevice.Viewport.Bounds.Height;
            gridWidth = g.GraphicsDevice.Viewport.Bounds.Width;

            //Store te number of cells wide and high
            this.numCellsHigh = numCellsHigh;
            this.numCellsLong = numCellsLong;

            //Calc the width and height of each cell
            cellWidth = (double)gridWidth / numCellsLong;
            cellHeight = (double)gridHeight / numCellsHigh;

            //Initialze the grid array
            grid = new bool[numCellsHigh, numCellsLong];
            charGrid = new char[numCellsHigh, numCellsLong];
            
            //Initialze the grid
            initGrid();
        }

        public void initGrid()
        {
            for (int i = 0; i < numCellsHigh; i++)
            {
                for (int j = 0; j < numCellsLong; j++)
                {
                    char c;
                    do
                    {
                        c = (char)streamReader.Read();
                    } while (c.Equals('\n') || c.Equals('\r'));

                    charGrid[i, j] = c;

                    if (c == 'b')
                    {
                        grid[i, j] = true; 
                    }
                    else if(c == 'e')
                    {
                        grid[i, j] = false;
                    }
                    
                }
            }
        }

        public void Draw(SpriteBatch s, Texture2D image)
        {
            s.Begin();
            for (int i = 0; i < numCellsHigh; i++)
            {
                for (int j = 0; j < numCellsLong; j++)
                {
                    if (charGrid[i, j] == 'b')
                    {
                        //s.Begin();
                        s.Draw(image, new Rectangle((int)(j*cellWidth), (int)(i*cellHeight), (int)cellWidth, (int)cellHeight), Color.White);
                        //s.End();
                    }
                }
            }
            s.End();
        }

        public bool hasCollided(Rectangle ball) 
        {
            bool collided = false;

            if (grid[getRowWide(ball.X), getRowHigh(ball.Y)] == true) //top left corner
            {
                grid[getRowWide(ball.X), getRowHigh(ball.Y)] = false;
                collided = true;
            }
            else if (grid[getRowWide(ball.X + ball.Width), getRowHigh(ball.Y)] == true) // top right corner
            {
                grid[getRowWide(ball.X + ball.Width), getRowHigh(ball.Y)] = false;
                collided = true;
            }
            else if (grid[getRowWide(ball.X), getRowHigh(ball.Y + ball.Height)] == true) // bottom left corner
            {
                grid[getRowWide(ball.X), getRowHigh(ball.Y + ball.Height)] = false;
                collided = true;
            }
            else if (grid[getRowWide(ball.X + ball.Width), getRowHigh(ball.Y + ball.Height)] == true) // bottom right corner
            {
                grid[getRowWide(ball.X + ball.Width), getRowHigh(ball.Y + ball.Height)] = false;
                collided = true;
            }

            return collided;
        }

        private int getRowWide(int n)
        {
            int cell = (int)(n / cellWidth);
            if (cell >= numCellsLong)
            {
                cell = numCellsLong - 1;
            }
            else if (cell < 0)
            {
                cell = 0;
            }
            return cell;
        }

        private int getRowHigh(int n)
        {
            int cell = (int)(n / cellHeight);
            if (cell >= numCellsHigh)
            {
                cell = numCellsHigh - 1;
            }
            else if (cell < 0)
            {
                cell = 0;
            }
            return cell;
        }

    }
}
