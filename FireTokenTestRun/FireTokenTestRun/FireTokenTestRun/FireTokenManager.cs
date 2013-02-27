using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FireTokenTestRun
{
    class FireTokenManager
    {
        List<FireToken> fireTokens;
        Game1 game;
        Random rand;

        public FireTokenManager(Game1 game)
        {
            this.game = game;
            rand = new Random();

            fireTokens = new List<FireToken>();
        }

        public void Update(paddle p, FireBallBar fbb)
        {
            for (int i = 0; i < fireTokens.Count; i++)
            {
                FireToken temp = fireTokens[i];

                temp.Update(p, fbb);
            }
        }

        public void Draw(SpriteBatch s)
        {
            for (int i = 0; i < fireTokens.Count; i++)
            {
                FireToken temp = fireTokens[i];

                temp.Draw(s);
            }
        }

        public void Drop(String image, int x, int y, bool isUp)
        {
            if (rand.Next(0, 5) == 0)
            {
                fireTokens.Add(new FireToken(game, image, x, y, isUp));
            }
        }
    }

    public class FireToken
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
