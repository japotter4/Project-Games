using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SnakeGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Use the grid class to create the background
        Grid backgroundGrid;
        //Create a snake object
        Snake snake;
        //Snake block texture
        Texture2D snakeTexture;
        //Background brick texture
        Texture2D bgBrick;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //Init the background grid
            backgroundGrid = new Grid(this, "backgroundFile.txt");
            //Init the background brick texture
            bgBrick = Content.Load<Texture2D>(@"Textures/GreyBlock");
            //Init the snake
            snake = new Snake(this, backgroundGrid);
            //Init the snake texture
            snakeTexture = Content.Load<Texture2D>(@"Textures/RedBlock");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent()
        {
            
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Update the snake
            snake.Update(Keyboard.GetState());

            base.Update(gameTime);
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            //Draw the background grid
            backgroundGrid.Draw(spriteBatch, bgBrick);
            //Draw the snake
            snake.DrawSnake(spriteBatch, snakeTexture);

            base.Draw(gameTime);
        }
    }
}
