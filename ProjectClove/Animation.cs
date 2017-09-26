using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class Animation
    {
        public string AnimationName { get; set; }
        public Rectangle Bounds { get; set; }
        public Frame[] Frames { get; set; }
        public Animation()
        {
        }
    }
}
