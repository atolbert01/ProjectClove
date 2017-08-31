using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectSkelAnimator
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont consolas;
        Texture2D[] partTextures;
        Texture2D cursorTexture;
        Texture2D transparentTexture;
        Texture2D pixelTexture;
        PartsPalette partsPalette;
        Frame currentFrame;
        Cursor cursor;
        ButtonPanel buttonPanel;
        PlayButton playButton;
        PauseButton pauseButton;
        NextButton nextButton;
        PrevButton prevButton;
        NewFrameButton newFrameButton;
        DeleteFrameButton deleteFrameButton;
        PlusTicksButton plusTicksButton;
        OnionSkinButton onionSkinButton;
        TweenButton tweenButton;
        SaveButton saveButton;
        LoadButton loadButton;
        MinusTicksButton minusTicksButton;
        Animation animation;
        TextField textBoundsX;
        TextField textBoundsY;
        TextField textBoundsWidth;
        TextField textBoundsHeight;
        TextField textTweenFrames;
        string frameName = "Frame: ";
        string frameTicks = "Ticks: ";
        bool isOnionSkin = false;
        int tweenCount = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            partTextures = new Texture2D[32];
            pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            pixelTexture.SetData(new Color[] { Color.White });
            animation = new Animation(pixelTexture);
            animation.AddFrame(new Frame());
            currentFrame = animation.CurrentFrame;
            transparentTexture = Content.Load<Texture2D>("gfx/transparency");
            cursorTexture = Content.Load<Texture2D>("gfx/cursors");
            consolas = Content.Load<SpriteFont>("consolas");

            textBoundsX = new TextField(consolas, new Vector2(16, GraphicsDevice.Viewport.Height - 80), animation.Bounds.X.ToString(), "Bounds X:",12, pixelTexture);
            textBoundsY = new TextField(consolas, new Vector2(128, GraphicsDevice.Viewport.Height - 80), animation.Bounds.Y.ToString(), "Bounds Y:", 12, pixelTexture);
            textBoundsWidth = new TextField(consolas, new Vector2(240, GraphicsDevice.Viewport.Height - 80), animation.Bounds.Y.ToString(), "Bounds Width:", 12, pixelTexture);
            textBoundsHeight = new TextField(consolas, new Vector2(352, GraphicsDevice.Viewport.Height - 80), animation.Bounds.Y.ToString(), "Bounds Height:", 12, pixelTexture);
            textTweenFrames = new TextField(consolas, new Vector2(112, 32), "1", "Tween Frames:", 12, pixelTexture);

            cursor = new Cursor(cursorTexture, graphics, currentFrame, consolas);
            cursor.Load();

            deleteFrameButton = new DeleteFrameButton(cursorTexture, new Rectangle(16, 16, 24, 24));
            newFrameButton = new NewFrameButton(cursorTexture, new Rectangle(48, 16, 24, 24));
            tweenButton = new TweenButton(cursorTexture, new Rectangle(80, 16, 24, 24));

            plusTicksButton = new PlusTicksButton(cursorTexture, new Rectangle(112, 60, 16, 16));
            minusTicksButton = new MinusTicksButton(cursorTexture, new Rectangle(88, 60, 16, 16));

            onionSkinButton = new OnionSkinButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2) - 96, 16, 24, 24));
            prevButton = new PrevButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width/2) - 64, 16, 24, 24));
            pauseButton = new PauseButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2) - 32, 16, 24, 24));
            playButton = new PlayButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2), 16, 24, 24));
            nextButton = new NextButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2) + 32, 16, 24, 24));

            saveButton = new SaveButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width) - 64, 16, 24, 24));
            loadButton = new LoadButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width) - 32, 16, 24, 24));

            Button[] buttons = { prevButton, pauseButton, playButton, nextButton, newFrameButton, deleteFrameButton, plusTicksButton, minusTicksButton, tweenButton, onionSkinButton, saveButton, loadButton};
            buttonPanel = new ButtonPanel(buttons);
            buttonPanel.Load();

            int textureCounter = 0;
            while (textureCounter != -1){textureCounter = LoadPartTextures(textureCounter);}

            partsPalette = new PartsPalette(partTextures, transparentTexture, graphics);
            partsPalette.Load();
        }

        /// <summary>
        /// Adds a texture to the textures array. Returns -1 when there are no more parts textures to load.
        /// </summary>
        /// <param name="textureCounter"></param>
        /// <returns></returns>
        private int LoadPartTextures(int textureCounter)
        {
            try
            {
                partTextures[textureCounter] = Content.Load<Texture2D>("gfx/texPrototype" + textureCounter);
                textureCounter++;
                return textureCounter;
            }
            catch (Microsoft.Xna.Framework.Content.ContentLoadException)
            {
                return -1;
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            currentFrame = animation.CurrentFrame;
            cursor.Frame = currentFrame;
            cursor.Update();
            buttonPanel.Update(cursor);
            if (nextButton.IsClicked)
            {
                animation.NextFrame();
            }
            else if (prevButton.IsClicked)
            {
                animation.PreviousFrame();
            }
            else if (playButton.IsClicked)
            {
                if (currentFrame.CurrentTick == currentFrame.Ticks)
                {
                    animation.NextFrame();
                }
                currentFrame.CurrentTick++;
            }
            else if (plusTicksButton.IsClicked)
            {
                currentFrame.Ticks++;
            }
            else if (minusTicksButton.IsClicked)
            {
                if (currentFrame.Ticks > 1) { currentFrame.Ticks--; } // 1 is the least possible number of ticks for a frame.
            }
            else if (newFrameButton.IsClicked)
            {
                //if (animation.Frames.Length > 1)
                //{
                //    animation.AddFrame(new Frame());
                //}
                //else
                //{
                //    animation.InsertFrame(new Frame());
                //}

                Frame newFrame = currentFrame;
                if (animation.Frames.Length > 1)
                {
                    animation.AddFrame(new Frame(newFrame));
                }
                else
                {
                    animation.InsertFrame(new Frame(newFrame));
                }
            }
            else if (deleteFrameButton.IsClicked)
            {
                if (animation.Frames.Length > 1) { animation.RemoveFrame(currentFrame); } // There will always be at least 1 frame.
            }
            else if (onionSkinButton.IsClicked)
            {
                isOnionSkin = !isOnionSkin;
            }
            else if (tweenButton.IsClicked)
            {
                if (animation.CurrentFrameIndex + 1 <= animation.Frames.Length)
                {
                    TweenFrames(currentFrame, animation.Frames[animation.CurrentFrameIndex + 1]);
                }
            }

            partsPalette.Update(cursor);
            currentFrame.Update(cursor);
            animation.Update(cursor);

            textBoundsX.Update(cursor);
            textBoundsY.Update(cursor);
            textBoundsWidth.Update(cursor);
            textBoundsHeight.Update(cursor);
            textTweenFrames.Update(cursor);

            if (textBoundsX.State == TextState.None && textBoundsX.PrevState == TextState.Edit)
            {
                cursor.BoundsRect = new Rectangle(Int32.Parse(textBoundsX.Text), animation.Bounds.Y, animation.Bounds.Width, animation.Bounds.Height);
                textBoundsX.PrevState = TextState.None;
            }
            else if (textBoundsX.State == TextState.None && textBoundsX.State == TextState.None)
            {
                textBoundsX.Text = cursor.BoundsRect.X.ToString();
            }

            if (textBoundsY.State == TextState.None && textBoundsY.PrevState == TextState.Edit)
            {
                cursor.BoundsRect = new Rectangle(animation.Bounds.X, Int32.Parse(textBoundsY.Text), animation.Bounds.Width, animation.Bounds.Height);
                textBoundsY.PrevState = TextState.None;
            }
            else if (textBoundsY.State == TextState.None && textBoundsY.State == TextState.None)
            {
                textBoundsY.Text = cursor.BoundsRect.Y.ToString();
            }

            if (textBoundsWidth.State == TextState.None && textBoundsWidth.PrevState == TextState.Edit)
            {
                cursor.BoundsRect = new Rectangle(animation.Bounds.X, animation.Bounds.Y, Int32.Parse(textBoundsWidth.Text), animation.Bounds.Height);
                textBoundsWidth.PrevState = TextState.None;
            }
            else if (textBoundsWidth.State == TextState.None && textBoundsWidth.State == TextState.None)
            {
                textBoundsWidth.Text = cursor.BoundsRect.Width.ToString();
            }

            if (textBoundsHeight.State == TextState.None && textBoundsHeight.PrevState == TextState.Edit)
            {
                cursor.BoundsRect = new Rectangle(animation.Bounds.X, animation.Bounds.Y, animation.Bounds.Width, Int32.Parse(textBoundsHeight.Text));
                textBoundsHeight.PrevState = TextState.None;
            }
            else if (textBoundsHeight.State == TextState.None && textBoundsHeight.State == TextState.None)
            {
                textBoundsHeight.Text = cursor.BoundsRect.Height.ToString();
            }

            base.Update(gameTime);
        }

        void TweenFrames(Frame startFrame, Frame endFrame)
        {
            tweenCount = Int32.Parse(textTweenFrames.Text);
            //Frame thisFrame = new Frame();
            for (int i = 1; i < tweenCount; i++)
            {
                Frame thisFrame = new Frame();
                //Frame thisFrame = animation.InsertFrame();
                foreach (Part startPart in startFrame.Parts)
                {
                    foreach (Part endPart in endFrame.Parts)
                    {
                        if (startPart.ID == endPart.ID)
                        {
                            Part newPart = new Part(startPart.ID, startPart.Texture, startPart.SourceRect, endPart.DestRect);
                            newPart.Position = endPart.Position;
                            newPart.Rotation = endPart.Rotation;

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
                                newPart.Rotation = ((endPart.Rotation - startPart.Rotation) / tweenCount) * i;
                            }
                            thisFrame.AddPart(newPart);
                        }
                    }
                }
                animation.InsertFrame(thisFrame);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            partsPalette.Draw(spriteBatch);
            if (isOnionSkin)
            {
                if (animation.CurrentFrameIndex - 1 > -1)
                {
                    animation.Frames[animation.CurrentFrameIndex - 1].Draw(spriteBatch, Color.White * 0.2f);
                }
            }
            currentFrame.Draw(spriteBatch);
            animation.Draw(spriteBatch);
            spriteBatch.DrawString(consolas, frameName + animation.CurrentFrameIndex, new Vector2(16,48), Color.White);
            spriteBatch.DrawString(consolas, frameTicks + currentFrame.Ticks, new Vector2(16, 64), Color.White);
            if (currentFrame.SelectedPart != null)
            { spriteBatch.DrawString(consolas, "Selected Part ID: " + currentFrame.SelectedPart.ID.ToString(), new Vector2(16, 80), Color.White); }
            spriteBatch.DrawString(consolas, "Cursor: X=" + cursor.MouseState.X.ToString() + " Y=" + cursor.MouseState.Y.ToString(), new Vector2(GraphicsDevice.Viewport.Width - 192, GraphicsDevice.Viewport.Height - 80), Color.White);
            textBoundsX.Draw(spriteBatch);
            textBoundsY.Draw(spriteBatch);
            textBoundsWidth.Draw(spriteBatch);
            textBoundsHeight.Draw(spriteBatch);
            textTweenFrames.Draw(spriteBatch);
            buttonPanel.Draw(spriteBatch);
            cursor.Draw(spriteBatch); // cursor should be drawn after everything else.

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
