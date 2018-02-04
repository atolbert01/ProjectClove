using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class HorizontalPanelBorder
    {
        public Rectangle Bounds { get; set; }
        /// <summary>
        /// The width of the panel.
        /// </summary>
        public int BorderWidth { get; set; }
        private int positionX, positionY;
        private int borderHeight = 2;
        private Texture2D pixelTexture;
        /// <summary>
        /// Defines a border panel at the X, Y coordinates specified. The Y coordinate is used to specify the bottom of the border.
        /// The interactible area of the border extends -4 pixels from the bottom of the border.
        /// </summary>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        /// <param name="borderWidth"></param>
        public HorizontalPanelBorder(Texture2D pixelTexture, int positionX, int positionY, int borderWidth)
        {
            this.pixelTexture = pixelTexture;
            this.positionX = positionX;
            this.positionY = positionY - borderHeight;
            BorderWidth = borderWidth;
            Bounds = new Rectangle(positionX, positionY, BorderWidth, borderHeight);
        }

        /// <summary>
        /// Adjusts the Y position of the border.
        /// </summary>
        /// <param name="positionY"></param>
        public void AdjustPosition(int positionY)
        {
            this.positionY = positionY;
            Bounds.Offset(positionX, positionY);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixelTexture, Bounds, Color.Black);
        }
    }
}
