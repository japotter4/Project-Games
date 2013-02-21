using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//these are imports that we need for some of our objects
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FireTokenTestRun
{
    public class Paddle
    {
        //make the bounds public so the ball can detect for collision
        public Rectangle bounds;

        //other variables should be private
        private Texture2D texture;
        private Keys leftKey;
        private Keys rightKey;

        //when we create the paddle, it should know what player it is for
        public Paddle(int player)
        {
            //Player one settings
            if (player == 1)
            {
                //paddle is on left side of screen, with a size of 20x100
                bounds = new Rectangle(10, 500, 100, 20);

                //paddle moves up when W is pressed, and down when S is pressed
                leftKey = Keys.A;
                rightKey = Keys.D;
            }
            else //Player two settings
            {
                //paddle is on right side of screen
                bounds = new Rectangle(770, 250, 20, 100);

                //paddle moves up when Up-arrow is pressed, and down when down-arrow is pressed
                leftKey = Keys.Up;
                rightKey = Keys.Down;
            }
        }

        public void Load(Texture2D textureA)
        {
            //we need a texture in order to draw the paddle
            texture = textureA;
        }

        //update is called every frame
        public void Update(Game1 game)
        {
            //get the most recent snapshot of the keyboard.
            //a KeyboardState object is basically a collection of states for each of
            //the keys. a key's state is either up or down
            KeyboardState keyboardState = Keyboard.GetState();

            //check if the paddles upKey is being pressed
            //move the paddle up as long as the paddle stays on screen
            if (keyboardState.IsKeyDown(leftKey) && bounds.X > 0)
            {
                bounds.X -= 5;
            }

            //check if the paddles upKey is being pressed
            //move the paddle down as long as the paddle stays on screen
            if (keyboardState.IsKeyDown(rightKey) && bounds.X + bounds.Width < game.GraphicsDevice.Viewport.Bounds.Width)
            {
                bounds.X += 5;
            }
        }

        //draw is called every frame, after updating
        //we need a spriteBatch to draw our paddle onto, so pass in the reference
        public void Draw(SpriteBatch spriteBatch)
        {
            //drawing the paddle requires its texture, location, and size
            //we also need to add a color mask: Color.White has no effect on how the
            //texture is drawn, so we usually prefer this
            spriteBatch.Begin();
            spriteBatch.Draw(texture, bounds, Color.White);
            spriteBatch.End();
        }
    }
}
