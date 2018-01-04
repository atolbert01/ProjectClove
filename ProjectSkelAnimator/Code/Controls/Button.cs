using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectCloveAnimator
{
    class Button : Sliceable
    {
        public Rectangle DestRect { get; set; }
        public Rectangle SourceRect { get; set; }
        public Texture2D Texture { get; set; }
        public bool IsClicked { get; set; }
        public int SourceIndex { get; set; }
        public Cursor Cursor { get; set; }
        public Animation CurrentAnimation { get; set; } // certain sub classes of Button will need to access the CurrentAnimation
        public bool QuickClick;
        public bool IsCursorIntersect { get; set; }
        public int GridSize = 16;
        public Color Tint = Color.White;
        public string TipText { get; set; }
        SourceRectangleInfo[] SourceRectangles;
        public Button(Texture2D texture, Rectangle destRect, Animation currentAnimation)
        {
            Texture = texture;
            DestRect = destRect;
            CurrentAnimation = CurrentAnimation;
            IsClicked = false;
            IsCursorIntersect = false;
            TipText = "";

        }
        public void Update(Cursor cursor, Animation currentAnimation)
        {
            Cursor = cursor;
            CurrentAnimation = currentAnimation;
            if (Cursor.DestRect.Intersects(DestRect))
            {
                if (!IsCursorIntersect)
                {
                    Cursor.PrevState = Cursor.State;
                }
                IsCursorIntersect = true;
                Cursor.State = CursorState.Arrow;
                if (TipText != "")
                {
                    Cursor.Tip.Text = TipText;
                    Cursor.ShowToolTip = true;
                }
            }
            else if (!Cursor.DestRect.Intersects(DestRect))
            {
                if (IsCursorIntersect)
                {
                    Cursor.State = Cursor.PrevState;
                    IsCursorIntersect = false;
                    Cursor.ShowToolTip = false;
                }
            }

            if (IsClicked == true)
            {
                Execute();
            }
        }

        public virtual void Execute()
        {
            Tint = Color.DarkRed;
        }

        public void Load(int sourceIndex)
        {
            SourceRectangles = base.SliceSourceRectangles(Texture, GridSize);
            SourceRect = SourceRectangles[sourceIndex].SourceRect;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestRect, SourceRect, Tint);
        }
    }

    class PlayButton : Button
    {
        public PlayButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 14;
            QuickClick = false;
            base.TipText = "Play Animation";
        }

        public override void Execute()
        {
            base.Execute();
            if (CurrentAnimation.CurrentFrame.CurrentTick == CurrentAnimation.CurrentFrame.Ticks)
            {
                CurrentAnimation.NextFrame();
                //textScript.Text = _currentAnimation.Frames[_currentAnimation.CurrentFrameIndex].Script;
            }
            CurrentAnimation.CurrentFrame.CurrentTick++;
        }
    }

    class PauseButton : Button
    {
        public PauseButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 13;
            QuickClick = false;
            base.TipText = "Pause Animation";
        }
    }

    class NextButton : Button
    {
        public NextButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 15;
            QuickClick = true;
            base.TipText = "Next Frame";
        }

        public override void Execute()
        {
            base.Execute();
            CurrentAnimation.NextFrame();
            //textScript.Text = currentAnimation.Frames[currentAnimation.CurrentFrameIndex].Script;
        }
    }

    class PrevButton : Button
    {
        public PrevButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 12;
            QuickClick = true;
            base.TipText = "Previous Frame";
        }
        public override void Execute()
        {
            base.Execute();
            CurrentAnimation.PreviousFrame();
            //textScript.Text = currentAnimation.Frames[currentAnimation.CurrentFrameIndex].Script;
        }
    }

    class NewFrameButton : Button
    {
        public NewFrameButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 18;
            QuickClick = true;
            base.TipText = "Add New Animation Frame";
        }

        public override void Execute()
        {
            base.Execute();
            Frame newFrame = CurrentAnimation.CurrentFrame;
            if (CurrentAnimation.Frames.Length > 1)
            {
                CurrentAnimation.AddFrame(new Frame(newFrame, false, CurrentAnimation.Bounds.Center));
                //textScript.Text = currentAnimation.Frames[currentAnimation.CurrentFrameIndex].Script;
            }
            else
            {
                CurrentAnimation.InsertFrame(new Frame(newFrame, false, CurrentAnimation.Bounds.Center));
                //textScript.Text = currentAnimation.Frames[currentAnimation.CurrentFrameIndex].Script;
            }
        }
    }

    class DeleteFrameButton : Button
    {
        public DeleteFrameButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 19;
            QuickClick = true;
            base.TipText = "Delete the current Animation Frame";
        }

        public override void Execute()
        {
            base.Execute();
            if (CurrentAnimation.Frames.Length > 1)
            {
                CurrentAnimation.RemoveFrame(CurrentAnimation.CurrentFrameIndex);
                //CurrentAnimation.RemoveFrame(CurrentAnimation.CurrentFrame);
            } // There will always be at least 1 frame.
        }
    }

    class PlusTicksButton : Button
    {
        public PlusTicksButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 16;
            QuickClick = true;
            base.TipText = "Increase Frame duration";
        }
        public override void Execute()
        {
            base.Execute();
            CurrentAnimation.CurrentFrame.Ticks++;
        }
    }

    class MinusTicksButton : Button
    {
        public MinusTicksButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 17;
            QuickClick = true;
            base.TipText = "Reduce Frame duration";
        }

        public override void Execute()
        {
            base.Execute();
            if (CurrentAnimation.CurrentFrame.Ticks > 1) { CurrentAnimation.CurrentFrame.Ticks--; } // 1 is the least possible number of ticks for a frame.
        }
    }

    class AddAnimButton : Button
    {
        public AddAnimButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 16;
            QuickClick = true;
            base.TipText = "Add a new Animation to AnimationGroup";
        }
    }

    class RemoveAnimButton : Button
    {
        public RemoveAnimButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 17;
            QuickClick = true;
            base.TipText = "Delete Animation from AnimationGroup";
        }
    }

    class OnionSkinButton : Button
    {
        public OnionSkinButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 9;
            QuickClick = true;
            base.TipText = "Toggle onion skinning";
        }
    }

    class TweenButton : Button
    {
        public int TweenCount { get; set; }
        public TweenButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 10;
            QuickClick = true;
            TweenCount = 0;
            base.TipText = "Tween frames";
        }

        public override void Execute()
        {
            base.Execute();
            if (CurrentAnimation.CurrentFrameIndex + 1 <= CurrentAnimation.Frames.Length)
            {
                if (CurrentAnimation.CurrentFrameIndex + 1 < CurrentAnimation.Frames.Length) { TweenFrames(CurrentAnimation.CurrentFrame, CurrentAnimation.Frames[CurrentAnimation.CurrentFrameIndex + 1], TweenCount); }
            }
        }

        void TweenFrames(Frame startFrame, Frame endFrame, int tweenCount)
        {
            //tweenCount = Int32.Parse(textTweenFrames.Text);
            for (int i = 1; i < tweenCount; i++)
            {
                Frame thisFrame = new Frame();
                foreach (Part startPart in startFrame.Parts)
                {
                    foreach (Part endPart in endFrame.Parts)
                    {
                        if (startPart.ID == endPart.ID)
                        {
                            Part newPart = new Part(startPart.ID, startPart.Texture, startPart.SourceRect, endPart.DestRect);
                            newPart.Position = endPart.Position;
                            newPart.Rotation = startPart.Rotation;

                            if (startPart.Position != endPart.Position)
                            {
                                if (endPart.Position.X > startPart.Position.X && endPart.Position.Y < startPart.Position.Y) // I Quadrant
                                {
                                    newPart.Position = new Vector2(startPart.Position.X + ((endPart.Position.X - startPart.Position.X) / tweenCount) * i, startPart.Position.Y - ((startPart.Position.Y - endPart.Position.Y) / tweenCount) * i);
                                }
                                else if (endPart.Position.X < startPart.Position.X && endPart.Position.Y < startPart.Position.Y) // II Quadrant
                                {
                                    newPart.Position = new Vector2(startPart.Position.X - ((startPart.Position.X - endPart.Position.X) / tweenCount) * i, startPart.Position.Y - ((startPart.Position.Y - endPart.Position.Y) / tweenCount) * i);
                                }
                                else if (endPart.Position.X < startPart.Position.X && endPart.Position.Y > startPart.Position.Y) // III Quadrant
                                {
                                    newPart.Position = new Vector2(startPart.Position.X - ((startPart.Position.X - endPart.Position.X) / tweenCount) * i, startPart.Position.Y + ((endPart.Position.Y - startPart.Position.Y) / tweenCount) * i);
                                }
                                else if (endPart.Position.X > startPart.Position.X && endPart.Position.Y > startPart.Position.Y) // IV Quadrant
                                {
                                    newPart.Position = new Vector2(startPart.Position.X + ((endPart.Position.X - startPart.Position.X) / tweenCount) * i, startPart.Position.Y + ((endPart.Position.Y - startPart.Position.Y) / tweenCount) * i);
                                }
                            }

                            if (startPart.Rotation != endPart.Rotation)
                            {
                                if (endPart.Rotation > startPart.Rotation)
                                {
                                    newPart.Rotation += ((endPart.Rotation - startPart.Rotation) / tweenCount) * i;
                                }
                                else
                                {
                                    newPart.Rotation -= ((startPart.Rotation - endPart.Rotation) / tweenCount) * i;
                                }
                            }
                            thisFrame.AddPart(newPart);
                        }
                    }
                }
                CurrentAnimation.InsertFrame(thisFrame);
            }
        }
    }

    class UpButton : Button
    {
        public UpButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 22;
            QuickClick = true;
            base.TipText = "";
        }
    }

    class DownButton : Button
    {
        public DownButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 23;
            QuickClick = true;
            base.TipText = "";
        }
    }

    class LeftButton : Button
    {
        public LeftButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 20;
            QuickClick = true;
            base.TipText = "";
        }
    }

    class RightButton : Button
    {
        public RightButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 21;
            QuickClick = true;
            base.TipText = "";
        }
    }

    class SaveButton : Button
    {
        public SaveButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 24;
            QuickClick = true;
            base.TipText = "";
        }
    }

    class LoadButton : Button
    {
        public LoadButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 25;
            QuickClick = true;
            base.TipText = "";
        }
    }

    class EyeButton : Button
    {
        public EyeButton(Texture2D texture, Rectangle destRect, Animation currentAnimation) : base(texture, destRect, currentAnimation)
        {
            SourceIndex = 11;
            QuickClick = false;
            base.TipText = "Show this Animation";
        }
    }
    /// <summary>
    /// A ButtonPanel is a container for a button array. It handles Load, Update, and Draw calls for buttons in the array.
    /// </summary>
    class ButtonPanel
    {
        public Button[] Buttons { get; set; }
        public Button ClickedButton;
        public ButtonPanel(Button[] buttons)
        {
            Buttons = buttons;
        }

        public void Load()
        {
            foreach (Button btn in Buttons)
            {
                if (btn != null)
                {
                    btn.Load(btn.SourceIndex);
                }
            }
        }

        public void Update(Cursor cursor, Animation currentAnimation)
        {
            foreach (Button btn in Buttons)
            {
                if (btn != null)
                {
                    if (ClickedButton == null)
                    {
                        btn.Tint = Color.White;
                        if (cursor.DestRect.Intersects(btn.DestRect))
                        {
                            btn.Tint = Color.Yellow;
                            if (cursor.MouseState.LeftButton == ButtonState.Pressed && cursor.PrevMouseState.LeftButton == ButtonState.Released)
                            {
                                ClickedButton = btn;
                                ClickedButton.IsClicked = true;
                            }
                        }
                        else
                        {
                            btn.Tint = Color.White;
                        }
                    }
                    else if (btn != ClickedButton)
                    {
                        btn.Tint = Color.White;
                        if (cursor.DestRect.Intersects(btn.DestRect))
                        {
                            btn.Tint = Color.Yellow;
                            if (cursor.MouseState.LeftButton == ButtonState.Pressed && cursor.PrevMouseState.LeftButton == ButtonState.Released)
                            {
                                ClickedButton.IsClicked = false;
                                ClickedButton.Tint = Color.White;
                                btn.IsClicked = true;
                                ClickedButton = btn;
                            }
                        }
                        else
                        {
                            btn.Tint = Color.White;
                        }
                    }
                    else if (btn == ClickedButton)
                    {
                        if (btn.QuickClick)
                        {
                            ClickedButton.IsClicked = false;
                            btn.Tint = Color.White;
                            ClickedButton = null;
                        }
                        else if (cursor.MouseState.LeftButton == ButtonState.Pressed && cursor.PrevMouseState.LeftButton == ButtonState.Released)
                        {
                            if (cursor.DestRect.Intersects(btn.DestRect))
                            {
                                ClickedButton.IsClicked = false;
                                btn.Tint = Color.White;
                                ClickedButton = null;
                            }
                        }
                    }
                    else
                    {
                        btn.IsClicked = false;
                    }
                    btn.Update(cursor, currentAnimation);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button btn in Buttons)
            {
                if (btn != null)
                {
                    spriteBatch.Draw(btn.Texture, btn.DestRect, btn.SourceRect, btn.Tint);
                }
            }
        }
    }
}
