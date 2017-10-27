using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class TerrainPanel
    {
        public Sprite TPanelTop { get; set; }
        public Sprite TPanelBottom { get; set; }
        public TerrainPanel(float imageScale, int screenWidth, int screenHeight, Texture2D editorPanel)
        {
            TPanelTop = new Sprite();
            TPanelTop.Texture = editorPanel;
            TPanelTop.SourceRect = new Rectangle(0, 0, 480, 864);
            TPanelTop.DestRect = new Rectangle((int)(0 * imageScale), (int)(0 * imageScale), (int) (480 * imageScale), (int) (864 * imageScale));
            TPanelTop.Position = new Vector2(0 * imageScale, 0 * imageScale);
            TPanelTop.Scale = 1.0f * imageScale;

            TPanelBottom = new Sprite();
            TPanelBottom.Texture = editorPanel;
            TPanelBottom.SourceRect = new Rectangle(512, 576, 480, 256);
            TPanelBottom.DestRect = new Rectangle((int)(0 * imageScale), (int)(824 * imageScale), (int)(480 * imageScale), (int)(256 * imageScale));
            TPanelBottom.Position = new Vector2(0 * imageScale, 824 * imageScale);
            TPanelBottom.Scale = 1.0f * imageScale;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            TPanelTop.Draw(spriteBatch);
            TPanelBottom.Draw(spriteBatch);
        }
    }
}
