using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FireTokenTestRun
{
    public class FireBallBar
    {
        Texture2D image;
        Rectangle position;
        Rectangle imageSrc;
        MouseState ms;
        public int numOfBalls = 0;
        int imageWidth = 500;
        int imageHeight = 198;

        List<FireBall> fireballs;


        public FireBallBar(Game1 game, String image, Rectangle position, MouseState ms)
        {
            this.image = game.Content.Load<Texture2D>(image);
            imageSrc = new Rectangle(0, 0, imageWidth, imageHeight);
            this.position = position;
            this.ms = ms;

            fireballs = new List<FireBall>();
        }

        public void Update()
        {
            ms = Mouse.GetState();
            imageSrc = new Rectangle(0, imageHeight * numOfBalls, imageWidth, imageHeight);

            for (int i = 0; i < fireballs.Count; i++)
            {
                FireBall temp = fireballs[i];

                temp.Update();
            }
        }

        public void Draw(SpriteBatch s)
        {
            s.Begin();
            s.Draw(image, position, imageSrc, Color.White);
            for (int i = 0; i < fireballs.Count; i++)
            {
                FireBall temp = fireballs[i];
                s.Draw(temp.image, temp.position, Color.White);
            }
            s.End();
        }

        public void Fire(Game1 game, String image, paddle p)
        {
            //if (numOfBalls >= 0)
            //{
                fireballs.Add(new FireBall(game, image, p, ms.X, ms.Y));
                //numOfBalls--;
            //}
        }

    }

    public class FireBall
    {
        public Texture2D image;
        public Rectangle position;
        int Width = 15;
        int Height = 15;
        int Xvel;
        int Yvel;
        int maxMove = 6;
        int MouseX, MouseY;
        int curX, curY;
        int triangleWidth, triangleHeight, triangleHyp;

        public FireBall(Game1 game, String image, paddle Paddle, int MouseX, int MouseY)
        {
            //Get the image
            this.image = game.Content.Load<Texture2D>(image);

            this.MouseX = MouseX - this.image.Bounds.Width / 2;
            this.MouseY = MouseY - this.image.Bounds.Height / 2;
            curX = Paddle.bounds.X + Paddle.bounds.Width / 2 - this.image.Bounds.Width / 2;
            curY = Paddle.bounds.Y - this.image.Bounds.Height / 2;

            //calc current triangle width
            triangleWidth = MouseX - curX;
            //calc current triangle height
            triangleHeight = curY - MouseY;
            //calc current hypotenmoose
            triangleHyp = (int)Math.Sqrt(triangleWidth * triangleWidth + triangleHeight * triangleHeight);

            Update();

        }

        public void Update()
        {
            //Make sure to keep the velocity when it gets too close to the point
            if (triangleHyp > 15)
            {
                //calc current triangle width
                triangleWidth = MouseX - curX;
                //calc current triangle height
                triangleHeight = curY - MouseY;
                //calc current hypotenmoose
                triangleHyp = (int)Math.Sqrt(triangleWidth * triangleWidth + triangleHeight * triangleHeight);

            
                //calc the x vel
                Xvel = triangleWidth / (triangleHyp / maxMove);
                //calc the y vel
                Yvel = triangleHeight / (triangleHyp / maxMove);
            }

            //update the postion
            curX += (int)Xvel;
            curY -= (int)Yvel;

            position = new Rectangle(curX, curY, Width, Height);

            position.X  = curX;
            position.Y  = curY;
        }
    }
}
