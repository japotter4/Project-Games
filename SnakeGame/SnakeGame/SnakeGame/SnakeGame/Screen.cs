using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SnakeGame
{
    public class Screen
    {
        //Background Image
        public Texture2D background;
        //Selection Keys
        public Keys upKey = Keys.Up;
        public Keys downKey = Keys.Down;
        //Number of selectable links
        public int numOfLinks;
        //List that holds all links
        public List<Link> myLinks = new List<Link>();
        //Int referring to currently selected link
        public int selection;
        //Spritefont ish
        public SpriteFont font;
        //Positioning for the Links
        public float posInterval;
        

        public Screen(Game1 g, String bg, String f)
        {
            background = g.Content.Load<Texture2D>(bg);
            font = g.Content.Load<SpriteFont>(f);
            selection = 0;
            numOfLinks = 0;
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(downKey) && selection > 0)
            {
                selection--;
                System.Threading.Thread.Sleep(150);
            }

            if (keyboardState.IsKeyDown(upKey) && selection < numOfLinks-1)
            {
                selection++;
                System.Threading.Thread.Sleep(150);
            }

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                myLinks[selection].Switch();
            }

            //Going through all the links to reset them as selected or not
            for (int i = 0; i < numOfLinks; i++)
            {
                if (i == selection)
                {
                    myLinks[i].isSelected = true;
                }
                else
                {
                    myLinks[i].isSelected = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Background First
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0,0), Color.White);

            //Draw all links
            for (int i = 0; i < numOfLinks; i++)
            {
                if (myLinks[i].isSelected)
                {
                    spriteBatch.DrawString(font, myLinks[i].name, new Vector2(200, 400 - i * posInterval), Color.Red);
                }
                else
                {
                    spriteBatch.DrawString(font, myLinks[i].name, new Vector2(200, 400 - i * posInterval), Color.White);
                }
                
            }

            spriteBatch.End();
        }

        public void AddLink(String s, bool b)
        {
            myLinks.Add(new Link(s, b));
            numOfLinks++;

            posInterval = 400 / numOfLinks; /*NOTE TO SELF: get input so you know how big the screen will be*/
            myLinks[0].isSelected = true;
        }

    }

    public class Link
    {
        public bool isSelected;
        public String name;

        public bool functionality;

        public Link(String s, bool f)
        {
            name = s;
            functionality = f;
            isSelected = false;
        }

        public void Switch()
        {
            //functionality = functionality ? false : true;
            if (functionality == true)
            {
                functionality = false;
            }
            else
            {
                functionality = true;
            }
        }
    }
}
