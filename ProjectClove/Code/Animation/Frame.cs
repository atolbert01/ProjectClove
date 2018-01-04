using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class Frame
    {
        public int Ticks { get; set; }
        public Sprite[] Parts { get; set; }

        public Frame() { }

        public void Draw(SpriteBatch spriteBatch, Vector2 loc)
        {
            if (Parts != null)
            {
                foreach (Sprite part in Parts)
                {
                    part.Draw(spriteBatch, loc);
                }
            }
        }
    }
}
