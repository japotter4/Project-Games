using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SideScroller {
    class Ship {
        // Source texture for the sprite
        private Texture2D texture;
        // The rectangle of the sprite
        public Rectangle rectangle;
        // The middle of the sprite. The rotation point
        private Vector2 origin;
        // The current position of the sprite
        public Vector2 position;

        // The velocity of the sprite
        private Vector2 velocity;
        // This is how fast the sprite moves in one direction per call
        private const float tangentialVelocity = 5f;
        // This is how fast the sprite will slow down
        private float friction = 0.1f;
        // This is the rotation angle measured in radians
        private float rotationAngle;

        // Constructor that accepts the source texture and initial position
        public Ship(Texture2D newTexture, Vector2 newPosition) {
            texture = newTexture;
            position = newPosition;
            rotationAngle = 0;
            // The origin never changes so it's declared in the constructor
            origin = new Vector2(texture.Width / 2, texture.Height/ 2);
        }

        // Called everytime sprite needs to be updated 
        public void Update(GameTime gameTime) {
            // Update the rectangle
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            // Update the position based on the velocity values
            position = velocity + position;

            // Get the state of the keyboard
            KeyboardState keyboard = Keyboard.GetState();

            // Check which keys are down. Using WASD for movement
            if (keyboard.IsKeyDown(Keys.D)) { // Right movement
		        rotationAngle += 0.1f;
	        }
            if (keyboard.IsKeyDown(Keys.A)) { // Left movement
		        rotationAngle -= 0.1f;
            }
            if (keyboard.IsKeyDown(Keys.W)) { // Forward movement
                velocity.X = (float)Math.Cos(rotationAngle) * tangentialVelocity;
                velocity.Y = (float)Math.Sin(rotationAngle) * tangentialVelocity;
            }
            else if(velocity != Vector2.Zero) { // Slow down the sprite when forward isn't being pressed
                velocity -= friction * velocity;
            }
        }

        // Simply draw the sprite to the screen. Draw.Begin() must be called first outside of method.
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, null, Color.White, rotationAngle, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
