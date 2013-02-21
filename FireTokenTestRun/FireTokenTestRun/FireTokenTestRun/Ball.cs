using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FireTokenTestRun
{
    public class Ball
    {
        //The ball needs a velocity to move. This represents how many pixels it
        //moves every frame
        Vector2 ballVelocity = Vector2.Zero;

        //We need the ball's location and size, which is represent by a Rectangle
        //object
        public Rectangle ball;
        private Texture2D texture;
        private Random rand;
        private Game1 game1;

        //delay the ability to reset the ball
        int timeTillUnpaused = 0;


        //Score variables
        SpriteFont font;
        int redScore = 0;
        int blueScore = 0;

        //We will need references to the paddles for collision detection, and
        //our Game1 contains these references, our ball should have the
        //reference to the Game1
        public Ball(Game1 g, String texture)
        {
            //set the game1 object
            game1 = g;

            //set the texture of the ball
            this.texture = game1.Content.Load<Texture2D>(texture);

            //we use a random object when randomly setting the balls direction
            rand = new Random();

            //the ball should start near at the center of the screen
            //use a size of 20x20
            ball = new Rectangle(
                game1.GraphicsDevice.Viewport.Bounds.Width / 2 - ball.Width / 2,
                game1.GraphicsDevice.Viewport.Bounds.Height / 2 - ball.Height / 2,
                15,
                15);

            reset();

            //Initialize the score
            //font = game1.Content.Load<SpriteFont>(@"ScoreFont");
        }

        //whenever the ball needs to be placed into the center, we should 
        //call this
        public void reset()
        {
            int speed = 5; // default velocity
            Random rand = new Random();

            //randomize the ball orientation
            switch (rand.Next(4))
            {
                case 0: ballVelocity.X = speed; ballVelocity.Y = speed; break;
                case 1: ballVelocity.X = -speed; ballVelocity.Y = speed; break;
                case 2: ballVelocity.X = speed; ballVelocity.Y = -speed; break;
                case 3: ballVelocity.X = -speed; ballVelocity.Y = -speed; break;
            }

            //Initialize the ball to the center of the screen
            ball.X = game1.GraphicsDevice.Viewport.Bounds.Width / 2 - ball.Width / 2;
            ball.Y = game1.GraphicsDevice.Viewport.Bounds.Height / 2 - ball.Height / 2;
        }

        //This checks against the edges of the screen
        public void Update()
        {
            if (ball.X <= 0 || ball.X + ball.Width >= game1.GraphicsDevice.Viewport.Bounds.Width)
            {
                ballVelocity.X *= -1;
            }
            if (ball.Y <= 0)
            {
                ballVelocity.Y *= -1;
            }
        }

        //this is called every frame, it checks the grid (bricks in this game)
        public void Update(Grid grid)
        {
            bool coll = false;
            ball.X += (int)ballVelocity.X;
            if (grid.hasCollided(ball) == true) // Check if collided with left or right of brick
            {
                ballVelocity.X *= -1;
                coll = true;
            }
            ball.Y += (int)ballVelocity.Y;
            if (grid.hasCollided(ball) == true && coll == false) // Check if collided with top or bottom of brick
            {
                ballVelocity.Y *= -1;
            }
        }

        //this is called every frame, it checks rectangles (paddles in this game)
        public void Update(Rectangle box)
        {
            if (ball.Intersects(box)) // player 1, left
            {
                ballVelocity.Y *= -1;
                //ball.Y += box.Height;
            }
            /*else if (ball.Intersects(box) && ball.X > game1.GraphicsDevice.Viewport.Bounds.Width / 2) // player 2, right
            {
                ballVelocity.X = -ballVelocity.X;
                ball.X = box.X - ball.Width;
            }*/

            //Score handling
            if (ball.Y + ball.Height > game1.GraphicsDevice.Viewport.Bounds.Height)
            {
                //blueScore++;
                reset();
            }
        }

        //draw is called every frame, after updating
        //we need a spriteBatch to draw our paddle onto, so pass in the reference
        public void Draw(SpriteBatch spriteBatch)
        {
            //drawing the paddle requires its texture, location, and size
            //we also need to add a color mask: Color.White has no effect on how the
            //texture is drawn, so we usually prefer this

            //draw the ball
            spriteBatch.Begin();
            spriteBatch.Draw(texture, ball, Color.White);
            spriteBatch.End();
            //draw the score
            /*spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spriteBatch.DrawString(
                font,
                blueScore.ToString() + " - " + redScore.ToString(),
                new Vector2(
                    game1.GraphicsDevice.Viewport.Bounds.Width / 2 - 35,
                    10.0f),
                Color.Yellow);
            spriteBatch.End();*/
        }
    }
}