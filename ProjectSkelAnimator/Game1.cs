using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

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
        ButtonPanel eyeButtonPanel;
        PlayButton playButton;
        PauseButton pauseButton;
        NextButton nextButton;
        PrevButton prevButton;
        NewFrameButton newFrameButton;
        DeleteFrameButton deleteFrameButton;
        PlusTicksButton plusTicksButton, addAnimButton;
        OnionSkinButton onionSkinButton;
        TweenButton tweenButton;
        SaveButton saveButton;
        LoadButton loadButton;
        MinusTicksButton minusTicksButton, removeAnimButton;
        UpButton animUpButton;
        DownButton animDownButton;
        LeftButton paletteLeftButton;
        RightButton paletteRightButton;
        AnimationGroup animGroup;
        Animation currentAnimation;
        TextField[] animGroupTextFields;
        TextField[] textFields;
        TextField textBoundsX;
        TextField textBoundsY;
        TextField textBoundsWidth;
        TextField textBoundsHeight;
        TextField textTweenFrames;
        TextField textAnimGroupName;
        TextField textScript;
        KeyboardState KeyState;
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

            animGroup = new AnimationGroup(new Animation(pixelTexture));
            currentAnimation = animGroup.CurrentAnimation;
            currentAnimation.AddFrame(new Frame());
            currentFrame = currentAnimation.CurrentFrame;

            transparentTexture = Content.Load<Texture2D>("gfx/transparency");
            cursorTexture = Content.Load<Texture2D>("gfx/cursors");
            consolas = Content.Load<SpriteFont>("consolas");
            
            textFields = new TextField[32];

            Vector2 animGroupTextPos = new Vector2(16, 128);
            textAnimGroupName = new TextField(consolas, animGroupTextPos, "", "Animation Group:", 12, pixelTexture);

            for (int i = 0; i < animGroup.Animations.Length; i++)
            {
                Vector2 textPosition = new Vector2(animGroupTextPos.X + 20, 152 + ((consolas.LineSpacing + 2) * (i+ 1)));
                animGroupTextFields = AddNewTextField(animGroupTextFields, textPosition);
            }
            textFields[0] = textBoundsX = new TextField(consolas, new Vector2(16, GraphicsDevice.Viewport.Height - 80), currentAnimation.Bounds.X.ToString(), "Bounds X:", 12, pixelTexture);
            textFields[1] = textBoundsY = new TextField(consolas, new Vector2(128, GraphicsDevice.Viewport.Height - 80), currentAnimation.Bounds.Y.ToString(), "Bounds Y:", 12, pixelTexture);
            textFields[2] = textBoundsWidth = new TextField(consolas, new Vector2(240, GraphicsDevice.Viewport.Height - 80), currentAnimation.Bounds.Y.ToString(), "Bounds Width:", 12, pixelTexture);
            textFields[3] = textBoundsHeight = new TextField(consolas, new Vector2(352, GraphicsDevice.Viewport.Height - 80), currentAnimation.Bounds.Y.ToString(), "Bounds Height:", 12, pixelTexture);
            textFields[4] = textTweenFrames = new TextField(consolas, new Vector2(112, 32), "1", "Tween Frames:", 12, pixelTexture);
            textFields[5] = textScript = new TextField(consolas, new Vector2(GraphicsDevice.Viewport.Width/2 + 128, 32), currentFrame.Script, "Script:" , 120, pixelTexture);

            cursor = new Cursor(cursorTexture, graphics, currentFrame, consolas);
            cursor.Load();

            deleteFrameButton = new DeleteFrameButton(cursorTexture, new Rectangle(16, 16, 24, 24));
            newFrameButton = new NewFrameButton(cursorTexture, new Rectangle(48, 16, 24, 24));
            tweenButton = new TweenButton(cursorTexture, new Rectangle(80, 16, 24, 24));

            plusTicksButton = new PlusTicksButton(cursorTexture, new Rectangle(112, 60, 16, 16));
            minusTicksButton = new MinusTicksButton(cursorTexture, new Rectangle(88, 60, 16, 16));

            addAnimButton = new PlusTicksButton(cursorTexture, new Rectangle(96, 144, 16, 16));
            removeAnimButton = new MinusTicksButton(cursorTexture, new Rectangle(72, 144, 16, 16));
            animUpButton = new UpButton(cursorTexture, new Rectangle( 40, 144, 16, 16));
            animDownButton = new DownButton(cursorTexture, new Rectangle(16, 144, 16, 16));

            paletteLeftButton = new LeftButton(cursorTexture, new Rectangle(0, GraphicsDevice.Viewport.Height - 44, 24, 24));
            paletteRightButton = new RightButton(cursorTexture, new Rectangle(GraphicsDevice.Viewport.Width - 24, GraphicsDevice.Viewport.Height - 44, 24, 24));

            onionSkinButton = new OnionSkinButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2) - 96, 16, 24, 24));
            prevButton = new PrevButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width/2) - 64, 16, 24, 24));
            pauseButton = new PauseButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2) - 32, 16, 24, 24));
            playButton = new PlayButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2), 16, 24, 24));
            nextButton = new NextButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2) + 32, 16, 24, 24));

            saveButton = new SaveButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width) - 64, 16, 24, 24));
            loadButton = new LoadButton(cursorTexture, new Rectangle((graphics.GraphicsDevice.Viewport.Width) - 32, 16, 24, 24));

            Button[] buttons = { prevButton, pauseButton, playButton, nextButton, newFrameButton, deleteFrameButton, plusTicksButton, minusTicksButton, tweenButton, saveButton, onionSkinButton, loadButton, addAnimButton, removeAnimButton, animUpButton, animDownButton, paletteLeftButton, paletteRightButton};
            buttonPanel = new ButtonPanel(buttons);
            buttonPanel.Load();

            LoadEyeButtons();

            int textureCounter = 0;
            while (textureCounter != -1){textureCounter = LoadPartTextures(textureCounter);}

            partsPalette = new PartsPalette(partTextures, transparentTexture, graphics);
            partsPalette.Load();
        }

        void LoadEyeButtons()
        {
            Button[] eyeButtons = new Button[animGroupTextFields.Length];
            for (int i = 0; i < eyeButtons.Length; i++)
            {
                if (animGroupTextFields[i] != null)
                {
                    eyeButtons[i] = new EyeButton(cursorTexture, new Rectangle((int)animGroupTextFields[i].Position.X - 20, (int)animGroupTextFields[i].Position.Y - 1, 16, 16));
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
            KeyState = Keyboard.GetState();
            currentFrame = currentAnimation.CurrentFrame;
            cursor.Frame = currentFrame;
            cursor.Update();
            buttonPanel.Update(cursor);
            eyeButtonPanel.Update(cursor);
            if (nextButton.IsClicked)
            {
                currentAnimation.NextFrame();
                textScript.Text = currentAnimation.Frames[currentAnimation.CurrentFrameIndex].Script;
            }
            else if (prevButton.IsClicked)
            {
                currentAnimation.PreviousFrame();
                textScript.Text = currentAnimation.Frames[currentAnimation.CurrentFrameIndex].Script;
            }
            else if (playButton.IsClicked)
            {
                if (currentFrame.CurrentTick == currentFrame.Ticks)
                {
                    currentAnimation.NextFrame();
                    textScript.Text = currentAnimation.Frames[currentAnimation.CurrentFrameIndex].Script;
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
                Frame newFrame = currentFrame;
                if (currentAnimation.Frames.Length > 1)
                {
                    currentAnimation.AddFrame(new Frame(newFrame));
                    textScript.Text = currentAnimation.Frames[currentAnimation.CurrentFrameIndex].Script;
                }
                else
                {
                    currentAnimation.InsertFrame(new Frame(newFrame));
                    textScript.Text = currentAnimation.Frames[currentAnimation.CurrentFrameIndex].Script;
                }
            }
            else if (deleteFrameButton.IsClicked)
            {
                if (currentAnimation.Frames.Length > 1) { currentAnimation.RemoveFrame(currentFrame); } // There will always be at least 1 frame.
                textScript.Text = currentAnimation.Frames[currentAnimation.CurrentFrameIndex].Script;
            }
            else if (onionSkinButton.IsClicked)
            {
                isOnionSkin = !isOnionSkin;
            }
            else if (tweenButton.IsClicked)
            {
                if (currentAnimation.CurrentFrameIndex + 1 <= currentAnimation.Frames.Length)
                {
                    TweenFrames(currentFrame, currentAnimation.Frames[currentAnimation.CurrentFrameIndex + 1]);
                }
            }

            if (paletteLeftButton.IsClicked)
            {
                partsPalette.Scroll = new Vector2(8, 0);
            }
            else if (paletteRightButton.IsClicked)
            {
                partsPalette.Scroll = new Vector2(-8, 0);
            }
            else
            {
                partsPalette.Scroll = Vector2.Zero;
            }

            if (addAnimButton.IsClicked)
            {
                animGroup.AddAnimation(new Animation(pixelTexture));
                animGroupTextFields = AddNewTextField(animGroupTextFields, new Vector2(36, 152 + ((consolas.LineSpacing + 2) * animGroup.Animations.Length)));
                if (animGroupTextFields[animGroup.Animations.Length - 1] != null)
                {
                    //eyeButtonPanel.Buttons[animGroup.Animations.Length - 1] = new EyeButton(cursorTexture, new Rectangle((int)animGroupTextFields[animGroup.Animations.Length - 1].Position.X - 20, (int)animGroupTextFields[animGroup.Animations.Length - 1].Position.Y - 1, 16, 16));
                    LoadEyeButtons();
                }
                //LoadEyeButtons();
                //eyeButtonPanel.Buttons[animGroup.Animations.Length] = new MinusTicksButton(cursorTexture, new Rectangle((int)animGroupTextFields[animGroup.Animations.Length].Position.X + (int)animGroupTextFields[animGroup.Animations.Length].Bounds.Width, (int)animGroupTextFields[animGroup.Animations.Length].Position.Y - 1, 16, 16));
                //eyeButtonPanel.Load();
            }

            if (removeAnimButton.IsClicked)
            {
                if (animGroup.Animations.Length - 1 > -1)
                {
                    animGroup.RemoveAnimation(animGroup.Animations[animGroup.Animations.Length - 1]);
                    animGroupTextFields = RemoveTextField(animGroupTextFields);
                    LoadEyeButtons();
                }
            }

            if (saveButton.IsClicked)
            {
                SaveAnimation();
            }

            if (loadButton.IsClicked)
            {
                LoadAnimation();
            }

            for (int i = 0; i < animGroup.Animations.Length; i++)
            {
                if (eyeButtonPanel.Buttons[i] != null)
                {
                    if (eyeButtonPanel.Buttons[i].IsClicked)
                    {
                        currentAnimation = animGroup.Animations[i];

                        if (currentAnimation.Frames.Length < 1)
                        {
                            currentAnimation.AddFrame(new Frame());
                        }
                        currentFrame = currentAnimation.CurrentFrame;
                    }
                }
            }

            partsPalette.Update(cursor);
            if (currentFrame != null){ currentFrame.Update(cursor); }
            currentAnimation.Update(cursor);

            foreach (TextField text in animGroupTextFields)
            {
                if (text != null) { text.Update(cursor); }
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
                currentAnimation.Frames[currentAnimation.CurrentFrameIndex].Script = textScript.Text;
                textScript.State = TextState.None;
            }

            if (KeyState.IsKeyDown(Keys.NumPad4))
            {
                foreach (Frame frame in currentAnimation.Frames)
                {
                    foreach (Part part in frame.Parts)
                    {
                        part.Position += new Vector2(-4, 0);
                    }
                }
            }
            else if (KeyState.IsKeyDown(Keys.NumPad6))
            {
                foreach (Frame frame in currentAnimation.Frames)
                {
                    foreach (Part part in frame.Parts)
                    {
                        part.Position += new Vector2(4, 0);
                    }
                }
            }

            if (KeyState.IsKeyDown(Keys.NumPad8))
            {
                foreach (Frame frame in currentAnimation.Frames)
                {
                    foreach (Part part in frame.Parts)
                    {
                        part.Position += new Vector2(0, -4);
                    }
                }
            }
            else if (KeyState.IsKeyDown(Keys.NumPad2))
            {
                foreach (Frame frame in currentAnimation.Frames)
                {
                    foreach (Part part in frame.Parts)
                    {
                        part.Position += new Vector2(0, 4);
                    }
                }
            }

            for (int i = 0; i < animGroup.Animations.Length; i++)
            {
                if (animGroupTextFields[i] != null)
                {
                    if (animGroupTextFields[i].State == TextState.Edit && animGroupTextFields[i].PrevState == TextState.None)
                    {
                        animGroup.Animations[i].AnimationName = animGroupTextFields[i].Text;
                    }
                }
            }
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
                        file.Write(part.Position.X); file.Write(part.Position.Y); // float float
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
                        newPart.Position = new Vector2(file.ReadSingle(), file.ReadSingle());
                        newPart.Scale = file.ReadSingle();
                        newPart.Rotation = file.ReadSingle();
                        newPart.Texture = partTextures[file.ReadInt32()];
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

            animGroupTextFields = new TextField[animGroup.Animations.Length];
            Vector2 animGroupTextPos = new Vector2(16, 128);

            animGroupTextFields = new TextField[0];

            for (int i = 0; i < animGroup.Animations.Length; i++)
            {
                Vector2 textPosition = new Vector2(animGroupTextPos.X + 20, 152 + ((consolas.LineSpacing + 2) * (i + 1)));
                animGroupTextFields = AddNewTextField(animGroupTextFields, textPosition);
            }
            for (int i = 0; i < animGroup.Animations.Length; i++)
            {
                animGroupTextFields[i].Text = animGroup.Animations[i].AnimationName;
            }
            LoadEyeButtons();
        }

        TextField[] AddNewTextField(TextField[] textFields, Vector2 textPosition)
        {
            TextField[] newTextFields;
            TextField newTextField = new TextField(consolas, textPosition, "", 12, pixelTexture);
            if (textFields != null)
            {
                newTextFields = new TextField[textFields.Length + 1];
                for (int i = 0; i < textFields.Length; i++)
                {
                    newTextFields[i] = textFields[i];
                }
            }
            else
            {
                newTextFields = new TextField[1];
            }
            newTextFields[newTextFields.Length - 1] = newTextField;
            return newTextFields;
        }

        TextField[] RemoveTextField(TextField[] textFields)
        {
            if (textFields.Length - 1 > -1)
            {
                TextField[] newTextFields = new TextField[textFields.Length - 1];
                for (int i = 0; i < newTextFields.Length; i++)
                {
                    newTextFields[i] = textFields[i];
                }
                return newTextFields;
            }
            else
            {
                return textFields;
            }
        }

        void TweenFrames(Frame startFrame, Frame endFrame)
        {
            tweenCount = Int32.Parse(textTweenFrames.Text);
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
                currentAnimation.InsertFrame(thisFrame);
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
                if (currentAnimation.CurrentFrameIndex - 1 > -1)
                {
                    currentAnimation.Frames[currentAnimation.CurrentFrameIndex - 1].Draw(spriteBatch, Color.White * 0.2f);
                }
            }
            if (currentFrame != null) { currentFrame.Draw(spriteBatch); }
            currentAnimation.Draw(spriteBatch);

            spriteBatch.DrawString(consolas, frameName + currentAnimation.CurrentFrameIndex, new Vector2(16,48), Color.White);
            if (currentFrame != null) { spriteBatch.DrawString(consolas, frameTicks + currentFrame.Ticks, new Vector2(16, 64), Color.White); }

            if (currentFrame != null)
            { if (currentFrame.SelectedPart != null) { spriteBatch.DrawString(consolas, "Selected Part ID: " + currentFrame.SelectedPart.ID.ToString(), new Vector2(16, 80), Color.White); } }
            spriteBatch.DrawString(consolas, "Cursor: X=" + cursor.MouseState.X.ToString() + " Y=" + cursor.MouseState.Y.ToString(), new Vector2(GraphicsDevice.Viewport.Width - 192, GraphicsDevice.Viewport.Height - 80), Color.White);

            textAnimGroupName.Draw(spriteBatch);

            foreach (TextField text in animGroupTextFields)
            {
                if (text != null) { text.Draw(spriteBatch); }
            }

            foreach (TextField text in textFields)
            {
                if (text != null) { text.Draw(spriteBatch); }
            }
            buttonPanel.Draw(spriteBatch);
            eyeButtonPanel.Draw(spriteBatch);

            cursor.Draw(spriteBatch); // cursor should be drawn after everything else.

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
