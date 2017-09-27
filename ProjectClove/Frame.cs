using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class Frame
    {
        public int Ticks { get; set; }
        public Part[] Parts { get; set; }

        public Frame() { }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Parts != null)
            {
                foreach (Part part in Parts)
                {
                    part.Draw(spriteBatch);
                }
            }
        }
    }
}
