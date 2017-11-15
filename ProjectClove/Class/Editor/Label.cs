using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class Label
    {
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        private SpriteFont _font;
        private float _size;
        public Label(string text, Vector2 position, SpriteFont font, float size)
        {
            Text = text;
            Position = position;
            _font = font;
            _size = size;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, Text, Position, Color.White, 0f, Vector2.Zero, _size, SpriteEffects.None, 0f);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 loc)
        {
            spriteBatch.DrawString(_font, Text, Position + loc, Color.White, 0f, Vector2.Zero, _size, SpriteEffects.None, 0f);
        }
    }
}
