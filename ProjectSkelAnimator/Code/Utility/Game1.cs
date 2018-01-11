using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace ProjectCloveAnimator
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch spriteBatchUI;
        InputManager input;
        Camera2D camera;
        Matrix camTransform;
        SpriteFont consolas, leelawadee;
        Texture2D[] partTextures;
        Texture2D cursorTexture, transparentTexture, pixelTexture;
        PartsPalette partsPalette;
        Frame currentFrame;
        Cursor cursor;
        ButtonPanel buttonPanel;
        ButtonPanel eyeButtonPanel;
        Button playButton, pauseButton, nextButton, prevButton, newFrameButton, dupAnimButton, deleteFrameButton, 
            plusTicksButton, addAnimButton, onionSkinButton, saveButton, loadButton, minusTicksButton, 
            removeAnimButton, animUpButton, animDownButton, paletteLeftButton, paletteRightButton;
        TweenButton tweenButton;
        AnimationGroup animGroup;
        Animation currentAnimation;
        TextField[] textFields;
        TextField textBoundsX, textBoundsY, textBoundsWidth, textBoundsHeight, textTweenFrames, textAnimGroupName, textScript;
        KeyboardState KeyState, PrevKeyState;
        string frameName = "Frame: ";
        string frameTicks = "Ticks: ";
        bool isOnionSkin = false;
        int groupAnimCount = 0;
        Label mouseCoords;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 720;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            Window.IsBorderless = true;
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
            spriteBatchUI = new SpriteBatch(GraphicsDevice);
            camera = new Camera2D(GraphicsDevice);
            camera.Zoom = 1.0f;

            partTextures = new Texture2D[32];

            pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            pixelTexture.SetData(new Color[] { Color.White });

            transparentTexture = Content.Load<Texture2D>("gfx/transparency");
            cursorTexture = Content.Load<Texture2D>("gfx/cursors");
            consolas = Content.Load<SpriteFont>("consolas");
            leelawadee = Content.Load<SpriteFont>("leelawadee");

            animGroup = new AnimationGroup(new Animation(pixelTexture), consolas, leelawadee);
            currentAnimation = animGroup.CurrentAnimation;
            currentAnimation.AddFrame(new Frame());
            currentFrame = currentAnimation.CurrentFrame;

            textFields = new TextField[32];

            Vector2 animGroupTextPos = new Vector2(16, 128);
            textAnimGroupName = new TextField(consolas, leelawadee, animGroupTextPos, "newgroup", "Animation Group:", 12, pixelTexture);

            for (int i = 0; i < animGroup.Animations.Length; i++)
            {
                Vector2 textPosition = new Vector2(animGroupTextPos.X + 20, 152 + ((consolas.LineSpacing + 2) * (i+ 1)));
                animGroup.AddNewTextField(textPosition, "animation" + animGroup.Animations.Length);
            }

            cursor = new Cursor(cursorTexture, graphics, currentFrame, leelawadee, pixelTexture);
            cursor.Load();

            input = new InputManager(animGroup, camera);

            deleteFrameButton = new DeleteFrameButton(cursorTexture, new Rectangle(16, 16, 24, 24), currentAnimation);
            newFrameButton = new NewFrameButton(cursorTexture, new Rectangle(48, 16, 24, 24), currentAnimation);
            tweenButton = new TweenButton(cursorTexture, new Rectangle(80, 16, 24, 24), currentAnimation);

            plusTicksButton = new PlusTicksButton(cursorTexture, new Rectangle(112, 60, 16, 16), currentAnimation);
            minusTicksButton = new MinusTicksButton(cursorTexture, new Rectangle(88, 60, 16, 16), currentAnimation);

            addAnimButton = new AddAnimButton(cursorTexture, new Rectangle(96, 144, 16, 16), currentAnimation);
            removeAnimButton = new RemoveAnimButton(cursorTexture, new Rectangle(72, 144, 16, 16), currentAnimation);
            dupAnimButton = new DuplicateAnimButton(cursorTexture, new Rectangle(120, 144, 16, 16), currentAnimation);
            animUpButton = new UpButton(cursorTexture, new Rectangle( 40, 144, 16, 16), currentAnimation);
            animDownButton = new DownButton(cursorTexture, new Rectangle(16, 144, 16, 16), currentAnimation);
            
            paletteLeftButton = new PaletteLeftButton(cursorTexture, new Rectangle(0, GraphicsDevice.Viewport.Height - 44, 24, 24), currentAnimation);
            paletteRightButton = new PaletteRightButton(cursorTexture, new Rectangle(GraphicsDevice.Viewport.Width - 24, GraphicsDevice.Viewport.Height - 44, 24, 24), currentAnimation);

            onionSkinButton = new OnionSkinButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2) - 96, 16, 24, 24), currentAnimation);
            prevButton = new PrevButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width/2) - 64, 16, 24, 24), currentAnimation);
            pauseButton = new PauseButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2) - 32, 16, 24, 24), currentAnimation);
            playButton = new PlayButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2), 16, 24, 24), currentAnimation);
            nextButton = new NextButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2) + 32, 16, 24, 24), currentAnimation);

            saveButton = new SaveButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width) - 64, 16, 24, 24), currentAnimation);
            loadButton = new LoadButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width) - 32, 16, 24, 24), currentAnimation);

            Button[] buttons = { prevButton, pauseButton, playButton, nextButton, newFrameButton, deleteFrameButton, plusTicksButton, minusTicksButton, tweenButton, saveButton, onionSkinButton, loadButton, addAnimButton, removeAnimButton, dupAnimButton, animUpButton, animDownButton, paletteLeftButton, paletteRightButton};
            buttonPanel = new ButtonPanel(buttons);
            buttonPanel.Load();

            LoadEyeButtons();

            int textureCounter = 0;
            while (textureCounter != -1){textureCounter = LoadPartTextures(textureCounter);}

            partsPalette = new PartsPalette(partTextures, transparentTexture, graphics);
            partsPalette.Load();

            textFields[0] = textBoundsX = new TextField(consolas, leelawadee, new Vector2(16, GraphicsDevice.Viewport.Height - (partsPalette.GridSize + 16)), currentAnimation.Bounds.X.ToString(), "Bounds X:", 12, pixelTexture);
            textFields[1] = textBoundsY = new TextField(consolas, leelawadee, new Vector2(128, GraphicsDevice.Viewport.Height - (partsPalette.GridSize + 16)), currentAnimation.Bounds.Y.ToString(), "Bounds Y:", 12, pixelTexture);
            textFields[2] = textBoundsWidth = new TextField(consolas, leelawadee, new Vector2(240, GraphicsDevice.Viewport.Height - (partsPalette.GridSize + 16)), currentAnimation.Bounds.Y.ToString(), "Bounds Width:", 12, pixelTexture);
            textFields[3] = textBoundsHeight = new TextField(consolas, leelawadee, new Vector2(352, GraphicsDevice.Viewport.Height - (partsPalette.GridSize + 16)), currentAnimation.Bounds.Y.ToString(), "Bounds Height:", 12, pixelTexture);
            textFields[4] = textTweenFrames = new TextField(consolas, leelawadee, new Vector2(112, 32), "1", "Tween Frames:", 12, pixelTexture);
            textFields[5] = textScript = new TextField(consolas, leelawadee, new Vector2(GraphicsDevice.Viewport.Width / 2 + 128, 32), currentFrame.Script, "Script:", 120, pixelTexture);

            groupAnimCount = animGroup.Animations.Length;
            mouseCoords = new Label(leelawadee, "Mouse Coords: " + cursor.MouseState.X + ", " + cursor.MouseState.Y, new Vector2((graphics.GraphicsDevice.Viewport.Width - 16) - (leelawadee.MeasureString("Mouse Coords: 0000, 0000").X), GraphicsDevice.Viewport.Height - (partsPalette.GridSize + 16)) ,Color.White);
        }

        void LoadEyeButtons()
        {
            Button[] eyeButtons = new Button[animGroup.Animations.Length];
            for (int i = 0; i < eyeButtons.Length; i++)
            {
                if (animGroup.NameFields[i] != null)
                {
                    eyeButtons[i] = new EyeButton(cursorTexture, new Rectangle((int)animGroup.NameFields[i].Position.X - 20, (int)animGroup.NameFields[i].Position.Y - 1, 16, 16), currentAnimation);
                }
            }
            eyeButtonPanel = new ButtonPanel(eyeButtons);
            eyeButtonPanel.Load();
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
                partTextures[textureCounter] = Content.Load<Texture2D>("gfx/tex" + textureCounter);
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
            camTransform = camera.Get_Transformation();
            KeyState = Keyboard.GetState();
            animGroup.Update(cursor);
            currentAnimation = animGroup.CurrentAnimation;
            currentFrame = currentAnimation.CurrentFrame;
            cursor.Frame = currentFrame;
            cursor.Update(camera.Transform);
            buttonPanel.Update(cursor, animGroup);
            eyeButtonPanel.Update(cursor, animGroup);
            input.Update(cursor, animGroup, camera);
            partsPalette.Update(cursor);
            if (currentFrame != null) { currentFrame.Update(cursor); }
            currentAnimation.Update(cursor);
            mouseCoords.Text = "Mouse Coords: " + cursor.MouseState.X + ", " + cursor.MouseState.Y;

            if (onionSkinButton.IsClicked) { isOnionSkin = !isOnionSkin; }
            if (tweenButton.IsClicked) { tweenButton.TweenCount = Int32.Parse(textTweenFrames.Text); }
            if (saveButton.IsClicked) { SaveAnimation(); }
            if (loadButton.IsClicked) { LoadAnimation(); }

            if (paletteLeftButton.IsClicked) { partsPalette.Scroll = new Vector2(8, 0); }
            else if (paletteRightButton.IsClicked) { partsPalette.Scroll = new Vector2(-8, 0); }
            else { partsPalette.Scroll = Vector2.Zero; }

            // Did we add or remove an animation from the group between updates?
            if (animGroup.Animations.Length != groupAnimCount)
            {
                if (animGroup.Animations.Length > 0)
                {
                    if (animGroup.NameFields[animGroup.Animations.Length - 1] != null)
                    {
                        groupAnimCount = animGroup.Animations.Length;
                        LoadEyeButtons();
                    }
                }
                else
                {
                    groupAnimCount = 0;
                    LoadEyeButtons();
                }
            }

            for (int i = 0; i < animGroup.Animations.Length; i++)
            {
                if (eyeButtonPanel.Buttons[i] != null)
                {
                    if (eyeButtonPanel.Buttons[i].IsClicked)
                    {
                        currentAnimation = animGroup.Animations[i];

                        if (currentAnimation.Frames.Length < 1 || currentAnimation.Frames == null)
                        {
                            currentAnimation.AddFrame(new Frame());
                        }
                        currentFrame = currentAnimation.CurrentFrame;
                        animGroup.CurrentAnimation = currentAnimation;
                    }
                }
            }
            
            foreach (TextField text in textFields)
            {
                if (text != null) { text.Update(cursor); }
            }

            textAnimGroupName.Update(cursor);

            if (textBoundsX.State == TextState.None && textBoundsX.PrevState == TextState.Edit)
            { 
                currentAnimation.Bounds = new Rectangle(Int32.Parse(textBoundsX.Text), currentAnimation.Bounds.Y, currentAnimation.Bounds.Width, currentAnimation.Bounds.Height);
                textBoundsX.PrevState = TextState.None;
            }
            else if (textBoundsX.State == TextState.None && textBoundsX.State == TextState.None)
            {
                textBoundsX.Text = currentAnimation.Bounds.X.ToString();
            }

            if (textBoundsY.State == TextState.None && textBoundsY.PrevState == TextState.Edit)
            {
                currentAnimation.Bounds = new Rectangle(currentAnimation.Bounds.X, Int32.Parse(textBoundsY.Text), currentAnimation.Bounds.Width, currentAnimation.Bounds.Height);
                textBoundsY.PrevState = TextState.None;
            }
            else if (textBoundsY.State == TextState.None && textBoundsY.State == TextState.None)
            {
                textBoundsY.Text = currentAnimation.Bounds.Y.ToString();
            }

            if (textBoundsWidth.State == TextState.None && textBoundsWidth.PrevState == TextState.Edit)
            {
                currentAnimation.Bounds = new Rectangle(currentAnimation.Bounds.X, currentAnimation.Bounds.Y, Int32.Parse(textBoundsWidth.Text), currentAnimation.Bounds.Height);
                textBoundsWidth.PrevState = TextState.None;
            }
            else if (textBoundsWidth.State == TextState.None && textBoundsWidth.State == TextState.None)
            {
                textBoundsWidth.Text = currentAnimation.Bounds.Width.ToString();
            }

            if (textBoundsHeight.State == TextState.None && textBoundsHeight.PrevState == TextState.Edit)
            {
                currentAnimation.Bounds = new Rectangle(currentAnimation.Bounds.X, currentAnimation.Bounds.Y, currentAnimation.Bounds.Width, Int32.Parse(textBoundsHeight.Text));
                textBoundsHeight.PrevState = TextState.None;
            }
            else if (textBoundsHeight.State == TextState.None && textBoundsHeight.State == TextState.None)
            {
                textBoundsHeight.Text = currentAnimation.Bounds.Height.ToString();
            }

            if (textAnimGroupName.State == TextState.Edit && textAnimGroupName.PrevState == TextState.None)
            {
                animGroup.GroupName = textAnimGroupName.Text;
            }

            if (textScript.State == TextState.None && textScript.PrevState == TextState.Edit)
            {
                currentAnimation.CurrentFrame.Script = textScript.Text;
                textScript.State = TextState.None;
            }
            else if (textScript.State == TextState.Edit && textScript.State == TextState.Edit)
            {
                currentAnimation.CurrentFrame.Script = textScript.Text;
            }
            else if (textScript.State == TextState.None && textScript.PrevState == TextState.None)
            {
                if (currentAnimation.CurrentFrame != null)
                {
                    // We are not editing the field so the text should be the current frame's script
                    textScript.Text = currentAnimation.CurrentFrame.Script;
                }
            }
            if (KeyState != null) { PrevKeyState = KeyState; }
            base.Update(gameTime);
        }

        void SaveAnimation()
        {
            BinaryWriter file = new BinaryWriter(File.Open(@"Content/data/" + animGroup.GroupName + ".anim", FileMode.Create));

            file.Write(animGroup.Animations.Length); // int

            foreach (Animation anim in animGroup.Animations)
            {
                file.Write(anim.AnimationName); // string
                file.Write(anim.Bounds.X); file.Write(anim.Bounds.Y); file.Write(anim.Bounds.Width); file.Write(anim.Bounds.Height); // int int int int

                file.Write(anim.Frames.Length); // int

                for (int i = 0; i < anim.Frames.Length; i++)
                {
                    //file.Write(i); // int
                    file.Write(anim.Frames[i].Ticks); // int
                    file.Write(anim.Frames[i].Parts.Length); // int
                    foreach (Part part in anim.Frames[i].Parts)
                    {
                        file.Write(part.ID); // int
                        file.Write(part.IsFlipped); // bool
                        file.Write(part.Origin.X); file.Write(part.Origin.Y); // float float
                        file.Write(part.WorldOrigin.X); file.Write(part.WorldOrigin.Y); // float float
                        //file.Write(part.Position.X); file.Write(part.Position.Y); // float float
                        file.Write(part.Position.X - anim.Bounds.Center.X); file.Write(part.Position.Y - anim.Bounds.Center.Y); // float float
                        file.Write(part.Scale); // float
                        file.Write(part.Rotation); // float
                        file.Write(part.TexID); // int
                        file.Write(part.SourceRect.X); file.Write(part.SourceRect.Y); file.Write(part.SourceRect.Width); file.Write(part.SourceRect.Height); // int int int int
                        file.Write(part.DestRect.X); file.Write(part.DestRect.Y); file.Write(part.DestRect.Width); file.Write(part.DestRect.Height); // int int int int
                    }
                }
            }
            file.Close();
        }

        void LoadAnimation()
        {
            BinaryReader file = new BinaryReader(File.Open(@"Content/data/" + animGroup.GroupName + ".anim", FileMode.Open));

            AnimationGroup newAnimGroup = new AnimationGroup();
            newAnimGroup.GroupName = animGroup.GroupName;
            newAnimGroup.Animations = new Animation[file.ReadInt32()];
            for (int i = 0; i < newAnimGroup.Animations.Length; i++)
            {
                Animation newAnim = new Animation(pixelTexture);
                newAnim.AnimationName = file.ReadString();
                newAnim.Bounds = new Rectangle(file.ReadInt32(), file.ReadInt32(), file.ReadInt32(), file.ReadInt32());
                newAnim.Frames = new Frame[file.ReadInt32()];

                for (int j = 0; j < newAnim.Frames.Length; j++)
                {
                    Frame newFrame = new Frame();
                    newFrame.Ticks = file.ReadInt32();
                    newFrame.Parts = new Part[file.ReadInt32()];

                    for (int p = 0; p < newFrame.Parts.Length; p++)
                    {
                        Part newPart = new Part();
                        newPart.ID = file.ReadInt32();
                        newPart.IsFlipped = file.ReadBoolean();
                        newPart.Origin = new Vector2(file.ReadSingle(), file.ReadSingle());
                        newPart.WorldOrigin = new Vector2(file.ReadSingle(), file.ReadSingle());
                        newPart.Position = new Vector2(newAnim.Bounds.Center.X + file.ReadSingle(), newAnim.Bounds.Center.Y + file.ReadSingle());
                        newPart.Scale = file.ReadSingle();
                        newPart.Rotation = file.ReadSingle();
                        newPart.Texture = partTextures[file.ReadInt32()];
                        newPart.TexID = Int32.Parse(newPart.Texture.Name.Substring(newPart.Texture.Name.Length - 1, 1));
                        newPart.SourceRect = new Rectangle(file.ReadInt32(), file.ReadInt32(), file.ReadInt32(), file.ReadInt32());
                        newPart.DestRect = new Rectangle(file.ReadInt32(), file.ReadInt32(), file.ReadInt32(), file.ReadInt32());
                        newFrame.Parts[p] = newPart;
                    }
                    newAnim.Frames[j] = newFrame;
                }
                newAnimGroup.Animations[i] = newAnim;
            }
            file.Close();
            ResetAnimGroup(newAnimGroup);
        }

        void ResetAnimGroup(AnimationGroup newAnimGroup)
        {
            animGroup = newAnimGroup;
            animGroup.CurrentAnimation = newAnimGroup.Animations[0];

            foreach (Animation anim in animGroup.Animations)
            {
                anim.CurrentFrame = anim.Frames[0];
            }
            currentAnimation = animGroup.CurrentAnimation;
            currentFrame = animGroup.CurrentAnimation.CurrentFrame;
            currentAnimation.CurrentFrameIndex = 0;

            animGroup.NameFields = new TextField[animGroup.Animations.Length];
            Vector2 animGroupTextPos = new Vector2(16, 128);

            animGroup.NameFields = new TextField[0];
            animGroup.TextFont = consolas;
            animGroup.LabelFont = leelawadee;

            for (int i = 0; i < animGroup.Animations.Length; i++)
            {
                Vector2 textPosition = new Vector2(animGroupTextPos.X + 20, 152 + ((consolas.LineSpacing + 2) * (i + 1)));
                animGroup.AddNewTextField(textPosition, animGroup.Animations[i].AnimationName);
            }
            for (int i = 0; i < animGroup.Animations.Length; i++)
            {
                animGroup.NameFields[i].Text = animGroup.Animations[i].AnimationName;
            }
            LoadEyeButtons();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSlateGray);

            ///////////////////////////////////////////
            ///                                     ///
            ///     FIRST SPRITEBATCH DRAW CALL     ///
            ///                                     ///
            ///////////////////////////////////////////
            
            // This call is for the Animation itself

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, null, null, null, camTransform);
            base.Draw(gameTime);
            if (isOnionSkin)
            {
                if (currentAnimation.CurrentFrameIndex - 1 > -1)
                {
                    currentAnimation.Frames[currentAnimation.CurrentFrameIndex - 1].Draw(spriteBatch, Color.White * 0.2f);
                }
            }
            if (currentFrame != null) { currentFrame.Draw(spriteBatch); }
            currentAnimation.Draw(spriteBatch);
            spriteBatch.End();

            ///////////////////////////////////////////
            ///                                     ///
            ///     SECOND SPRITEBATCH DRAW CALL    ///
            ///                                     ///
            ///////////////////////////////////////////

            // This call is for the UI elements and things we don't need to apply a camera transform Matrix to

            spriteBatch.Begin();
            partsPalette.Draw(spriteBatch);

            spriteBatch.DrawString(consolas, frameName + currentAnimation.CurrentFrameIndex, new Vector2(16, 48), Color.White);
            if (currentFrame != null) { spriteBatch.DrawString(consolas, frameTicks + currentFrame.Ticks, new Vector2(16, 64), Color.White); }

            if (currentFrame != null)
            { if (currentFrame.SelectedPart != null) { spriteBatch.DrawString(consolas, "Selected Part ID: " + currentFrame.SelectedPart.ID.ToString(), new Vector2(16, 80), Color.White); } }
            spriteBatch.DrawString(consolas, "Cursor: X=" + cursor.MouseState.X.ToString() + " Y=" + cursor.MouseState.Y.ToString(), new Vector2(GraphicsDevice.Viewport.Width - 192, GraphicsDevice.Viewport.Height - 80), Color.White);

            textAnimGroupName.Draw(spriteBatch);

            foreach (TextField text in animGroup.NameFields)
            {
                if (text != null) { text.Draw(spriteBatch); }
            }

            foreach (TextField text in textFields)
            {
                if (text != null) { text.Draw(spriteBatch); }
            }
            buttonPanel.Draw(spriteBatch);
            eyeButtonPanel.Draw(spriteBatch);

            mouseCoords.Draw(spriteBatch);
            cursor.Draw(spriteBatch); // cursor should be drawn after everything else.
            spriteBatch.End();
        }
    }
}
