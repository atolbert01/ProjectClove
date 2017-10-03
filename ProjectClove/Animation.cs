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
        public float Tick { get; set; }
        private Vector2 center { get; set; }
        public Animation()
        {

        }

        public void Update(GameTime gameTime)
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
                    float et = (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Tick += et * 60.0f;

                    if(Tick > CurrentFrame.Ticks)
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

        public void Draw(SpriteBatch spriteBatch, Vector2 loc)
        {
            if (CurrentFrame != null)
            {
                CurrentFrame.Draw(spriteBatch, loc);
            }
        }
    }
}
