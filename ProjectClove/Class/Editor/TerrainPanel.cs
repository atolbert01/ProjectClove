using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class TerrainPanel
    {
        public Sprite Fill { get; set; }
        public Sprite PanelSurface_Top { get; set; }
        public Sprite PanelSurface_Bottom { get; set; }
        public Sprite MapGrid { get; set; }
        public UIButton TBtnFlat { get; set; }
        public UIButton TBtnInclineL { get; set; }
        public UIButton TBtnInclineR { get; set; }
        public UIButton NavLeft_LevelName { get; set; }
        public UIButton NavRight_LevelName { get; set; }
        public UIButton NavLeft_TerrainType { get; set; }
        public UIButton NavRight_TerrainType { get; set; }
        public UITextBox LevelNameField { get; set; }
        public UITextBox TerrainTypeField { get; set; }

        private SpriteFont _font;
        private float _imageScale;
        private string _mapLabel, _terrainLabel;
        public TerrainPanel(float imageScale, Texture2D editorTexture, Texture2D pixelTexture, SpriteFont font)
        {
            _imageScale = imageScale;

            _font = font;
            _mapLabel = "-level map-";
            _terrainLabel = "-terrain-";

            Fill = new Sprite();
            Fill.Texture = pixelTexture;
            Fill.DestRect = new Rectangle((int)(0 * _imageScale), (int)(0 * _imageScale), (int)(472 * _imageScale), (int)(1080 * _imageScale));
            Fill.Position = new Vector2(0 * _imageScale, 0 * _imageScale);
            Fill.Origin = Vector2.Zero;
            Fill.Scale = 1.0f;
            Fill.Tint = Color.Black;

            PanelSurface_Top = new Sprite(editorTexture, new Vector2(0 * _imageScale, 0 * _imageScale), new Rectangle(0, 0, 480, 864), Color.White, 0f, Vector2.Zero, _imageScale, false);
            PanelSurface_Bottom = new Sprite(editorTexture, new Vector2(0, 824) * _imageScale, new Rectangle(512, 576, 480, 256), Color.White, 0f, Vector2.Zero, _imageScale, false);
            MapGrid = new Sprite(editorTexture, new Vector2(32, 128) * _imageScale, new Rectangle(496, 0, 416, 416), Color.White, 0f, Vector2.Zero, _imageScale, false);

            TBtnFlat = new UIButton(editorTexture, new Vector2(32, 768) * _imageScale, new Rectangle(928, 160, 128, 128), Color.White, 0f, Vector2.Zero, 1.0f * _imageScale, false);
            TBtnInclineR = new UIButton(editorTexture, new Vector2(176, 768) * _imageScale, new Rectangle(1072, 160, 128, 128), Color.White, 0f, Vector2.Zero, 1.0f * _imageScale, false);
            TBtnInclineL = new UIButton(editorTexture, new Vector2(320, 768) * _imageScale, new Rectangle(1216, 160, 128, 128), Color.White, 0f, Vector2.Zero, 1.0f * _imageScale, false);

            LevelNameField = new UITextBox(editorTexture, new Vector2(48, 576) * _imageScale, new Rectangle(512, 448, 368, 64), Color.White, 0f, Vector2.Zero, 1.0f * _imageScale, false);
            NavLeft_LevelName = new UIButton(editorTexture, new Vector2(16, 586) * _imageScale, new Rectangle(561, 528, 32, 48), Color.White, 0f, Vector2.Zero, 1.0f * _imageScale, false);
            NavRight_LevelName = new UIButton(editorTexture, new Vector2(416, 586) * _imageScale, new Rectangle(624, 528, 32, 48), Color.White, 0f, Vector2.Zero, 1.0f * _imageScale, false);

            TerrainTypeField = new UITextBox(editorTexture, new Vector2(48, 912) * _imageScale, new Rectangle(512, 448, 368, 64), Color.White, 0f, Vector2.Zero, 1.0f * _imageScale, false);
            NavLeft_TerrainType = new UIButton(editorTexture, new Vector2(16, 920) * _imageScale, new Rectangle(561, 528, 32, 48), Color.White, 0f, Vector2.Zero, 1.0f * _imageScale, false);
            NavRight_TerrainType = new UIButton(editorTexture, new Vector2(416, 920) * _imageScale, new Rectangle(624, 528, 32, 48), Color.White, 0f, Vector2.Zero, 1.0f * _imageScale, false);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Fill.Draw(spriteBatch, Fill.DestRect);
            PanelSurface_Top.Draw(spriteBatch);
            PanelSurface_Bottom.Draw(spriteBatch);
            MapGrid.Draw(spriteBatch);
            spriteBatch.DrawString(_font, _mapLabel, new Vector2(80, 64) * _imageScale, Color.White, 0f, Vector2.Zero, 2 * _imageScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(_font, _terrainLabel, new Vector2(112, 688) * _imageScale, Color.White, 0f, Vector2.Zero, 2 * _imageScale, SpriteEffects.None, 0f);
            TBtnFlat.Draw(spriteBatch);
            TBtnInclineR.Draw(spriteBatch);
            TBtnInclineL.Draw(spriteBatch);
            LevelNameField.Draw(spriteBatch);
            TerrainTypeField.Draw(spriteBatch);
            NavLeft_LevelName.Draw(spriteBatch);
            NavRight_LevelName.Draw(spriteBatch);
            NavLeft_TerrainType.Draw(spriteBatch);
            NavRight_TerrainType.Draw(spriteBatch);
        }
    }
}
