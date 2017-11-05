using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class UITextBox : Sprite
    {
        public Sprite Border { get; set; }
        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)Position.X - Border.Texture.Width / 2, (int)Position.Y - Border.Texture.Height / 2, Border.Texture.Width, Border.Texture.Height); }
        }
        public UITextBox(Texture2D texture, Vector2 position, Rectangle sourceRect, Color tint, float rotation, Vector2 origin, float scale, bool isFlipped) : base(texture, position, sourceRect, tint, rotation, origin, scale, isFlipped){ }
    }
}
