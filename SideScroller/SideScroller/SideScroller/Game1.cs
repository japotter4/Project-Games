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

namespace SideScroller
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Create a sprite
        Animation player;
        //Using fonts
        SpriteFont font;
        //Score holder
        int score;
        //Control to keys
        KeyboardState presentKey;
        KeyboardState pastKey;
        //Ship object
        Ship ship;
        //Viewing object
        Camera camera;
        //Add a background
        Texture2D backgroundTexture;
        Vector2 backgroundPosition;

        public enum GameState
        {
            MainMenu,
            LevelSelect,
            LevelOne,
        }
        GameState currentState = GameState.MainMenu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //Initialize the variable in GlobalClass
            GlobalClass.ScreenWidth = graphics.GraphicsDevice.Viewport.Width;
            GlobalClass.ScreenHeight = graphics.GraphicsDevice.Viewport.Height;

            //Initialize the player
            player = new Animation(Content.Load<Texture2D>(@"Textures/ExportSmallCharacter1"), new Vector2(0, 65), 65, 125);

            //Initialize the ship
            ship = new Ship(Content.Load<Texture2D>(@"Textures/Ship"), new Vector2(GlobalClass.ScreenWidth / 2, GlobalClass.ScreenHeight / 2));
            ship.BulletTexture(Content.Load<Texture2D>(@"Textures/Bullet"));

            //Initialize the camera
            camera = new Camera(GraphicsDevice.Viewport);

            //Initialize the background
            backgroundTexture = Content.Load<Texture2D>(@"Textures/StarBackground");
            backgroundPosition = new Vector2(0, 0);

            // Make sure the mouse is visible
            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>(@"Fonts/Font");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Get the keyboard state
            presentKey = Keyboard.GetState();

            //Update the player
            player.Update(gameTime);

            //Update the schip
            ship.Update(gameTime);

            //Update the score
            if (presentKey.IsKeyDown(Keys.Right) && pastKey.IsKeyUp(Keys.Right))
            {
                score += 1;
            }
            else if (presentKey.IsKeyDown(Keys.Left) && pastKey.IsKeyUp(Keys.Left))
            {
                score -= 1;
            }

            if (presentKey.IsKeyDown(Keys.Space) && pastKey.IsKeyUp(Keys.Space))
            {
                ship.Shoot();
            }

            //Update the level
            switch (currentState)
            {
                case GameState.MainMenu:
                    //Check what level the player wants
                    if (presentKey.IsKeyDown(Keys.Space))
                    {
                        currentState = GameState.LevelSelect;
                    }
                    break;
                case GameState.LevelSelect:
                    if (presentKey.IsKeyDown(Keys.D1))
                    {
                        currentState = GameState.LevelOne;
                    }
                    break;
                case GameState.LevelOne:
                    if (presentKey.IsKeyDown(Keys.Enter))
                    {
                        currentState = GameState.MainMenu;
                    }
                    break;
            }

            //Update the camera
            camera.Update(gameTime, ship);

            pastKey = presentKey;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (currentState)
            {
                case GameState.MainMenu:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;
                case GameState.LevelSelect:
                    GraphicsDevice.Clear(Color.Tomato);
                    break;
                case GameState.LevelOne:
                    GraphicsDevice.Clear(Color.Salmon);
                    break;
            }

            spriteBatch.Begin(SpriteSortMode.Deferred,
                              BlendState.AlphaBlend,
                              null, null, null, null,
                              camera.transform);
            spriteBatch.Draw(backgroundTexture, 
                             new Vector2(0, 0),  
                             Color.White);
            player.Draw(spriteBatch);
            ship.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(20, 10), Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
