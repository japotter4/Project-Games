using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeGame
{
    class Brick
    {
        //The type of brick
        String type;
        //Health
        int health;
        //Deminsions of the rectangle
        Rectangle brickRect;

        //Constructor
        public Brick(int x, int y, int width, int height, String type)
        {
            this.type = type;
            brickRect = new Rectangle(x, y, width, height);
            InitBrick();
        }

        //List of types
        private void InitBrick()
        {
            if (type == "Brick")
            {
                health = 1;
            }
        }

        //Check for collision
        public void Update()
        {

        }

        //return the type
        public void getType()
        {
            return type;
        }
    }
}
