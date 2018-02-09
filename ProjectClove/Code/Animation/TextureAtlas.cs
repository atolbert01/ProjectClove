using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class TextureAtlas
    {
        //public Vector2 Position { get; set; }
        public int SpriteIndex { get; set; }
        public int SpriteSize { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }
        private Texture2D texture { get; set; }
        private Rectangle[] sourceRects { get; set; }
        public TextureAtlas(Texture2D texture)
        {
            this.texture = texture;
            sourceRects = SliceTexture(this.texture);
            Scale = 1.0f;
            Rotation = 0f;
        }

        private Rectangle[] SliceTexture(Texture2D texture)
        {
            Rectangle[] slicedRects = new Rectangle[0];
            int spriteCount = 0;
            //int gridSize = 0;
            int columns = 0;
            int rows = 0;

            // TextureAtlases will only be sliceable by a few predefined dimensions
            if (texture.Bounds.Width % 64 == 0 && texture.Bounds.Height % 64 == 0)
            {
                SpriteSize = 64;
            }

            columns = texture.Bounds.Width / SpriteSize;
            rows = texture.Bounds.Height / SpriteSize;
            slicedRects = new Rectangle[columns * rows];
            for (int i = 0; i < texture.Bounds.Height / SpriteSize; i++)
            {
                for (int j = 0; j < texture.Bounds.Width / SpriteSize; j++)
                {
                    slicedRects[spriteCount] = new Rectangle(j * SpriteSize, i * SpriteSize, SpriteSize, SpriteSize);//new SourceRectangleInfo(tex, new Rectangle(j * gridSize, i * gridSize, gridSize, gridSize));
                    spriteCount++;
                }
            }
            return slicedRects;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(texture, position, sourceRects[SpriteIndex], Color.White, Rotation, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);
        }
    }
}
