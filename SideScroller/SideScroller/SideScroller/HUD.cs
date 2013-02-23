using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SideScroller
{
    class HUD
    {
        //Create a texture
        Texture2D mainTexture;
        
        
        public HUD()
        {

        }

        public void Draw(SpriteBatch s)
        {
            s.Begin();
            s.Draw(mainTexture, new Rectangle(0, 0, GraphicsDeviceManager.DefaultBackBufferHeight, GraphicsDeviceManager.DefaultBackBufferWidth), Color.White);
            s.End();
        }

        public void Update()
        {

        }

    }
}
