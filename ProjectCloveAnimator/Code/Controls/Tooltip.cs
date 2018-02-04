using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectCloveAnimator
{
    class ToolTip
    {
        private Texture2D pixelTex;
        public string Text;
        private int bgX, bgY;
        private Vector2 location;
        private SpriteFont font;
        public ToolTip(Texture2D pixelTex, string text, SpriteFont font)
        {
            this.pixelTex = pixelTex;
            this.font = font;
            Text = text;
        }

        public void Update(Vector2 loc)
        {
            location = loc;
            bgX = (int)loc.X;
            bgY = (int)loc.Y;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixelTex, new Rectangle(bgX - 9, bgY - 9, ((int)font.MeasureString(Text).X) + 18, ((int)font.MeasureString(Text).Y) + 18), Color.LightSteelBlue * 0.75f);
            spriteBatch.Draw(pixelTex, new Rectangle(bgX - 8, bgY - 8, ((int)font.MeasureString(Text).X) + 16, ((int)font.MeasureString(Text).Y) + 16), Color.DimGray);
            spriteBatch.DrawString(font, Text, new Vector2(location.X, location.Y), Color.White);
        }
    }
}
