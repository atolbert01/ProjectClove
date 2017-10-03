using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace ProjectClove
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level[] levels;
        Level currentLevel;
        Player player;
        Player player2;

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
            // TODO: Add your initialization logic here
            levels = new Level[1];
            levels[0] = new Level();
            currentLevel = levels[0];
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
            // TODO: use this.Content to load your game content here
            Texture2D man1Tex = Content.Load<Texture2D>("gfx/man1");
            player = new Player(ParseAnimFiles("man2", man1Tex), new Vector2(100, 360));
            player2 = new Player(ParseAnimFiles("man2", man1Tex), new Vector2(180, 360));
            currentLevel.Rooms.Add(0, new Room(0));
            currentLevel.CurrentRoom = currentLevel.Rooms[0];
            currentLevel.CurrentRoom.Layers = new Layer[2];
            currentLevel.CurrentRoom.Layers[0] = new Layer();
            currentLevel.CurrentRoom.Layers[1] = new Layer();
            currentLevel.CurrentRoom.Layers[0].Actors = new GameActor[1];
            currentLevel.CurrentRoom.Layers[0].Actors[0] = player;
            currentLevel.CurrentRoom.Layers[1].Actors = new GameActor[1];
            currentLevel.CurrentRoom.Layers[1].Actors[0] = player2;
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

            currentLevel.Update(gameTime);
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

            currentLevel.Draw(spriteBatch);

            base.Draw(gameTime);
            spriteBatch.End();
        }

        /// <summary>
        /// Reads in anim file data and returns Animation[]
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="texture"></param>
        /// <returns></returns>
        Animation[] ParseAnimFiles(string groupName, Texture2D texture)
        {
            BinaryReader file = new BinaryReader(File.Open(@"Content/data/" + groupName + ".anim", FileMode.Open));

            Animation[] animations = new Animation[file.ReadInt32()];
            for (int i = 0; i < animations.Length; i++)
            {
                Animation newAnim = new Animation();
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

                        //newPart.Texture = partTextures[file.ReadInt32()];
                        file.ReadInt32();
                        newPart.Texture = texture;

                        //newPart.TexID = Int32.Parse(newPart.Texture.Name.Substring(newPart.Texture.Name.Length - 1, 1));
                        newPart.SourceRect = new Rectangle(file.ReadInt32(), file.ReadInt32(), file.ReadInt32(), file.ReadInt32());
                        newPart.DestRect = new Rectangle(file.ReadInt32(), file.ReadInt32(), file.ReadInt32(), file.ReadInt32());
                        newFrame.Parts[p] = newPart;
                    }
                    newAnim.Frames[j] = newFrame;
                }
                animations[i] = newAnim;
            }
            file.Close();
            return animations;
        }
    }
}
