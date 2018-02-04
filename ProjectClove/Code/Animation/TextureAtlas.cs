using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class TextureAtlas
    {
        public enum SpriteIndex { }
        public Texture2D Texture { get; set; }
        public Rectangle[] SourceRects { get; set; }
        public TextureAtlas(Texture2D texture)
        {
            Texture = texture;
            SourceRects = SliceTexture(Texture);
        }

        private Rectangle[] SliceTexture(Texture2D texture)
        {
            Rectangle[] slicedRects = new Rectangle[0];
            int spriteCount = 0;
            int gridSize = 0;
            int columns = 0;
            int rows = 0;

            // TextureAtlases will only be sliceable by a few predefined dimensions
            if (texture.Bounds.Width % 64 == 0 && texture.Bounds.Height % 64 == 0)
            {
                gridSize = 64;
            }

            columns = texture.Bounds.Width / gridSize;
            rows = texture.Bounds.Height / gridSize;
            slicedRects = new Rectangle[columns * rows];
            for (int i = 0; i < texture.Bounds.Width / gridSize; i++)
            {
                for (int j = 0; j < texture.Bounds.Height / gridSize; j++)
                {
                    slicedRects[spriteCount] = new Rectangle(j * gridSize, i * gridSize, gridSize, gridSize);//new SourceRectangleInfo(tex, new Rectangle(j * gridSize, i * gridSize, gridSize, gridSize));
                    spriteCount++;
                }
            }
            return slicedRects;
        }
    }
}
