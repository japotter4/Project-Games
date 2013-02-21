using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Mouse_Training
{
    class Cursor
    {
        Texture2D image;
        int posX;
        int posY;

        public Cursor(Game1 game, MouseState ms, String image)
        {
            posX = ms.X;
            posY = ms.Y;

            this.image = game.Content.Load<Texture2D>(image);
        }

        public void Update(MouseState ms)
        {
            posX = ms.X;
            posY = ms.Y;
        }

        public void Draw(SpriteBatch s)
        {
            s.Begin();
            s.Draw(image, new Rectangle(posX - 2, posY - 2, 5, 5), Color.White);
            s.End();
        }
    }
}
