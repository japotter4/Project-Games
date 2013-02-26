using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DickingAround
{
    class Grid
    {
        int gridHeight;
        int gridWidth;
        int cellHeight;
        int cellWidth;
        int numCellsHigh;
        int numCellsWide;
        StreamReader sr;
        String strLayout;
        Block[,] grid;

        public Grid(Game1 game, String layoutFile, int gridHeight, int gridWidth)
        {
            this.gridHeight = gridHeight;
            this.gridWidth = gridWidth;

            //Load out the text file into a string
            sr = new StreamReader(layoutFile);
            strLayout = sr.ReadToEnd();
            sr.Close();

            //Get number of columns
            sr = new StreamReader(layoutFile);
            numCellsWide = sr.ReadLine().Length;
            sr.Close();

            cellWidth = this.gridWidth / numCellsWide;
            
            //Get number of rows
            numCellsHigh = strLayout.Length / numCellsWide;
            cellHeight = this.gridHeight / numCellsHigh;

            //Create your grid array
            grid = new Block[numCellsHigh, numCellsWide];

            //Initialize your grid
            this.Init(game);
        }

        private void Init(Game1 game)
        {
            int count = 0; 

            for (int i = 0; i < numCellsHigh; i++)
            {
                for (int j = 0; j < numCellsWide; j++)
                {
                    while(strLayout[count] == '\r' || strLayout[count] == '\n')
                    {
                        count++;
                    }
                    switch (strLayout[count])
                    {
                        //Enter your block symbols here 
                        case '-':
                            grid[i, j] = new Stone(game, this, "Stone", j, i);
                            count++;
                            break;
                        default:
                            grid[i, j] = null;
                            count++;
                            break;
                    }
                }
            }
        }

        public void Draw(SpriteBatch s)
        {
            for (int i = 0; i < numCellsHigh; i++)
            {
                for (int j = 0; j < numCellsWide; j++)
                {
                    if (grid[i, j] == null)
                    {
                        continue;
                    }
                    grid[i, j].Draw(s);
                }
            }
        }

        public void Update()
        {
            for (int i = 0; i < numCellsHigh; i++)
            {
                for (int j = 0; j < numCellsWide; j++)
                {
                    if (grid[i, j] == null)
                    {
                        continue;
                    }
                    grid[i, j].Update();
                }
            }
        }

        public Rectangle GetPosition(int x, int y)
        {
            return new Rectangle(cellWidth * x, cellHeight * y, cellWidth, cellHeight);
        }
    }
}
