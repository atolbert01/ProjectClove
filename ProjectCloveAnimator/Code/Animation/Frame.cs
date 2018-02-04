using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectCloveAnimator
{
    class Frame
    {
        public string Script;
        public Part[] Parts;
        public Part SelectedPart;
        public Part Root;
        public int SelectedIndex = 0;
        public int Ticks = 4;
        public int CurrentTick = 0;

        public Frame()
        {
            //this.cursor = cursor;
            //Parts = parts;
            Parts = new Part[0];
            Script = "";
        }

        public Frame(Frame originalFrame, bool flip, Point center)
        {
            //this.cursor = cursor;
            Parts = new Part[originalFrame.Parts.Length];
            for (int i = 0; i < originalFrame.Parts.Length; i++)
            {

                Parts[i] = new Part(originalFrame.Parts[i].ID, originalFrame.Parts[i].Texture, originalFrame.Parts[i].SourceRect, originalFrame.Parts[i].DestRect);

                Parts[i].Parent = originalFrame.Parts[i].Parent;
                Parts[i].Scale = originalFrame.Parts[i].Scale;
                Parts[i].Origin = originalFrame.Parts[i].Origin;
                Parts[i].WorldOrigin = originalFrame.Parts[i].WorldOrigin;
                Parts[i].Tint = originalFrame.Parts[i].Tint;
                Parts[i].State = originalFrame.Parts[i].State;

                if (flip)
                {
                    Parts[i].Rotation = -originalFrame.Parts[i].Rotation;
                    Parts[i].Position = new Vector2(center.X + (center.X - originalFrame.Parts[i].Position.X), originalFrame.Parts[i].Position.Y);
                    Parts[i].IsFlipped = true;
                }
                else
                {
                    Parts[i].Rotation = originalFrame.Parts[i].Rotation;
                    Parts[i].Position = originalFrame.Parts[i].Position;
                    Parts[i].IsFlipped = false;
                }
            }
            SelectedIndex = originalFrame.SelectedIndex;
            if (Parts.Length > 0)
            {
                SelectedPart = Parts[SelectedIndex];
            }
            else
            {
                SelectedPart = null;
            }
            //Root = originalFrame.Root;
            Ticks = originalFrame.Ticks;
            Script = "";
        }

        public void Update(Cursor cursor)
        {
            foreach (Part part in Parts)
            {
                part.Update(cursor);
            }
        }

        public void SetSelectedPart()
        {
            if (Parts.Length > 0)
            {
                SelectedPart = Parts[SelectedIndex];
                foreach (Part part in Parts)
                {
                    if (part != SelectedPart)
                    {
                        part.State = PartState.Idle;
                    }
                    else
                    {
                        part.State = PartState.Selected;
                    }
                }
            }
        }

        public void AddPart(Part newPart)
        {
            int newSize = Parts.Length + 1;
            Part[] resizedParts = new Part[newSize];

            // Add the existing parts to the new array.
            for (int i = 0; i < Parts.Length; i++)
            {
                resizedParts[i] = Parts[i];
            }
            // Finally, add the new part to the new array.
            resizedParts[newSize - 1] = newPart;
            SelectedIndex = newSize - 1;
            //SelectedPart = newPart;
            Parts = resizedParts;
            SetSelectedPart();

            // if there is only one part set it to be the root.
            if (Parts.Length == 1)
            {
                Root = Parts[0];
            }
        }

        /// <summary>
        /// Inserts a new part at the selected index + 1. The part at the selected index will become the parent of the new part.
        /// The array is recreated up to and including the selected index, the new part is inserted, then the rest of the array is added.
        /// </summary>
        public void InsertPart(Part newPart)
        {
            int newSize = Parts.Length + 1;
            Part[] resizedParts = new Part[newSize];

            int j = 0;
            for (int i = 0; i < resizedParts.Length; i++)
            {
                if (i != SelectedIndex + 1)
                {
                    resizedParts[i] = Parts[j];
                    j++;
                }
                else // We just added what will become the newPart's parent, now add newPart
                {
                    newPart.SetParent(Parts[SelectedIndex]);
                    resizedParts[i] = newPart;
                }
            }

            Parts = resizedParts;
            SelectedIndex++; // Automatically select the new part.
            SetSelectedPart();
        }

        public void RemovePart(Part removedPart)
        {
            if (Parts.Length != 0)
            {
                int newSize = Parts.Length - 1;
                Part[] resizedParts = new Part[newSize];

                int j = 0;
                for (int i = 0; i < Parts.Length; i++)
                {
                    if (Parts[i] != removedPart)
                    {
                        resizedParts[j] = Parts[i];
                        SelectedIndex = j;
                        SelectedPart = Parts[j];
                        j++;
                    }
                }
                Parts = resizedParts;
                // if there is only one part set it to be the root.
                if (Parts.Length == 1)
                {
                    Root = Parts[0];
                }

                if (Parts.Length == 0)
                {
                    Root = null;
                    SelectedPart = null;
                    SelectedIndex = 0;
                }
                SetSelectedPart();
            }
        }

        /// <summary>
        /// Sets a new root.
        /// </summary>
        /// <param name="newRoot"></param>
        public void ChangeRoot(Part newRoot)
        {
            Root = newRoot;
        }

        /// <summary>
        /// Changes the index order of the Parts array, which changes the draw order of the parts in the Frame.
        /// </summary>
        /// <param name="isUp"></param>
        public void SwapPartOrder(bool isUp)
        {
            if (Parts.Length > 1)
            {
                if (isUp)
                {
                    if (SelectedIndex + 1 < Parts.Length)
                    {
                        Parts[SelectedIndex] = Parts[SelectedIndex + 1];
                        Parts[SelectedIndex + 1] = SelectedPart;
                        SelectedIndex++;
                    }
                }
                else
                {
                    if (SelectedIndex - 1 >= 0)
                    {
                        Parts[SelectedIndex] = Parts[SelectedIndex - 1];
                        Parts[SelectedIndex - 1] = SelectedPart;
                        SelectedIndex--;
                    }
                }
                SetSelectedPart();
            }
        }

        /// <summary>
        /// Returns the next part in the hierarchy.
        /// </summary>
        /// <returns></returns>
        public Part NextPart()
        {
            Part selectedPart = null;

            if (SelectedIndex + 1 <= Parts.Length - 1)
            {
                selectedPart = Parts[SelectedIndex + 1];
                SelectedIndex++;
            }
            else
            {
                selectedPart = Parts[0];
                SelectedIndex = 0;
            }
            SetSelectedPart();
            return selectedPart;
        }

        /// <summary>
        /// Returns the previous part in the hierarchy.
        /// </summary>
        /// <returns></returns>
        public Part PreviousPart()
        {
            Part selectedPart = null;

            if (SelectedIndex - 1 >= 0)
            {
                selectedPart = Parts[SelectedIndex - 1];
                SelectedIndex--;
            }
            else
            {
                selectedPart = Parts[Parts.Length - 1];
                SelectedIndex = Parts.Length - 1;
            }
            SetSelectedPart();
            return selectedPart;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Parts != null)
            {

                foreach (Part part in Parts)
                {
                    if (part != null)
                    {
                        part.Draw(spriteBatch);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (Parts != null)
            {

                foreach (Part part in Parts)
                {
                    if (part != null)
                    {
                        part.Draw(spriteBatch, color);
                    }
                }
            }
        }
    }
}