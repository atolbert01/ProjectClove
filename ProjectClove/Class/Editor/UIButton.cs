using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class UIButton
    {
        public Sprite Face { get; set; }
        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)Position.X - Face.Texture.Width / 2, (int)Position.Y - Face.Texture.Height / 2, Face.Texture.Width, Face.Texture.Height); }
        }
        public Vector2 Position { get; set; }
        public UIButton(Sprite face)
        {
            Face = face;
            Position = Face.Position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Face.Draw(spriteBatch);
        }
    }
}
