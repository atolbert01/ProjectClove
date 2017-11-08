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
        private float _imageScale;
        public ObjectPanel(float imageScale, Texture2D editorTexture, Texture2D pixelTexture, SpriteFont font)
        {
            _imageScale = imageScale;
            _font = font;
            PanelSurface = new Sprite(editorTexture, new Vector2(0, 1080 - 216) * _imageScale, new Rectangle(0, 1080 - 216, 1920, 216), Color.White, 0f, Vector2.Zero, _imageScale, false);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PanelSurface.Draw(spriteBatch);
        }
    }
}
