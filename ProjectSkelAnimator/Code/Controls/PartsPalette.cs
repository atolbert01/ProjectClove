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
        public Vector2 Scroll;
        public int GridSize = 90;
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
                    int rows = tex.Height / GridSize;
                    int columns = tex.Width / GridSize;
                    totalParts += rows * columns;
                }
            }

        }

        public void Load()
        {
            // Set up panel background
            panelRectangle = new Rectangle(0, graphics.GraphicsDevice.Viewport.Bounds.Height - GridSize, 16, 16);

            // Identify part source rectangles
            if (textures[0] != null){sourceRectangles = base.SliceSourceRectangles(textures, GridSize, totalParts);}

            // Initialize parts
            parts = new Part[sourceRectangles.Length];
            for (int i = 0; i < sourceRectangles.Length; i++)
            {
                Part newPart = new Part(sourceRectangles[i].Texture, sourceRectangles[i].SourceRect, new Rectangle(i * (GridSize / 2) + (GridSize / 2), graphics.GraphicsDevice.Viewport.Bounds.Height - (GridSize / 2), GridSize / 2, GridSize / 2));
                newPart.State = PartState.Preview;
                parts[i] = newPart;
            }

        }

        public void Update(Cursor cursor)
        {
            foreach (Part part in parts)
            {
                if (Scroll != null)
                {
                    if (Scroll != Vector2.Zero)
                    {
                        part.Position += Scroll;
                        part.DestRect = new Rectangle((int)part.Position.X, (int)part.Position.Y, part.DestRect.Width, part.DestRect.Height);

                    }
                }
                part.Update(cursor);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the panel background
            for (int i = 0; i < (graphics.GraphicsDevice.Viewport.Width / 16); i++)
            {
                for (int j = 0; j < ((graphics.GraphicsDevice.Viewport.Height - GridSize) / 16); j++)
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