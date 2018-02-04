using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectCloveAnimator
{
    class Sliceable
    {
        SourceRectangleInfo[] sourceRectangles;

        /// <summary>
        /// Assigns source rectangles to each grid section of each texture.
        /// </summary>
        /// /// <param name="texturesToSlice"></param>
        /// <param name="gridSize"></param>
        /// /// <param name="totalRectangles"></param>
        public SourceRectangleInfo[] SliceSourceRectangles(Texture2D[] texturesToSlice, int gridSize, int totalRectangles)
        {
            sourceRectangles = new SourceRectangleInfo[totalRectangles];

            int spriteCount = 0;
            foreach (Texture2D tex in texturesToSlice)
            {
                if (tex != null)
                {
                    int rows = tex.Height / gridSize;
                    int columns = tex.Width / gridSize;
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            sourceRectangles[spriteCount] = new SourceRectangleInfo(tex, new Rectangle(j * gridSize, i * gridSize, gridSize, gridSize));
                            spriteCount++;
                        }
                    }
                }
            }
            return sourceRectangles;
        }

        /// <summary>
        /// Assigns source rectangles to each grid section a texture.
        /// </summary>
        /// <param name="textureToSlice"></param>
        /// <param name="gridSize"></param>
        /// <returns></returns>
        public SourceRectangleInfo[] SliceSourceRectangles(Texture2D textureToSlice, int gridSize)
        {
            int partCount = 0;

            if (textureToSlice != null)
            {
                int rows = textureToSlice.Height / gridSize;
                int columns = textureToSlice.Width / gridSize;
                sourceRectangles = new SourceRectangleInfo[rows * columns];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        sourceRectangles[partCount] = new SourceRectangleInfo(textureToSlice, new Rectangle(j * gridSize, i * gridSize, gridSize, gridSize));
                        partCount++;
                    }
                }
            }
            return sourceRectangles;
        }
    }

    /// <summary>
    /// Contains both the Texture 2D and source rectangle for a sprite.
    /// </summary>
    class SourceRectangleInfo
    {
        public Texture2D Texture { get; set; }
        public Rectangle SourceRect { get; set; }
        public SourceRectangleInfo(Texture2D texture, Rectangle sourceRect)
        {
            Texture = texture;
            SourceRect = sourceRect;
        }

    }
}