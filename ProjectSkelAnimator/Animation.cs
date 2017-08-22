using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectSkelAnimator
{
    /// <summary>
    /// The Animation class is functionally similar to the Frame class, but is mainly just a data structure for storing our animation frames. 
    /// Hierarchy is only important in that it controls the playback order.
    /// </summary>
    class Animation
    {
        public Part[] Parts { get; set; }
        public Frame[] Frames { get; set; }
        public Frame CurrentFrame;
        public int CurrentIndex = 0;
        
        public Animation()
        {
            Frames = new Frame[0];
            Parts = new Part[0];
        }

        public void Update(Cursor cursor)
        {
            //if (Frames.Length > 0)
            //{
            //    CurrentFrame = Frames[CurrentIndex];
            //}
        }

        public void Add(Frame newFrame)
        {
            int newSize = Frames.Length + 1;
            Frame[] resizedFrames = new Frame[newSize];

            // Add the existing frames to the new array.
            for (int i = 0; i < Frames.Length; i++)
            {
                resizedFrames[i] = Frames[i];
            }
            // Finally, add the new frame to the new array.
            resizedFrames[newSize - 1] = newFrame;
            CurrentIndex = newSize - 1;
            Frames = resizedFrames;
            CurrentFrame = Frames[CurrentIndex];
        }

        /// <summary>
        /// Inserts a new frame at the CurrentIndex + 1. The array is recreated up to and including the CurrentIndex, the new frame is inserted, then the rest of the array is added.
        /// </summary>
        public void Insert(Frame newFrame)
        {
            int newSize = Frames.Length + 1;
            Frame[] resizedFrames = new Frame[newSize];

            int j = 0;
            for (int i = 0; i < resizedFrames.Length; i++)
            {
                if (i != CurrentIndex + 1)
                {
                    resizedFrames[i] = Frames[j];
                    j++;
                }
                else
                {
                    resizedFrames[i] = newFrame;
                }
            }

            Frames = resizedFrames;
            CurrentIndex++; // Automatically select the new Frame.
            CurrentFrame = Frames[CurrentIndex];
        }

        public void Remove(Frame removedFrame)
        {
            if (Frames.Length != 0)
            {
                int newSize = Frames.Length - 1;
                Frame[] resizedFrames = new Frame[newSize];

                int j = 0;
                for (int i = 0; i < Frames.Length; i++)
                {
                    if (Frames[i] != removedFrame)
                    {
                        resizedFrames[j] = Frames[i];
                        CurrentIndex = j;
                        CurrentFrame = Frames[j];
                        j++;
                    }
                }
                Frames = resizedFrames;

                if (Frames.Length == 0)
                {
                    CurrentFrame = null;
                    CurrentIndex = 0;
                }
            }
        }

        /// <summary>
        /// Changes the index order of the Frames in the array.
        /// </summary>
        /// <param name="isFwd"></param>
        public void SwapOrder(bool isFwd)
        {
            if (Frames.Length > 1)
            {
                if (isFwd)
                {
                    if (CurrentIndex + 1 < Frames.Length)
                    {
                        Frames[CurrentIndex] = Frames[CurrentIndex + 1];
                        Frames[CurrentIndex + 1] = CurrentFrame;
                        CurrentIndex++;
                    }
                }
                else
                {
                    if (CurrentIndex - 1 >= 0)
                    {
                        Frames[CurrentIndex] = Frames[CurrentIndex - 1];
                        Frames[CurrentIndex - 1] = CurrentFrame;
                        CurrentIndex--;
                    }
                }
                CurrentFrame = Frames[CurrentIndex];
            }
        }

        /// <summary>
        /// Returns the next Frame in the array.
        /// </summary>
        /// <returns></returns>
        public void NextFrame()
        {
            Frame currentFrame = null;

            if (CurrentIndex + 1 <= Frames.Length - 1)
            {
                currentFrame = Frames[CurrentIndex + 1];
                CurrentIndex++;
            }
            else
            {
                currentFrame = Frames[0];
                CurrentIndex = 0;
            }
            CurrentFrame = Frames[CurrentIndex];
            CurrentFrame.CurrentTick = 0; // Used in animation playback.
        }

        /// <summary>
        /// Returns the previous Frame in the array.
        /// </summary>
        /// <returns></returns>
        public void PreviousFrame()
        {
            Frame currentFrame = null;

            if (CurrentIndex - 1 >= 0)
            {
                currentFrame = Frames[CurrentIndex - 1];
                CurrentIndex--;
            }
            else
            {
                currentFrame = Frames[Frames.Length - 1];
                CurrentIndex = Frames.Length - 1;
            }
            CurrentFrame = Frames[CurrentIndex];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
