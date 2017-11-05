using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class UIButton : Sprite
    {
        public Sprite Face { get; set; }
        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)Position.X - Face.Texture.Width / 2, (int)Position.Y - Face.Texture.Height / 2, Face.Texture.Width, Face.Texture.Height); }
        }
        public UIButton(Texture2D texture, Vector2 position, Rectangle sourceRect, Color tint, float rotation, Vector2 origin, float scale, bool isFlipped) : base(texture, position, sourceRect, tint, rotation, origin, scale, isFlipped){}
    }
}
