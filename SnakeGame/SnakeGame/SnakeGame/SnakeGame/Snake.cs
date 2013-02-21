using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SnakeGame
{
    class Snake
    {
        //Get a copy of the game 
        Game1 game;
        //Current direction
        char direction;
        //Array of the snake body
        int[] bodyRow;
        int[] bodyCol;
        //Track the length of the snake
        int snakeLength;
        int curLength;
        //Starting snake locastion
        int startRow;
        int startCol;
        //Current grid
        Grid grid;
        //Set the speed and init the ticks
        int speed;
        int currentTick;
        //Track if the snake is alive
        bool lost;
        //Is the powerup on the board
        bool powerUpEnabled;
        int powerUpRow;
        int powerUpCol;

        //Constructor
        public Snake(Game1 game, Grid grid)
        {
            //Get the game
            this.game = game;
            //Get the current grid
            this.grid = grid;
            //Set the starting direction
            direction = 'R';
            //Init the body size
            bodyRow = new int[grid.numCellsHigh * grid.numCellsLong];
            bodyCol = new int[grid.numCellsHigh * grid.numCellsLong];
            //Init the snake size
            snakeLength = 6;
            curLength = 1;

            for (int i = 0; i < bodyRow.Length; i++)
            {
                bodyRow[i] = 0;
                bodyCol[i] = 0;
            }

            //Find the body
            for (int i = 0; i < grid.numCellsHigh; i++)
            {
                for (int j = 0; j < grid.numCellsLong; j++)
                {
                    if (grid.charGrid[i, j] == 's')
                    {
                        bodyRow[0] = i;
                        bodyCol[0] = j;
                        startRow = i;
                        startCol = j;
                    }
                }
            }

            //Init the speed and timer
            speed = 5;
            currentTick = 0;
            //Init the state of the game
            lost = false;
            //Init the powerup
            EnablePowerups();
            powerUpEnabled = true;
        }

        //Reset the snake
        public void Reset()
        {
            snakeLength = 0;
        }

        //Update the position
        public void Update(KeyboardState keyboardState)
        {
            //Update the speed counter
            currentTick++;
            if (currentTick % speed == 0)
            {
                //Reset number of ticks
                if (currentTick >= 180)
                {
                    currentTick = 0;
                    //enable the powerup
                    if (!powerUpEnabled)
                    {
                        powerUpEnabled = true;
                        EnablePowerups();
                    }
                }
                //Update the current length by 1 if too short
                if (curLength < snakeLength)
                {
                    curLength++;
                }

                //Figure out what the user wants to do
                if (keyboardState.IsKeyDown(Keys.Right) && direction != 'L')
                {
                    direction = 'R';
                }
                else if (keyboardState.IsKeyDown(Keys.Left) && direction != 'R')
                {
                    direction = 'L';
                }
                else if (keyboardState.IsKeyDown(Keys.Up) && direction != 'D')
                {
                    direction = 'U';
                }
                else if (keyboardState.IsKeyDown(Keys.Down) && direction != 'U')
                {
                    direction = 'D';
                }

                //Move the snake depending on the current direction
                if (direction == 'R') // Right
                {
                    //Figure out what the next square holds
                    char next = grid.charGrid[bodyRow[0], bodyCol[0] + 1];
                    if (next == 'p')
                    {
                        powerUpEnabled = false;
                        snakeLength += 3;
                    }

                    //Move the snake
                    if (next == 'e' || next == 'p')
                    {
                        ModSnake("erase");
                        pushBack();
                        bodyRow[0] = bodyRow[1];
                        bodyCol[0] = bodyCol[1] + 1;
                        ModSnake("draw");
                    }
                    else
                    {
                        lost = true;
                    }
                }
                if (direction == 'L') // Left
                {
                    //Figure out what the next square holds
                    char next = grid.charGrid[bodyRow[0], bodyCol[0] - 1];
                    if (next == 'p')
                    {
                        powerUpEnabled = false;
                        snakeLength += 3;
                    }

                    //Move the snake
                    if (next == 'e' || next == 'p')
                    {
                        ModSnake("erase");
                        pushBack();
                        bodyRow[0] = bodyRow[1];
                        bodyCol[0] = bodyCol[1] - 1;
                        ModSnake("draw");
                    }
                    else
                    {
                        lost = true;
                    }
                }
                if (direction == 'U') // Up
                {
                    //Figure out what the next square holds
                    char next = grid.charGrid[bodyRow[0] - 1, bodyCol[0]];
                    if (next == 'p')
                    {
                        powerUpEnabled = false;
                        snakeLength += 3;
                    }

                    //Move the snake
                    if (next == 'e' || next == 'p')
                    {
                        ModSnake("erase");
                        pushBack();
                        bodyRow[0] = bodyRow[1] - 1;
                        bodyCol[0] = bodyCol[1];
                        ModSnake("draw");
                    }
                    else
                    {
                        lost = true;
                    }
                }
                if (direction == 'D') // Down
                {
                    //Figure out what the next square holds
                    char next = grid.charGrid[bodyRow[0] + 1, bodyCol[0]];
                    if (next == 'p')
                    {
                        powerUpEnabled = false;
                        snakeLength += 3;
                    }

                    //Move the snake
                    if (next == 'e' || next == 'p')
                    {
                        ModSnake("erase");
                        pushBack();
                        bodyRow[0] = bodyRow[1] + 1;
                        bodyCol[0] = bodyCol[1];
                        ModSnake("draw");
                    }
                    else
                    {
                        lost = true;
                    }
                }
            }
        }

        //Need a method to pushback the array
        private void pushBack()
        {
            for (int i = curLength - 1; i > 0; i--)
            {
                bodyRow[i] = bodyRow[i-1];
                bodyCol[i] = bodyCol[i-1];
            }
        }

        // Draw method
         public void DrawSnake(SpriteBatch s, Texture2D image)
        {
            for (int i = 0; i < curLength; i++)
            {
                s.Begin();
                s.Draw(image, new Rectangle(
                    (int)(bodyCol[i] * grid.cellWidth), 
                    (int)(bodyRow[i] * grid.cellHeight), 
                    (int)grid.cellWidth, 
                    (int)grid.cellHeight), 
                    Color.White);
                if (powerUpEnabled == true)
                {
                    s.Draw(image, new Rectangle(
                        (int)(powerUpCol * grid.cellWidth),
                        (int)(powerUpRow * grid.cellHeight),
                        (int)grid.cellWidth,
                        (int)grid.cellHeight),
                        Color.White);
                }
                s.End();
            }
        }

        //This is to put 'e' or an 's' in the location of the snake currently
        private void ModSnake(String mod)
        {
            if (mod.Equals("erase"))
            {
                for (int i = 0; i < curLength; i++)
                {
                    grid.charGrid[bodyRow[i], bodyCol[i]] = 'e';
                }
            }
            else if (mod.Equals("draw"))
            {
                for (int i = 0; i < curLength; i++)
                {
                    grid.charGrid[bodyRow[i], bodyCol[i]] = 's';
                }
            }
        }

        //Enable powerups
        public void EnablePowerups()
        {
            Random random = new Random();
            do
            {
                powerUpRow = (int)random.Next(0, grid.numCellsHigh);
                powerUpCol = (int)random.Next(0, grid.numCellsLong);
            } while (grid.charGrid[powerUpRow, powerUpCol] == 'b' || grid.charGrid[powerUpRow, powerUpCol] == 's');

            grid.charGrid[powerUpRow, powerUpCol] = 'p';
        }

    }
}
