using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SideScroller
{
    class Bullets
    {
        public Texture2D texture;

        public Vector2 position;
        public Vector2 velocity;
        public Vector2 origin;

        public bool isVisible;

        float rotationAngle;

        public Bullets() {

        }

        public Bullets(Texture2D newTexture, float rotationAngle) {
            texture = newTexture;
            isVisible = false;
            this.rotationAngle = rotationAngle;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, null, Color.White, rotationAngle, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
