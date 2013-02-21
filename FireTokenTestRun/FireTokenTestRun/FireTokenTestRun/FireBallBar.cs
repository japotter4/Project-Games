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
        int numOfBalls;
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

        public void Fire(Game1 game, String image, Paddle p)
        {
            fireballs.Add(new FireBall(game, image, p, ms.X - p.bounds.Width / 2, ms.Y - p.bounds.Height));

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

        public FireBall(Game1 game, String image, Paddle p, int Xvel, int Yvel)
        {
            this.image = game.Content.Load<Texture2D>(image);
            position = new Rectangle(p.bounds.Width / 2 - Width / 2, p.bounds.Height - Height, Width, Height);
            this.Xvel = Xvel;
            this.Yvel = Yvel;
        }

        public void Update()
        {
            position.X += Xvel;
            position.Y += Yvel;
        }
    }
}
