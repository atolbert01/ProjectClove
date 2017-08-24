using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectSkelAnimator
{
    enum CursorState { Arrow, OpenHand, ClosedHand, SelectGroup, Pencil, Eraser, Translate, Rotate };
    class Cursor : Sliceable
    {
        public Rectangle DestRect { get; set; }
        public Frame Frame { get; set; }
        public MouseState MouseState;
        public MouseState PrevMouseState;
        public KeyboardState KeyboardState;
        public KeyboardState PrevKeyboardState;
        public CursorState State;
        public Part SelectedPart;
        public Part StandbyPart;
        public SourceRectangleInfo[] SourceRectangles;
        public int IDCount = 0;

        Texture2D cursorTexture;
        GraphicsDeviceManager graphics;
        CursorState prevState;
        SpriteFont consolas;

        int gridSize = 16;

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
        }

        public void Load()
        {
            SourceRectangles = base.SliceSourceRectangles(cursorTexture, gridSize);
        }

        public void Update()
        {
            if (KeyboardState != null) { PrevKeyboardState = KeyboardState; }
            if (MouseState != null) { PrevMouseState = MouseState; }

            MouseState = Mouse.GetState();
            KeyboardState = Keyboard.GetState();
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
            }
            else
            {
                if (KeyboardState.IsKeyDown(Keys.A)) { State = CursorState.Arrow; }
                else if (KeyboardState.IsKeyDown(Keys.F)) { State = CursorState.OpenHand; }
                else if (KeyboardState.IsKeyDown(Keys.D)) { State = CursorState.Pencil; }
                else if (KeyboardState.IsKeyDown(Keys.E)) { State = CursorState.Eraser; }
                else if (KeyboardState.IsKeyDown(Keys.R)) { State = CursorState.Rotate; }
            }

            if (KeyboardState.IsKeyDown(Keys.Up) && PrevKeyboardState.IsKeyUp(Keys.Up)) { Frame.SwapPartOrder(true); }
            if (KeyboardState.IsKeyDown(Keys.Down) && PrevKeyboardState.IsKeyUp(Keys.Down)) { Frame.SwapPartOrder(false); }

            if (KeyboardState.IsKeyDown(Keys.Right) && PrevKeyboardState.IsKeyUp(Keys.Right)) { SelectedPart = Frame.NextPart(); }
            if (KeyboardState.IsKeyDown(Keys.Left) && PrevKeyboardState.IsKeyUp(Keys.Left)) { SelectedPart = Frame.PreviousPart(); }

            if (KeyboardState.IsKeyDown(Keys.Tab) && PrevKeyboardState.IsKeyUp(Keys.Tab)) { showHelp = !showHelp; }

            switch (State)
            {
                case CursorState.Arrow:
                    break;
                case CursorState.OpenHand:
                    prevState = CursorState.OpenHand;
                    if (MouseState.LeftButton == ButtonState.Pressed) { State = CursorState.ClosedHand; }
                    break;
                case CursorState.ClosedHand:
                    prevState = CursorState.ClosedHand;
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
                case CursorState.SelectGroup:
                    prevState = CursorState.SelectGroup;
                    //code
                    break;
                case CursorState.Pencil:
                    prevState = CursorState.Pencil;
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
                    prevState = CursorState.Translate;
                    break;
                case CursorState.Eraser:
                    prevState = CursorState.Eraser;
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
                    prevState = CursorState.Rotate;
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