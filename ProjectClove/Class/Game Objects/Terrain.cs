using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class Terrain : Sprite
    {
        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)Position.X - Texture.Width / 2, (int)Position.Y - Texture.Height / 2, Texture.Width, Texture.Height); }
        }
        public Terrain(Texture2D texture, Vector2 position, Rectangle sourceRect, float imageScale) : base(texture, position, sourceRect, Color.White, 0f, new Vector2(), imageScale, false)
        {
        }
    }
}
