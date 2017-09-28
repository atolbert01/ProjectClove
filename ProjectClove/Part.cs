using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class Part
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

        public Part()
        {
            Tint = Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SourceRect, Tint, Rotation, Origin, Scale, (IsFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None), 0);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 loc)
        {
            spriteBatch.Draw(Texture, Position + loc, SourceRect, Tint, Rotation, Origin, Scale, (IsFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None), 0);
        }
    }
}
