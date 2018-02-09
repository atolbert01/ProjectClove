using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    /// <summary>
    /// Displays a texture and allows us to slice it into smaller pieces.
    /// </summary>
    class TexturePanel : UIControl
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
        public Rectangle TexBounds { get; set; }
        /// <summary>
        /// The texture that we are currently editing.
        /// </summary>
        public Texture2D Texture { get; set; }
        public HorizontalPanelBorder HBorder { get; set; }
        private Texture2D transparencyTexture;
        private Cursor cursor;
        private int screenHeight;
        /// <summary>
        /// Use this constructor to pass in a texture to edit
        /// </summary>
        /// <param name="editingTexture"></param>
        public TexturePanel(Texture2D transparencyTexture, Texture2D editingTexture, Rectangle view, Cursor cursor, int screenHeight)
        {
            this.transparencyTexture = transparencyTexture;
            this.cursor = cursor;
            this.screenHeight = screenHeight;
            Texture = editingTexture;
            View = view;
            HBorder = new HorizontalPanelBorder(editingTexture, View.X, View.Y, View.Width, this.cursor);
        }

        public TexturePanel(Texture2D transparencyTexture)
        {
            this.transparencyTexture = transparencyTexture;
        }

        public void ReplaceTexture(Texture2D newTexture)
        {
            Texture = newTexture;
        }

        public override void Update()
        {
            base.Update();
            //View = new Rectangle(HBorder.Bounds.X,,,);
            HBorder.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the transparent background
            for (int i = 0; i < (View.Width / 16); i++)
            {
                for (int j = 0; j <= ((screenHeight - transparencyTexture.Width) / 16); j++)
                {
                    spriteBatch.Draw(transparencyTexture, new Rectangle(View.X + (i * 16), HBorder.Bounds.Y + (j * 16), transparencyTexture.Width, transparencyTexture.Height), Color.White);
                }
            }

            HBorder.Draw(spriteBatch);
        }
    }
}
