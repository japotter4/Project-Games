using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Explorer
{
    // Pretty confusing stuff
    class Camera
    {
        // More stuff
        public Matrix transform;
        Viewport view;
        Vector2 center;

        // Pass this Viewport like "GraphicsDevice.Viewport"
        public Camera(Viewport newView) {
            view = newView;
        }

        // The Ship parameter should be changed to a general sprite which may involve changing the entire ship class into a class
        // but right now I'm not sure how it would be differentiated between which type of movement the sprite would get
        public void Update (GameTime gameTime, Ship ship) {
            // This is the top left corner of the view port I believe
            center = new Vector2(ship.position.X + (ship.rectangle.Width / 2) - GlobalClass.ScreenWidth / 2, 
                                 ship.position.Y + (ship.rectangle.Height / 2) - GlobalClass.ScreenHeight / 2);
            // No idea what this does
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * 
                Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }
    }

    /*
     * In order to display the camera the begin function should look like this
     * spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, {your camera object}.transform);
     * 
     */
}

