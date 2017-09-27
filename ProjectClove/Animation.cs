using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class Animation
    {
        public string AnimationName { get; set; }
        public Rectangle Bounds { get; set; }
        public Frame[] Frames { get; set; }
        public Frame CurrentFrame { get; set; }
        public int CurrentIndex { get; set; }
        public int Tick { get; set; }
        public Animation()
        {

        }

        public void Update()
        {
            if (Frames != null)
            {
                if (CurrentFrame == null)
                {
                    CurrentIndex = 0;
                    CurrentFrame = Frames[CurrentIndex];
                    Tick = 0;
                }
                else
                {
                    if (Tick < CurrentFrame.Ticks)
                    {
                        Tick++;
                    }
                    else
                    {
                        if (CurrentIndex + 1 < Frames.Length)
                        {
                            CurrentIndex++;
                            CurrentFrame = Frames[CurrentIndex];
                            Tick = 0;
                        }
                        else
                        {
                            CurrentIndex = 0;
                            CurrentFrame = Frames[CurrentIndex];
                            Tick = 0;
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentFrame != null)
            {
                CurrentFrame.Draw(spriteBatch);
            }
        }
    }
}
