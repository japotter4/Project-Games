using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FireTokenTestRun
{
    class FireToken
    {
        Texture2D image;
        Rectangle position;
        const int speed = 5;
        int velocity;

        public FireToken(Game1 game, String image, int x, int y, bool isUp)
        {
            this.image = game.Content.Load<Texture2D>(image);
            position = new Rectangle(x, y, 20, 20);

            if (isUp)
            {
                velocity = -1 * speed;
            }
            else
            {
                velocity = speed;
            }
        }

        public void Update(paddle p, FireBallBar fbb)
        {
            position.Y += velocity;
            if (position.Intersects(p.bounds))
            {
                fbb.numOfBalls++;
            }
        }

        public void Draw(SpriteBatch s)
        {
            s.Begin();
            s.Draw(image, position, Color.White);
            s.End();
        }
    }
}
