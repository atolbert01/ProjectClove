using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectCloveAnimator
{
    /// <summary>
    /// Defines a set of Animations.
    /// </summary>
    class AnimationGroup
    {
        public string GroupName { get; set; }
        public Animation[] Animations { get; set; }
        public Animation CurrentAnimation { get; set; }
        public TextField[] NameFields { get; set; }
        public SpriteFont TextFont, LabelFont;
        public AnimationGroup(){}

        public AnimationGroup(Animation newAnim, SpriteFont textFont, SpriteFont labelFont)
        {
            CurrentAnimation = newAnim;
            Animations = new Animation[1];
            Animations[0] = CurrentAnimation;
            TextFont = textFont;
            LabelFont = labelFont;
        }

        public void Update(Cursor cursor)
        {
            foreach (TextField text in NameFields)
            {
                if (text != null) { text.Update(cursor); }
            }

            // Make sure the AnimationName matches what is in the TextField
            for (int i = 0; i < Animations.Length; i++)
            {
                if (NameFields[i] != null)
                {
                    if (NameFields[i].Text != Animations[i].AnimationName)
                    {
                        Animations[i].AnimationName = NameFields[i].Text;
                    }
                }
            }
        }

        public void AddAnimation(Animation newAnim)
        {
            Animation[] newAnims = new Animation[Animations.Length + 1];
            for (int i = 0; i < Animations.Length; i++)
            {
                newAnims[i] = Animations[i];
            }
            newAnim.AddFrame(new Frame());
            newAnims[Animations.Length] = newAnim;
            Animations = newAnims;
            CurrentAnimation = Animations[Animations.Length - 1];
        }

        public void DuplicateAnimation()
        {
            Animation newAnim = new Animation(CurrentAnimation.PixelTexture);
            newAnim.Bounds = new Rectangle(CurrentAnimation.Bounds.X, CurrentAnimation.Bounds.Y, CurrentAnimation.Bounds.Width, CurrentAnimation.Bounds.Height);
            foreach (Frame frame in CurrentAnimation.Frames)
            {
                newAnim.AddFrame(new Frame(frame, true, newAnim.Bounds.Center));
            }
            AddAnimation(newAnim);
        }

        public void RemoveAnimation(Animation removedAnim)
        {
            if (Animations.Length != 0 && Animations != null)
            {
                int newSize = Animations.Length - 1;
                Animation[] resizedAnims = new Animation[newSize];

                int j = 0;
                for (int i = 0; i < Animations.Length; i++)
                {
                    if (Animations[i] != removedAnim)
                    {
                        resizedAnims[j] = Animations[i];
                        //CurrentFrameIndex = j;
                        CurrentAnimation = Animations[j];
                        j++;
                    }
                }
                Animations = resizedAnims;

                if (Animations.Length == 0)
                {
                    CurrentAnimation = null;
                    //CurrentFrameIndex = 0;
                }
            }
        }

        public void AddNewTextField(Vector2 textPosition, string defaultText)
        {
            TextField[] newTextFields;
            TextField newTextField = new TextField(TextFont, LabelFont, textPosition, defaultText, 12, CurrentAnimation.PixelTexture);
            if (NameFields != null)
            {
                newTextFields = new TextField[NameFields.Length + 1];
                for (int i = 0; i < NameFields.Length; i++)
                {
                    newTextFields[i] = NameFields[i];
                }
            }
            else
            {
                newTextFields = new TextField[1];
            }
            newTextFields[newTextFields.Length - 1] = newTextField;
            NameFields = newTextFields;
        }

        public void RemoveTextField()
        {
            if (NameFields.Length - 1 > -1)
            {
                TextField[] newTextFields = new TextField[NameFields.Length - 1];
                for (int i = 0; i < newTextFields.Length; i++)
                {
                    newTextFields[i] = NameFields[i];
                }
                NameFields = newTextFields;
            }
            else
            {
                NameFields = NameFields;
            }
        }
    }
}
