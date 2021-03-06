﻿using System;
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

namespace SideScroller {
    /// <summary>
    /// This is a Sprite class that implements a 2D side scrolling sprite, there is no 4 directional capabilities
    /// </summary>
    class Animation {
//#region "First attempt"
//        //Sprite texture
//        private Texture2D sprite;
//        //Width and height of each picture
//        private int spriteHeight, spriteWidth;
//        //Number of sprite images
//        private int numSprites;
//        //Remember the current direction
//        //F = forwards
//        //L = left
//        //R = right
//        private char curDirection = 'F';
//        //Know which image you currently on
//        private int curImage = 0;
//        //Make the rectangle over the sprite
//        private Rectangle srcImage;
//        //Make the rectangle that the sprite is currently in
//        private Rectangle curPos;
//        //Speed of character
//        private int speed = 0;
//        //Make sure it doesnt fly through the pictures
//        private int ticksUntilNextUpdate = 2;
//        private int curTick = 0;

//        //Constructor
//        public Sprite(Texture2D sprite, int width, int height, int num)
//        {
//            //Assign variables
//            this.sprite = sprite;
//            spriteHeight = height;
//            spriteWidth = width;
//            numSprites = num;
//            curPos = new Rectangle(0, GraphicsDeviceManager.DefaultBackBufferHeight - height, width, height);
//        }

//        public void Update()
//        {
//            curTick++;
//            if (curTick > ticksUntilNextUpdate)
//            {
//                //reset timer
//                curTick = 0;
//                if ((Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))) // travelling left
//                {
//                    curPos.X -= speed;
//                    if (curDirection == 'L')
//                    {
//                        curImage++;
//                    }
//                    else
//                    {
//                        curDirection = 'L';
//                        curImage = 0;
//                    }
//                }
//                else if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D) && curDirection == 'R') // travelling right
//                {
//                    curPos.X += speed;
//                    if (curDirection == 'R')
//                    {
//                        curImage++;
//                    }
//                    else
//                    {
//                        curDirection = 'R';
//                        curImage = 0;
//                    }
//                }
//                else // no button are pressed, face forwards
//                {
//                    curDirection = 'F';
//                    curImage = 0;
//                }
//            }
            
//        }

//        //Draw the current image to the screen
//        public void Draw(SpriteBatch s)
//        {
//            s.Begin();
//            s.Draw(sprite, curPos, NextImage(), Color.White);
//            s.End();
//        }

//        private Rectangle NextImage()
//        {
//            //Loop the sprite index
//            if (curImage >= numSprites)
//            {
//                curImage = 0;
//            }

//            //If facing forwards
//            if(curDirection == 'F')
//                srcImage = new Rectangle(0, 0, spriteWidth, spriteHeight);
//            //If facing right
//            if(curDirection == 'R')
//                srcImage = new Rectangle(curImage * spriteWidth, spriteHeight, spriteWidth, spriteHeight);
//            //if facing left
//            if(curDirection == 'L')
//                srcImage = new Rectangle(curImage * spriteWidth, spriteHeight * 2, spriteWidth, spriteHeight);

//            //Return the source image
//            return srcImage;
//        }

//        //set the speed
//        public void setSpeed(int n)
//        {
//            speed = n;
//        }

//        //set the refresh rate
//        public void setRefreshRate(int n)
//        {
//            ticksUntilNextUpdate = n;
//        }
        //#endregion

#region "Second attempt"
        Texture2D texture;
        Rectangle rectangle;
        Vector2 position;
        Vector2 origin;
        Vector2 velocity;

        int currentFrame;
        int frameHeight;
        int frameWidth;

        float timer;
        float interval = 75;

        public Animation()
        {

        }

        public Animation(Texture2D newTexture, Vector2 newPostion, int newFrameWidth, int newFrameHeight) {
            texture = newTexture;
            position = newPostion;
            frameHeight = newFrameHeight;
            frameWidth = newFrameWidth;
            currentFrame = 0;
        }

        public void Update(GameTime gameTime) {
            rectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            position = position + velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                AnimateRight(gameTime);
                velocity.X = 3;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                AnimateLeft(gameTime);
                velocity.X = -3;
            }
            else {
                velocity.X = 0;
            }
        }

        private void AnimateRight(GameTime gameTime) {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval) {
                currentFrame++;
                timer = 0;
                if(currentFrame > 3) {
                    currentFrame = 0;
                }
            }
        }

        private void AnimateLeft(GameTime gameTime) {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval) {
                currentFrame++;
                timer = 0;
                if (currentFrame > 7 || currentFrame < 4) {
                    currentFrame = 4;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }


#endregion


    }
}
