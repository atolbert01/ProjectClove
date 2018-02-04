using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class ObjectPanel
    {
        public UIButton EnemyGroup { get; set; }
        public UIButton ItemGroup { get; set; }
        public UIButton ObstacleGroup { get; set; } // Spikes, traps
        public UIButton DecorationGroup { get; set; } // Decorations and platforms
        public Sprite PanelSurface { get; set; }
        private SpriteFont _font;
        private Sprite[] _UISprites;
        private Label[] _UILabels;
        private float _imageScale;
        private Vector2 _camDims;
        private Camera2D _camera;
        public ObjectPanel(float imageScale, Texture2D editorTexture, Texture2D pixelTexture, SpriteFont font, Camera2D camera)
        {
            _imageScale = imageScale;
            _font = font;
            _camera = camera;
            _camDims = new Vector2((int)(_camera.BoundingBox.Width * 0.5), (int)(_camera.BoundingBox.Height * 0.5));
            PanelSurface = new Sprite(editorTexture, new Vector2(0, 1080 - 216) * _imageScale, new Rectangle(0, 1080 - 216, 1920, 216), Color.White, 0f, Vector2.Zero, _imageScale, false);

            _UISprites = new Sprite[] { PanelSurface };
        }

        public void Move(Vector2 direction)
        {
            foreach (Sprite s in _UISprites)
            {
                s.Position += direction;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite s in _UISprites)
            {
                s.Draw(spriteBatch, _camera.Position - _camDims);
            }
        }
    }
}
