using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectCloveAnimator
{
    class Camera2D
    {
        public Matrix Transform;
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        private Rectangle bounds { get; set; }
        protected float zoom;
        public float Zoom{ get { return zoom; } set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; } }
        private GraphicsDevice graphicsDevice;
        public Camera2D(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            bounds = graphicsDevice.Viewport.Bounds;
            zoom = 1.0f;
            Rotation = 0.0f;
            Position = Vector2.Zero;
            Position = new Vector2(graphicsDevice.Viewport.Width/2, graphicsDevice.Viewport.Height/2);
        }

        public void Move(Vector2 amount)
        {
            Position += amount;
        }

        public Matrix Get_Transformation()
        {
            Transform = Matrix.CreateTranslation(
                new Vector3(-Position.X, -Position.Y, 0)) 
                * Matrix.CreateRotationZ(Rotation) 
                * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) 
                * Matrix.CreateTranslation(new Vector3(bounds.Width * 0.5f, bounds.Height * 0.5f, 0));
            return Transform;
        }
    }
}
