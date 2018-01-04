using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectCloveAnimator
{
    class Camera2D
    {
        public Matrix Transform;
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        protected float zoom;
        public float Zoom{ get { return zoom; } set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; } }
        public Camera2D()
        {
            zoom = 1.0f;
            Rotation = 0.0f;
            Position = Vector2.Zero;
        }

        public void Move(Vector2 amount)
        {
            Position += amount;
        }

        public Matrix Get_Transformation(GraphicsDevice graphicsDevice)
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) * Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) * Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return Transform;
        }
    }
}
