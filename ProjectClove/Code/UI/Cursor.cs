using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectClove
{
    /// <summary>
    /// Interacts with UI elements.     
    /// </summary>
    class Cursor
    {
        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; set; }
        public Rectangle SourceRect { get; set; }
        public MouseState MState { get; set; }
        public MouseState PrevMState { get; set; }
        public Color Tint { get; set; }
        public TextureAtlas Sprites { get; set; }
        public bool IsClicked { get; set; }

        public Cursor(TextureAtlas sprites)
        {
            Sprites = sprites;
            Tint = Color.White;
        }
        public void Update()
        {
            if (MState != null)
            {
                PrevMState = MState;
            }
            MState = Mouse.GetState();
            Position = new Vector2(MState.X, MState.Y);
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, 16, 16);
        }

        public virtual void Execute(){}

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprites.Texture, Position, Sprites.SourceRects[0], Color.White, 0f, new Vector2(0, 0), 1, SpriteEffects.None, 0f);
        }
    }

    class ArrowTool : Cursor
    {
        public ArrowTool(TextureAtlas sprites) : base(sprites)
        {
            SourceRect = new Rectangle(0, 0, 16, 16);
        }

        public override void Execute()
        {
            base.Execute();
        }
    }
}
