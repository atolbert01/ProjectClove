using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class EditorUI
    {
        public TerrainPanel TPanel;
        public ObjectPanel OPanel;
        private Texture2D _pixelTexture;
        private float _imageScale;
        private int _screenWidth, _screenHeight;
        public EditorUI(float imageScale, int screenWidth, int screenHeight, Texture2D editorTexture, Texture2D pixelTexture, SpriteFont font)
        {
            _imageScale = imageScale;
            _pixelTexture = pixelTexture;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            TPanel = new TerrainPanel(imageScale, editorTexture, pixelTexture, font);
            OPanel = new ObjectPanel(imageScale, editorTexture, pixelTexture, font);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < _screenWidth; x += (int)(64 * _imageScale))
            {
                spriteBatch.Draw(_pixelTexture, new Rectangle(x, 0, 1, (int)(1080 * _imageScale)), Color.White * 0.5f);
            }
            for (int y = 0; y < _screenHeight; y += (int)(64 * _imageScale))
            {
                spriteBatch.Draw(_pixelTexture, new Rectangle(0, y, (int)(1920 * _imageScale), 1), Color.White * 0.5f);
            }

            OPanel.Draw(spriteBatch);
            TPanel.Draw(spriteBatch);
        }
    }
}
