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

//Simply refacter and rename solution
namespace Space_Explorer {
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        // Background stuff
        Texture2D backgroundTexture;
        Vector2 backgroundPosition;

        // Ship stuff
        Ship mainShip;
        Texture2D mainShipTexture;
        Vector2 mainShipPosition;

        // Bullet stuff
        Texture2D bulletTexture;

        // Camera stuff
        Camera mainCamera;

        // Prevent multiple key presses
        KeyboardState presentKey;
        KeyboardState pastKey;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load textures
            backgroundTexture = Content.Load<Texture2D>(@"Textures/StarBackground");
            mainShipTexture = Content.Load<Texture2D>(@"Textures/Ship");
            bulletTexture = Content.Load<Texture2D>(@"Textures/Bullet");

            // Init screen size
            GlobalClass.ScreenWidth = GraphicsDevice.Viewport.Bounds.Width;
            GlobalClass.ScreenHeight = GraphicsDevice.Viewport.Bounds.Height;

            // Init starting positions
            mainShipPosition = new Vector2(GlobalClass.ScreenWidth / 2, GlobalClass.ScreenHeight / 2);
            backgroundPosition = new Vector2(0, 0);

            // Init the objects
            mainShip = new Ship(mainShipTexture, mainShipPosition);
            mainShip.SetBulletTexture(bulletTexture);
            mainCamera = new Camera(GraphicsDevice.Viewport);

            // Set mouse to being visible
            IsMouseVisible = true;
        }

        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) this.Exit();

            // Get the keyboard state
            presentKey = Keyboard.GetState();

            // Update the ship
            mainShip.Update(gameTime);
            if (presentKey.IsKeyDown(Keys.Space) && pastKey.IsKeyUp(Keys.Space))
            {
                mainShip.Shoot();
            }

            // Update the camera
            mainCamera.Update(gameTime, mainShip);

            // Store the past keyboard state
            pastKey = presentKey; 

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, mainCamera.transform);
                //Draw the background first
                spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);

                //Draw the ship and it's bullets
                mainShip.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void UnloadContent() {

        }

    }
}
