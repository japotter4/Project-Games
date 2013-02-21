using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Mouse_Training
{
    class Circle
    {
        Texture2D texture;
        int radius;
        int posX;
        int posY;
        int level;

        public Circle(Game1 game, int radius, int posX, int posY, String texture)
        {
            level = 1;

            this.radius = radius;
            this.posX = posX;
            this.posY = posY;

            this.texture = game.Content.Load<Texture2D>(texture);
        }

        public void InitNewCircle(Game1 game)
        {
            Random random = new Random();

            level++;

            radius = (100 / level); 

            posX = random.Next(radius, game.GraphicsDevice.Viewport.Bounds.Width - radius);
            posY = random.Next(radius, game.GraphicsDevice.Viewport.Bounds.Height - radius);

        }

        public bool IsInBounds(MouseState ms)
        {
            if (Math.Sqrt((posX - ms.X) * (posX - ms.X) + (posY - ms.Y) * (posY - ms.Y)) <= radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Draw(SpriteBatch s)
        {
            s.Begin();
            s.Draw(texture, new Rectangle(posX - radius, posY - radius, 2 * radius, 2 * radius), Color.White);
            s.End();
        }
    }
}
