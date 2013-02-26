using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DickingAround
{
    class Block
    {
        Texture2D texture;
        Rectangle position;

        public Block(Game1 game, Grid grid, String image, int x, int y)
        {
            texture = game.Content.Load<Texture2D>(image);
            position = grid.GetPosition(x, y);
        }

        public void Draw(SpriteBatch s)
        {
            s.Begin();
            s.Draw(texture, position, Color.White);
            s.End();
        }

        public virtual void Update()
        {
            //Collisions relevant to block
        }
    }
}
