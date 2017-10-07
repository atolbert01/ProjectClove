using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    /// <summary>
    /// Sprites are terrain, platforms, backgrounds, UI elements, etc. Objects that have can be represented with a single sprite.
    /// They are also the basic component of an animation frame.
    /// </summary>
    class Sprite
    {
        public int ID { get; set; }
        public bool IsFlipped { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 WorldOrigin { get; set; }
        public Vector2 Position { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }
        public Texture2D Texture { get; set; }
        public int TexID { get; set; }
        public Rectangle SourceRect { get; set; }
        public Rectangle DestRect { get; set; }
        public Color Tint { get; set; }

        public Sprite()
        {
            Tint = Color.White;
        }

        public Sprite(Texture2D texture, Vector2 position, Rectangle sourceRect, Color tint, float rotation, Vector2 origin, float scale, bool isFlipped)
        {
            Texture = texture;
            Position = position;
            SourceRect = sourceRect;
            Tint = tint;
            Rotation = rotation;
            Origin = origin;
            Scale = scale;
            IsFlipped = isFlipped;
        }

        /// <summary>
        /// Use this draw call if the Sprite is stationary.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SourceRect, Tint, Rotation, Origin, Scale, (IsFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None), 0);
        }

        /// <summary>
        /// Use this draw call if the Sprite has movement.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="loc"></param>
        public void Draw(SpriteBatch spriteBatch, Vector2 loc)
        {
            spriteBatch.Draw(Texture, Position + loc, SourceRect, Tint, Rotation, Origin, Scale, (IsFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None), 0);
        }
    }
}
