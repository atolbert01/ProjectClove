using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class EditorUI
    {
        public TerrainPanel TPanel;
        public ObjectPanel OPanel;
        private Level _currentLevel;
        private Texture2D _pixelTexture;
        private float _imageScale;
        private int _screenWidth, _screenHeight, _roomWidthPx, _roomHeightPx;
        private Rectangle[] _horzGridRects;
        private Rectangle[] _vertGridRects;
        private Rectangle[] _boundaryRects;
        public EditorUI(float imageScale, int screenWidth, int screenHeight, Level currentLevel, Texture2D editorTexture, Texture2D pixelTexture, SpriteFont font, Camera2D camera)
        {
            _imageScale = imageScale;
            _pixelTexture = pixelTexture;
            _currentLevel = currentLevel;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _roomWidthPx = screenWidth * _currentLevel.CurrentRoom.Width;
            _roomHeightPx = screenHeight * _currentLevel.CurrentRoom.Height;
            TPanel = new TerrainPanel(imageScale, editorTexture, pixelTexture, font, camera);
            OPanel = new ObjectPanel(imageScale, editorTexture, pixelTexture, font, camera);

            int gridSpace = (int)(90 * _imageScale);
            _horzGridRects = new Rectangle[(int)(_roomWidthPx/(gridSpace)) + 1];
            _vertGridRects = new Rectangle[(int)(_roomHeightPx / (gridSpace)) + 1];
            for (int i = 0; i < _horzGridRects.Length; i++)
            {
                _horzGridRects[i] = new Rectangle(i * gridSpace, 0, 1, _roomHeightPx);
            }
            for (int j = 0; j < _vertGridRects.Length; j++)
            {
                _vertGridRects[j] = new Rectangle(0, j * gridSpace, _roomWidthPx, 1);
            }
            _boundaryRects = new Rectangle[] { new Rectangle(0,-_roomHeightPx,_roomWidthPx,_roomHeightPx), new Rectangle(0, _roomHeightPx, _roomWidthPx, _roomHeightPx), new Rectangle(-_roomWidthPx, -_roomHeightPx, _roomWidthPx, _roomHeightPx * 4), new Rectangle(_roomWidthPx, -_roomHeightPx, _roomWidthPx, _roomHeightPx * 4) };
        }

        public void Update()
        {
            //TPanel.Update();
            //OPanel.Update();
        }

        public void Move(Vector2 direction)
        {
            TPanel.Move(direction);
            OPanel.Move(direction);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Rectangle rect in _horzGridRects)
            {
                spriteBatch.Draw(_pixelTexture, rect, Color.DarkGray * 0.5f);
            }

            foreach (Rectangle rect in _vertGridRects)
            {
                spriteBatch.Draw(_pixelTexture, rect, Color.DarkGray * 0.5f);
            }

            foreach (Rectangle rect in _boundaryRects)
            {
                spriteBatch.Draw(_pixelTexture, rect, Color.Gray);
            }

            OPanel.Draw(spriteBatch);
            TPanel.Draw(spriteBatch);
        }
    }
}
