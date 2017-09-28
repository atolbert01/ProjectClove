using Microsoft.Xna.Framework;
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
        public Actor(Animation[] anims, Vector2 loc)
        {
            Animations = anims;
            Anim = Animations[1];
            Location = loc;
        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            
            // Testing collision checking.
            if (keyState.IsKeyDown(Keys.Left))
            {
                if (BoundingBox.X > 4) { Location += new Vector2(-4, 0); }
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                if (BoundingBox.X < 796 - BoundingBox.Width) { Location += new Vector2(4, 0); }
            }

            Anim.Update(Location);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Anim.Draw(spriteBatch, Location);
        }
    }
}
