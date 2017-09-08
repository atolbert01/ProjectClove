using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectSkelAnimator
{
    class Button : Sliceable
    {
        public Rectangle DestRect { get; set; }
        public Rectangle SourceRect { get; set; }
        public Texture2D Texture { get; set; }
        public bool IsClicked { get; set; }
        public int SourceIndex { get; set; }
        public bool QuickClick;
        public bool IsCursorIntersect { get; set; }
        public int GridSize = 16;
        public Color Tint = Color.White;
        SourceRectangleInfo[] SourceRectangles;
        public Button(Texture2D texture, Rectangle destRect)
        {
            Texture = texture;
            DestRect = destRect;
            IsClicked = false;
            IsCursorIntersect = false;

        }
        public void Update(Cursor cursor)
        {
            if (cursor.DestRect.Intersects(DestRect))
            {
                if (!IsCursorIntersect)
                {
                    cursor.PrevState = cursor.State;
                }
                IsCursorIntersect = true;
                cursor.State = CursorState.Arrow;
            }
            else if (!cursor.DestRect.Intersects(DestRect))
            {
                if (IsCursorIntersect)
                {
                    cursor.State = cursor.PrevState;
                    IsCursorIntersect = false;
                }

            }

            if (IsClicked == true)
            {
                Tint = Color.DarkRed;
            }
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
        public PlayButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 14;
            QuickClick = false;
        }
    }

    class PauseButton : Button
    {
        public PauseButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 13;
            QuickClick = false;
        }
    }

    class NextButton : Button
    {
        public NextButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 15;
            QuickClick = true;
        }
    }

    class PrevButton : Button
    {
        public PrevButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 12;
            QuickClick = true;
        }
    }

    class NewFrameButton : Button
    {
        public NewFrameButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 18;
            QuickClick = true;
        }
    }

    class DeleteFrameButton : Button
    {
        public DeleteFrameButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 19;
            QuickClick = true;
        }
    }

    class PlusTicksButton : Button
    {
        public PlusTicksButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 16;
            QuickClick = true;
        }
    }

    class MinusTicksButton : Button
    {
        public MinusTicksButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 17;
            QuickClick = true;
        }
    }

    class OnionSkinButton : Button
    {
        public OnionSkinButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 9;
            QuickClick = true;
        }
    }

    class TweenButton : Button
    {
        public TweenButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 10;
            QuickClick = true;
        }
    }

    class UpButton : Button
    {
        public UpButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 22;
            QuickClick = true;
        }
    }

    class DownButton : Button
    {
        public DownButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 23;
            QuickClick = true;
        }
    }

    class LeftButton : Button
    {
        public LeftButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 20;
            QuickClick = true;
        }
    }

    class RightButton : Button
    {
        public RightButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 21;
            QuickClick = true;
        }
    }

    class SaveButton : Button
    {
        public SaveButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 24;
            QuickClick = true;
        }
    }

    class LoadButton : Button
    {
        public LoadButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 25;
            QuickClick = true;
        }
    }

    class EyeButton : Button
    {
        public EyeButton(Texture2D texture, Rectangle destRect) : base(texture, destRect)
        {
            SourceIndex = 11;
            QuickClick = false;
        }
    }

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

        public void Update(Cursor cursor)
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
                    btn.Update(cursor);
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
