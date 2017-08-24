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
        public Rectangle Bounds { get; set; }
        public Part[] Parts { get; set; }
        public Frame[] Frames { get; set; }
        public Texture2D PixelTexture { get; set; }
        public Frame CurrentFrame;
        public int CurrentFrameIndex = 0;
        public int SelectedPartIndex = 0;
        public Animation(Texture2D pixelTexture)
        {
            PixelTexture = pixelTexture;
            Bounds = new Rectangle(200,200,128,196);
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

        public void AddFrame(Frame newFrame)
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
            CurrentFrameIndex = newSize - 1;
            Frames = resizedFrames;
            CurrentFrame = Frames[CurrentFrameIndex];
        }

        /// <summary>
        /// Inserts a new frame at the CurrentIndex + 1. The array is recreated up to and including the CurrentIndex, the new frame is inserted, then the rest of the array is added.
        /// </summary>
        public void InsertFrame(Frame newFrame)
        {
            int newSize = Frames.Length + 1;
            Frame[] resizedFrames = new Frame[newSize];

            int j = 0;
            for (int i = 0; i < resizedFrames.Length; i++)
            {
                if (i != CurrentFrameIndex + 1)
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
            CurrentFrameIndex++; // Automatically select the new Frame.
            CurrentFrame = Frames[CurrentFrameIndex];
        }

        public void RemoveFrame(Frame removedFrame)
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
                        CurrentFrameIndex = j;
                        CurrentFrame = Frames[j];
                        j++;
                    }
                }
                Frames = resizedFrames;

                if (Frames.Length == 0)
                {
                    CurrentFrame = null;
                    CurrentFrameIndex = 0;
                }
            }
        }

        /// <summary>
        /// Changes the index order of the Frames in the array.
        /// </summary>
        /// <param name="isFwd"></param>
        public void SwapFrameOrder(bool isFwd)
        {
            if (Frames.Length > 1)
            {
                if (isFwd)
                {
                    if (CurrentFrameIndex + 1 < Frames.Length)
                    {
                        Frames[CurrentFrameIndex] = Frames[CurrentFrameIndex + 1];
                        Frames[CurrentFrameIndex + 1] = CurrentFrame;
                        CurrentFrameIndex++;
                    }
                }
                else
                {
                    if (CurrentFrameIndex - 1 >= 0)
                    {
                        Frames[CurrentFrameIndex] = Frames[CurrentFrameIndex - 1];
                        Frames[CurrentFrameIndex - 1] = CurrentFrame;
                        CurrentFrameIndex--;
                    }
                }
                CurrentFrame = Frames[CurrentFrameIndex];
            }
        }

        /// <summary>
        /// Returns the next Frame in the array.
        /// </summary>
        /// <returns></returns>
        public void NextFrame()
        {
            Frame currentFrame = null;

            if (CurrentFrameIndex + 1 <= Frames.Length - 1)
            {
                currentFrame = Frames[CurrentFrameIndex + 1];
                CurrentFrameIndex++;
            }
            else
            {
                currentFrame = Frames[0];
                CurrentFrameIndex = 0;
            }
            CurrentFrame = Frames[CurrentFrameIndex];
            CurrentFrame.CurrentTick = 0; // Used in animation playback.
        }

        /// <summary>
        /// Returns the previous Frame in the array.
        /// </summary>
        /// <returns></returns>
        public void PreviousFrame()
        {
            Frame currentFrame = null;

            if (CurrentFrameIndex - 1 >= 0)
            {
                currentFrame = Frames[CurrentFrameIndex - 1];
                CurrentFrameIndex--;
            }
            else
            {
                currentFrame = Frames[Frames.Length - 1];
                CurrentFrameIndex = Frames.Length - 1;
            }
            CurrentFrame = Frames[CurrentFrameIndex];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PixelTexture, Bounds, Color.Red);
        }
    }
}
