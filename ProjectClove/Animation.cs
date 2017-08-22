using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class Animation

    {
        public Texture2D tex { get; set; }
        public int rows { get; set; }
        public int columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        public Animation(Texture2D tex, int rows, int columns)
        {
            this.tex = tex;
            this.rows = rows;
            this.columns = columns;
            currentFrame = 0;
            totalFrames = rows * columns;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = tex.Width / columns;
            int height = tex.Height / rows;
            int row = (int)((float)currentFrame / (float)columns);
            int column = currentFrame % columns;

            Rectangle rectSource = new Rectangle(width * column, height * row, width, height); // A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture.
            Rectangle rectDest = new Rectangle((int)location.X, (int)location.Y, width, height); // A rectangle that specifies (in screen coordinates) the destination for drawing the sprite. If this rectangle is not the same size as the source rectangle, the sprite will be scaled to fit.
            spriteBatch.Draw(tex, rectDest, rectSource, Color.White);
        }
    }
}
