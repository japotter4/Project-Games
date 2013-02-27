using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SideScroller
{
    class GlobalClass
    {
        private static float screenWidth;
        private static float screenHeight;

        public static float ScreenWidth {
            get { return screenWidth; }
            set { screenWidth = value; }
        }

        public static float ScreenHeight {
            get { return screenHeight; }
            set { screenHeight = value; }
        }
    }
    
    /*
     * Include this in your initialize function  
            GlobalClass.ScreenWidth = graphics.GraphicsDevice.Viewport.Width;
            GlobalClass.ScreenHeight = graphics.GraphicsDevice.Viewport.Height;
     */

}
