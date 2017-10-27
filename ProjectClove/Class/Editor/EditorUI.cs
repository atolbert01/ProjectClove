using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class EditorUI
    {
        public TerrainPanel TPanel;
        public EditorUI(float imageScale, int screenWidth, int screenHeight, Texture2D editorTexture)
        {
            TPanel = new TerrainPanel(imageScale, screenWidth, screenHeight, editorTexture);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TPanel.Draw(spriteBatch);
        }
    }
}
