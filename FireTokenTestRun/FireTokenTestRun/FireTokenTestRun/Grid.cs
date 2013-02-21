using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace FireTokenTestRun
{
    public class Grid
    {
        int gridHeight;
        int gridWidth;
        int numCellsHigh;
        int numCellsLong;
        double cellWidth;
        double cellHeight;
        bool[,] grid;

        public Grid(Rectangle screen, int numCellsLong, int numCellsHigh)
        {
            gridHeight = screen.Height;
            gridWidth = screen.Width;
            this.numCellsHigh = numCellsHigh;
            this.numCellsLong = numCellsLong;
            cellWidth = (double)gridWidth / numCellsLong;
            cellHeight = (double)gridHeight / numCellsHigh;
            grid = new bool[numCellsLong, numCellsHigh];
            initGrid();
        }

        public void initGrid()
        {
            for (int i = 5; i < 15; i++)
            {
                for (int j = 5; j < 10; j++)
                {
                    grid[i, j] = true;
                }
            }
        }

        public void Draw(SpriteBatch s, Texture2D image)
        {
            for (int i = 0; i < numCellsLong; i++)
            {
                for (int j = 0; j < numCellsHigh; j++)
                {
                    if (grid[i, j] == true)
                    {
                        s.Begin();
                        s.Draw(image, new Rectangle((int)(i * cellWidth), (int)(j * cellHeight), (int)cellWidth, (int)cellHeight), Color.White);
                        s.End();
                    }
                }
            }
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