using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class TerrainPanel
    {
        public Sprite TPanelBase { get; set; }
        public Sprite TPanelTop { get; set; }
        public Sprite TPanelBottom { get; set; }
        public Sprite MapGrid { get; set; }

        public UIButton TBtnFlat { get; set; }
        public UIButton TBtnInclineL { get; set; }
        public UIButton TBtnInclineR { get; set; }
        private SpriteFont _font;
        private float _imageScale;
        private string _mapLabel, _terrainLabel;
        public TerrainPanel(float imageScale, int screenWidth, int screenHeight, Texture2D editorTexture, Texture2D pixelTexture, SpriteFont font)
        {
            _imageScale = imageScale;

            _font = font;
            _mapLabel = "-level map-";
            _terrainLabel = "-terrain-";

            TPanelBase = new Sprite();
            TPanelBase.Texture = pixelTexture;
            TPanelBase.DestRect = new Rectangle((int)(0 * _imageScale), (int)(0 * _imageScale), (int)(472 * _imageScale), (int)(1080 * _imageScale));
            TPanelBase.Position = new Vector2(0 * _imageScale, 0 * _imageScale);
            TPanelBase.Origin = Vector2.Zero;
            TPanelBase.Scale = 1.0f;
            TPanelBase.Tint = Color.Black;

            TPanelTop = new Sprite();
            TPanelTop.Texture = editorTexture;
            TPanelTop.SourceRect = new Rectangle(0, 0, 480, 864);
            //TPanelTop.DestRect = new Rectangle((int)(0 * _imageScale), (int)(0 * _imageScale), (int) (480 * _imageScale), (int) (864 * _imageScale));
            TPanelTop.Origin = Vector2.Zero;
            TPanelTop.Position = new Vector2(0 * _imageScale, 0 * _imageScale);
            TPanelTop.Scale = 1.0f * _imageScale;

            TPanelBottom = new Sprite();
            TPanelBottom.Texture = editorTexture;
            TPanelBottom.SourceRect = new Rectangle(512, 576, 480, 256);
            //TPanelBottom.DestRect = new Rectangle((int)(0 * _imageScale), (int)(824 * _imageScale), (int)(480 * _imageScale), (int)(256 * _imageScale));
            TPanelBottom.Origin = Vector2.Zero;
            TPanelBottom.Position = new Vector2(0 * _imageScale, 824 * _imageScale);
            TPanelBottom.Scale = 1.0f * _imageScale;

            MapGrid = new Sprite();
            MapGrid.Texture = editorTexture;
            MapGrid.SourceRect = new Rectangle(496, 0, 416, 416);
            MapGrid.Origin = Vector2.Zero;
            MapGrid.Position = new Vector2(32, 128) * _imageScale;
            MapGrid.Scale = 1.0f * _imageScale;

            TBtnFlat = new UIButton(new ProjectClove.Sprite(editorTexture, new Vector2(32, 768) * _imageScale, new Rectangle(928, 160, 128, 128), Color.White, 0f, Vector2.Zero, 1.0f * _imageScale, false));
            TBtnInclineR = new UIButton(new ProjectClove.Sprite(editorTexture, new Vector2(176, 768) * _imageScale, new Rectangle(1072, 160, 128, 128), Color.White, 0f, Vector2.Zero, 1.0f * _imageScale, false));
            TBtnInclineL = new UIButton(new ProjectClove.Sprite(editorTexture, new Vector2(320, 768) * _imageScale, new Rectangle(1216, 160, 128, 128), Color.White, 0f, Vector2.Zero, 1.0f * _imageScale, false));
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            TPanelBase.Draw(spriteBatch, TPanelBase.DestRect);
            TPanelTop.Draw(spriteBatch);
            TPanelBottom.Draw(spriteBatch);
            MapGrid.Draw(spriteBatch);
            spriteBatch.DrawString(_font, _mapLabel, new Vector2(80, 64) * _imageScale, Color.White, 0f, Vector2.Zero, 2 * _imageScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(_font, _terrainLabel, new Vector2(112, 688) * _imageScale, Color.White, 0f, Vector2.Zero, 2 * _imageScale, SpriteEffects.None, 0f);
            TBtnFlat.Draw(spriteBatch);
            TBtnInclineR.Draw(spriteBatch);
            TBtnInclineL.Draw(spriteBatch);
        }
    }
}
