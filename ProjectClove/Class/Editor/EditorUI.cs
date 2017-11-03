using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class EditorUI
    {
        public TerrainPanel TPanel;
        public EditorUI(float imageScale, int screenWidth, int screenHeight, Texture2D editorTexture, Texture2D pixelTexture, SpriteFont font)
        {
            TPanel = new TerrainPanel(imageScale, screenWidth, screenHeight, editorTexture, pixelTexture, font);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TPanel.Draw(spriteBatch);
        }
    }
}
