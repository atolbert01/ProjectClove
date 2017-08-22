using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectSkelAnimator
{
    class PartsPalette : Sliceable
    {
        Rectangle panelRectangle;
        SourceRectangleInfo[] sourceRectangles;
        Part[] parts;
        Texture2D[] textures;
        //Texture2D pixel;
        Texture2D transparentTexture;
        GraphicsDeviceManager graphics;
        int gridSize = 64;
        int totalParts = 0; // we'll use this to determine how many source and destination rectangles to make.

        public PartsPalette(Texture2D[] textures, Texture2D transparentTexture, GraphicsDeviceManager graphics)
        {
            this.textures = textures;
            this.transparentTexture = transparentTexture;
            this.graphics = graphics;

            // Identify how many rectangles/sprites are in the textures
            foreach (Texture2D tex in textures)
            {
                if (tex != null)
                {
                    int rows = tex.Height / gridSize;
                    int columns = tex.Width / gridSize;
                    totalParts += rows * columns;
                }
            }

        }

        public void Load()
        {
            // Set up panel background
            panelRectangle = new Rectangle(0, graphics.GraphicsDevice.Viewport.Bounds.Height - gridSize, 16, 16);

            // Identify part source rectangles
            if (textures[0] != null){sourceRectangles = base.SliceSourceRectangles(textures, gridSize, totalParts);}

            // Initialize parts
            parts = new Part[sourceRectangles.Length];
            for (int i = 0; i < sourceRectangles.Length; i++)
            {
                parts[i] = new Part(sourceRectangles[i].Texture, sourceRectangles[i].SourceRect, new Rectangle(i * (gridSize / 2) + (gridSize / 2), graphics.GraphicsDevice.Viewport.Bounds.Height - (gridSize/2), gridSize/2, gridSize/2));
            }

        }

        public void Update(Cursor cursor)
        {
            foreach (Part part in parts)
            {
                part.Update(cursor);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the panel background
            for (int i = 0; i < (graphics.GraphicsDevice.Viewport.Width / 16); i++)
            {
                for (int j = 0; j < ((graphics.GraphicsDevice.Viewport.Height - gridSize) / 16); j++)
                {
                    spriteBatch.Draw(transparentTexture, new Rectangle(panelRectangle.X + (i * 16), panelRectangle.Y + (j * 16), panelRectangle.Width, panelRectangle.Height), Color.White);
                }
            }

            // Draw the part previews
            foreach (Part part in parts)
            {
                if (part.Texture != null) { part.Draw(spriteBatch); }
            }
        }
    }
}