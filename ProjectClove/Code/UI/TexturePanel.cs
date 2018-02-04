using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    /// <summary>
    /// Displays a texture and allows us to slice it into smaller pieces.
    /// </summary>
    class TexturePanel
    {
        /// <summary>
        /// Stores all of the rectangles we snip from our base texture.
        /// </summary>
        public ArrayList<Rectangle> SourceRects { get; set; }
        /// <summary>
        /// The viewable space of the panel.
        /// </summary>
        public Rectangle View { get; set; }
        /// <summary>
        /// The bounds of the texture we are viewing.
        /// </summary>
        public Rectangle Bounds { get; set; }
        /// <summary>
        /// The texture that we are currently editing.
        /// </summary>
        public Texture2D Texture { get; set; }
        public HorizontalPanelBorder HBorder { get; set; }
        private Texture2D transparencyTexture;
        /// <summary>
        /// Use this constructor to pass in a texture to edit
        /// </summary>
        /// <param name="editingTexture"></param>
        public TexturePanel(Texture2D transparencyTexture, Texture2D editingTexture, Rectangle view)
        {
            this.transparencyTexture = transparencyTexture;
            Texture = editingTexture;
            View = view;
            HBorder = new HorizontalPanelBorder(editingTexture, View.X, View.Y, View.Width);
        }

        public TexturePanel(Texture2D transparencyTexture)
        {
            this.transparencyTexture = transparencyTexture;
        }

        public void ReplaceTexture(Texture2D newTexture)
        {
            Texture = newTexture;
        }

        public void Update()
        {
            //View = new Rectangle(HBorder.Bounds.X,,,);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the transparent background
            for (int i = 0; i < (View.Width / 16); i++)
            {
                for (int j = 0; j <= ((View.Height - transparencyTexture.Width) / 16); j++)
                {
                    spriteBatch.Draw(transparencyTexture, new Rectangle(View.X + (i * 16), View.Y + (j * 16), transparencyTexture.Width, transparencyTexture.Height), Color.White);
                }
            }

            HBorder.Draw(spriteBatch);
        }
    }
}
