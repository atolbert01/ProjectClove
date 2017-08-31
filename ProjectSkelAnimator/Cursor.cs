using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectSkelAnimator
{
    enum CursorState { Arrow, OpenHand, ClosedHand, AdjustBounds, Pencil, Eraser, Translate, Rotate, TextEdit };
    class Cursor : Sliceable
    {
        public Rectangle DestRect { get; set; }
        public Rectangle BoundsRect { get; set; }
        public Frame Frame { get; set; }
        public bool IsOutOfBounds { get; set; }
        public TextField EditingField { get; set; }
        public string Text { get; set; }
        public MouseState MouseState { get; set; }
        public MouseState PrevMouseState { get; set; }
        public KeyboardState KeyState { get; set; }
        public KeyboardState PrevKeyState { get; set; }
        public CursorState State { get; set; }
        public CursorState PrevState { get; set; }
        public Part SelectedPart { get; set; }
        public Part StandbyPart { get; set; }
        public SourceRectangleInfo[] SourceRectangles { get; set; }
        public int IDCount = 0;
        Texture2D cursorTexture;
        GraphicsDeviceManager graphics;
        SpriteFont consolas;

        int gridSize = 16;
        int startBoundsX, startBoundsY, prevBoundsX, prevBoundsY, prevBoundsWidth, prevBoundsHeight, boundsX, boundsY, boundsWidth, boundsHeight;

        bool partAdded = true;
        bool showHelp = false;

        string helpTextPrompt = "[ Tab = Show/Hide Help ]";
        string helpText = "A = Arrow | D = Draw | E = Erase | F = Transpose | R = Rotate";
        string helpText2 = "Left = Select Previous Part | Right = Select Next Part | Up = Increase Draw Order | Down = Decrease Draw Order";

        public Cursor(Texture2D cursorTexture, GraphicsDeviceManager graphics, Frame frame, SpriteFont consolas)
        {
            this.cursorTexture = cursorTexture;
            this.graphics = graphics;
            this.consolas = consolas;
            Frame = frame;
            IsOutOfBounds = false;
        }

        public void Load()
        {
            SourceRectangles = base.SliceSourceRectangles(cursorTexture, gridSize);
        }

        public void Update()
        {
            //if (KeyState != null) { PrevKeyState = KeyState; }
            if (MouseState != null) { PrevMouseState = MouseState; }
            if (boundsX != 0) { prevBoundsX = boundsX; }
            if (boundsY != 0) { prevBoundsY = boundsY; }
            
            MouseState = Mouse.GetState();
            KeyState = Keyboard.GetState();
            DestRect = new Rectangle(MouseState.X, MouseState.Y, 1, 1);

            if (Frame != null)
            {
                if (Frame.SelectedPart != null)
                {
                    SelectedPart = Frame.SelectedPart;
                }
                else
                {
                    SelectedPart = null;
                }
            }

            if (MouseState.Y > graphics.GraphicsDevice.Viewport.Height - 64)
            {
                State = CursorState.Arrow;
                IsOutOfBounds = true;
            }
            else
            {
                if (IsOutOfBounds)
                {
                    State = PrevState;
                    IsOutOfBounds = false;
                }
                if (State != CursorState.TextEdit)
                {
                    if (KeyState.IsKeyDown(Keys.A)) { State = CursorState.Arrow; }
                    else if (KeyState.IsKeyDown(Keys.F)) { State = CursorState.OpenHand; }
                    else if (KeyState.IsKeyDown(Keys.D)) { State = CursorState.Pencil; }
                    else if (KeyState.IsKeyDown(Keys.E)) { State = CursorState.Eraser; }
                    else if (KeyState.IsKeyDown(Keys.R)) { State = CursorState.Rotate; }
                    else if (KeyState.IsKeyDown(Keys.B)) { State = CursorState.AdjustBounds; }
                }
            }

            if (KeyState.IsKeyDown(Keys.Up) && PrevKeyState.IsKeyUp(Keys.Up)) { Frame.SwapPartOrder(true); }
            if (KeyState.IsKeyDown(Keys.Down) && PrevKeyState.IsKeyUp(Keys.Down)) { Frame.SwapPartOrder(false); }

            if (KeyState.IsKeyDown(Keys.Right) && PrevKeyState.IsKeyUp(Keys.Right)) { SelectedPart = Frame.NextPart(); }
            if (KeyState.IsKeyDown(Keys.Left) && PrevKeyState.IsKeyUp(Keys.Left)) { SelectedPart = Frame.PreviousPart(); }

            if (KeyState.IsKeyDown(Keys.Tab) && PrevKeyState.IsKeyUp(Keys.Tab)) { showHelp = !showHelp; }

            switch (State)
            {
                case CursorState.Arrow:
                    break;
                case CursorState.OpenHand:
                    PrevState = CursorState.OpenHand;
                    if (MouseState.LeftButton == ButtonState.Pressed) { State = CursorState.ClosedHand; }
                    break;
                case CursorState.ClosedHand:
                    PrevState = CursorState.ClosedHand;
                    if (MouseState.LeftButton != ButtonState.Pressed) { State = CursorState.OpenHand; }
                    if (SelectedPart != null)
                    {
                        if (DestRect.Intersects(SelectedPart.DestRect))
                        {
                            SelectedPart.Position = new Vector2(MouseState.X, MouseState.Y);
                            SelectedPart.DestRect = new Rectangle((int)SelectedPart.Position.X - SelectedPart.SourceRect.Width / 2, (int)SelectedPart.Position.Y - SelectedPart.SourceRect.Height / 2, SelectedPart.SourceRect.Width, SelectedPart.SourceRect.Width);
                            SelectedPart.Origin = new Vector2(SelectedPart.DestRect.Width / 2, SelectedPart.DestRect.Height / 2);
                            SelectedPart.WorldOrigin = new Vector2((int)SelectedPart.Position.X, (int)SelectedPart.Position.Y);
                        }
                    }
                    break;
                case CursorState.AdjustBounds:
                    PrevState = CursorState.AdjustBounds;
                    if (PrevMouseState.LeftButton == ButtonState.Released && MouseState.LeftButton == ButtonState.Pressed)
                    {
                        startBoundsX = MouseState.X;
                        startBoundsY = MouseState.Y;
                    }
                    else if (PrevMouseState.LeftButton == ButtonState.Pressed && MouseState.LeftButton == ButtonState.Pressed)
                    {
                        boundsX = MouseState.X;
                        boundsY = MouseState.Y;
                        boundsWidth = Math.Abs(boundsX - startBoundsX);
                        boundsHeight = Math.Abs(boundsY - startBoundsY);
                        BoundsRect = new Rectangle(startBoundsX, startBoundsY, boundsWidth, boundsHeight);
                    }
                    break;
                case CursorState.Pencil:
                    PrevState = CursorState.Pencil;
                    if (MouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (StandbyPart != null)
                        {
                            partAdded = false;
                            StandbyPart.ID = IDCount;
                            StandbyPart.Position = new Vector2(MouseState.X, MouseState.Y);
                            StandbyPart.DestRect = new Rectangle((int)StandbyPart.Position.X - StandbyPart.SourceRect.Width / 2, (int)StandbyPart.Position.Y - StandbyPart.SourceRect.Height / 2, StandbyPart.SourceRect.Width, StandbyPart.SourceRect.Width);
                            StandbyPart.Origin = new Vector2(StandbyPart.DestRect.Width / 2, StandbyPart.DestRect.Height / 2);
                            StandbyPart.WorldOrigin = new Vector2((int)StandbyPart.Position.X, (int)StandbyPart.Position.Y);
                            StandbyPart.State = PartState.Selected;
                        }
                    }

                    if (!partAdded)
                    {
                        if (MouseState.LeftButton == ButtonState.Released)
                        {
                            if (Frame.Parts.Length < 1)
                            {
                                Frame.AddPart(StandbyPart);
                            }
                            else
                            {
                                Frame.InsertPart(StandbyPart);
                            }
                            IDCount++;
                            partAdded = true;
                            StandbyPart = new Part(StandbyPart.Texture, StandbyPart.SourceRect, StandbyPart.DestRect);
                        }
                    }

                    break;
                case CursorState.Translate:
                    PrevState = CursorState.Translate;
                    break;
                case CursorState.Eraser:
                    PrevState = CursorState.Eraser;
                    if (SelectedPart != null)
                    {
                        if (DestRect.Intersects(SelectedPart.DestRect))
                        {
                            if (MouseState.LeftButton == ButtonState.Pressed && PrevMouseState.LeftButton == ButtonState.Released)
                            {
                                Frame.RemovePart(SelectedPart);
                            }
                        }
                    }
                    break;
                case CursorState.Rotate:
                    PrevState = CursorState.Rotate;
                    if (SelectedPart != null)
                    {
                        if (MouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (MouseState.X < SelectedPart.WorldOrigin.X)
                            {
                                SelectedPart.Rotation -= .001f * (Math.Abs(MouseState.X - SelectedPart.WorldOrigin.X));
                            }
                            else if (MouseState.X > SelectedPart.WorldOrigin.X)
                            {
                                SelectedPart.Rotation += .001f * (Math.Abs(MouseState.X - SelectedPart.WorldOrigin.X));
                            }
                        }
                    }
                    break;
                case CursorState.TextEdit:
                    Keys[] currentKeys = KeyState.GetPressedKeys();
                    Keys[] prevKeys = PrevKeyState.GetPressedKeys();
                    bool found = false;
                    for (int i = 0; i < currentKeys.Length; i++)
                    {
                        found = false;
                        for (int j = 0; j < prevKeys.Length; j++)
                        {
                            if (currentKeys[i] == prevKeys[j])
                            {
                                // The two Keys arrays match so we don't need to add to the current string.
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            // The two Keys arrays don't match so we need to add a new letter to the current string.
                            PressKey(currentKeys[i]);
                        }
                    }
                    break;
            }
            if (KeyState != null) { PrevKeyState = KeyState; }
        }

        public void PressKey(Keys key)
        {
            if (key == Keys.Back)
            {
                if (Text.Length > 0)
                {
                    Text = Text.Substring(0, Text.Length - 1);
                }
            }
            else if (key == Keys.Enter)
            {
                EditingField.PrevState = EditingField.State;
                EditingField.State = TextState.None;
                State = CursorState.Arrow;
            }
            else
            {
                if(Text.Length + 1 <= EditingField.MaxChars)
                {
                    Text = (Text + (char)key).ToLower();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (SelectedPart != null)
            {
                if (State == CursorState.Rotate)
                {
                    spriteBatch.Draw(cursorTexture, SelectedPart.WorldOrigin, SourceRectangles[3].SourceRect, Color.White);
                }
            }

            if (showHelp)
            {
                spriteBatch.DrawString(consolas, helpTextPrompt, new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, 368), Color.White, 0, consolas.MeasureString(helpTextPrompt) / 2, 1.0f, SpriteEffects.None, 0);
                spriteBatch.DrawString(consolas, helpText, new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, 388), Color.White, 0, consolas.MeasureString(helpText) / 2, 1.0f, SpriteEffects.None, 0);
                spriteBatch.DrawString(consolas, helpText2, new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, 408), Color.White, 0, consolas.MeasureString(helpText2) / 2, 1.0f, SpriteEffects.None, 0);
            }

            // Draw the cursor.
            spriteBatch.Draw(cursorTexture, new Vector2(DestRect.X, DestRect.Y), SourceRectangles[(int)State].SourceRect, Color.White, 0f, new Vector2(0, 0), 1, SpriteEffects.None, 0f);
        }
    }
}