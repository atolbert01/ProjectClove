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
        MinusTicksButton minusTicksButton;
        Animation animation;
        string frameName = "Frame: ";
        string frameTicks = "Ticks: ";

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

            cursor = new Cursor(cursorTexture, graphics, currentFrame, consolas);
            cursor.Load();

            prevButton = new PrevButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width/2) - 64, 16, 24, 24));
            pauseButton = new PauseButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2) - 32, 16, 24, 24));
            playButton = new PlayButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2), 16, 24, 24));
            nextButton = new NextButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2) + 32, 16, 24, 24));

            newFrameButton = new NewFrameButton(cursorTexture, new Rectangle(16, 16, 24, 24));
            deleteFrameButton = new DeleteFrameButton(cursorTexture, new Rectangle(48, 16, 24, 24));

            plusTicksButton = new PlusTicksButton(cursorTexture, new Rectangle(112, 60, 16, 16));
            minusTicksButton = new MinusTicksButton(cursorTexture, new Rectangle(88, 60, 16, 16));

            Button[] buttons = { prevButton, pauseButton, playButton, nextButton, newFrameButton, deleteFrameButton, plusTicksButton, minusTicksButton};
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
            partsPalette.Update(cursor);
            currentFrame.Update(cursor);
            base.Update(gameTime);
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
            currentFrame.Draw(spriteBatch);
            animation.Draw(spriteBatch);
            spriteBatch.DrawString(consolas, frameName + animation.CurrentFrameIndex, new Vector2(16,48), Color.White);
            spriteBatch.DrawString(consolas, frameTicks + currentFrame.Ticks, new Vector2(16, 64), Color.White);
            if (currentFrame.SelectedPart != null)
            { spriteBatch.DrawString(consolas, "Selected Part ID: " + currentFrame.SelectedPart.ID.ToString(), new Vector2(16, 80), Color.White); }

            buttonPanel.Draw(spriteBatch);
            cursor.Draw(spriteBatch); // cursor should be drawn after everything else.

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
