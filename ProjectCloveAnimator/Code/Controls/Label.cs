using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectCloveAnimator
{
    class Label
    {
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public Color LabelColor { get; set; }
        private SpriteFont font;
        public Label(SpriteFont font, string text, Vector2 position, Color labelColor)
        {
            this.font = font;
            Text = text;
            Position = position;
            LabelColor = labelColor;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, Text, Position, LabelColor);
        }
    }
}
