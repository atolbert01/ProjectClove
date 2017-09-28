﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectClove
{
    class Actor
    {
        public Animation[] Animations { get; set; }
        public Animation Anim { get; set; }
        public Vector2 Location { get; set; }
        public Rectangle BoundingBox
        {
            get{ return new Rectangle((int)Location.X - Anim.Bounds.Width / 2, (int)Location.Y - Anim.Bounds.Height / 2, Anim.Bounds.Width, Anim.Bounds.Height);}
        }
        private KeyboardState prevKeyState;
        public Actor(Animation[] anims, Vector2 loc)
        {
            Animations = anims;
            Anim = Animations[0];
            Location = loc;
        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();

            // Testing collision checking.
            if (keyState.IsKeyDown(Keys.Left))
            {
                if (BoundingBox.X > 6)
                {
                    Location += new Vector2(-6, 0);
                    Anim = Animations[3];
                }
                else
                {
                    Anim = Animations[2];
                }
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                if (BoundingBox.X < 794 - BoundingBox.Width)
                {
                    Location += new Vector2(6, 0);
                    Anim = Animations[1];
                }
                else
                {
                    Anim = Animations[0];
                }
            }
            else if (keyState.IsKeyUp(Keys.Right) && keyState.IsKeyUp(Keys.Left))
            {
                if (prevKeyState.IsKeyDown(Keys.Right))
                {
                    Anim = Animations[0];
                }
                else if (prevKeyState.IsKeyDown(Keys.Left))
                {
                    Anim = Animations[2];
                }
            }

            Anim.Update(Location);
            prevKeyState = keyState;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Anim.Draw(spriteBatch, Location);
        }
    }
}