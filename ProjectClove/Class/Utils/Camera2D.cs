using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class Camera2D
    {
        public Matrix Transform;
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        private float _imageScale;
        protected float zoom;
        public float Zoom{ get { return zoom; } set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; } }
        public Level CurrentLevel { get; set; }
        public int RoomWidth { get; set; }
        public int RoomHeight { get; set; }
        public EditorUI Editor { get; set; }
        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, RoomWidth, RoomHeight); }
        }

        public Camera2D(float imageScale, Level currentLevel, EditorUI editor)
        {
            _imageScale = imageScale;
            zoom = imageScale;
            Rotation = 0.0f;
            //Position = Vector2.Zero;
            CurrentLevel = currentLevel;
            RoomWidth = (int)(CurrentLevel.CurrentRoom.Width * (1920 * _imageScale));
            RoomHeight = (int)(CurrentLevel.CurrentRoom.Height * (1080 * _imageScale));
            Position = new Vector2(RoomWidth/2, RoomHeight/2);
            Editor = editor;
        }

        public void Update(GameState gameState, InputManager input)
        {
            switch (gameState)
            {
                case GameState.Play:
                    break;
                case GameState.Playtest:
                    break;
                case GameState.RunLog:
                    break;
                case GameState.Edit:
                    if (input.State == InputState.Left)
                    {
                        if (BoundingBox.X > 0)
                        {
                            Move(new Vector2(-4, 0)); //*_imageScale
                        }
                    }
                    else if (input.State == InputState.Right)
                    {
                        if ((RoomWidth - BoundingBox.X) < RoomWidth)
                        {
                            Move(new Vector2(4, 0));
                        }
                    }
                    break;
            }
        }

        public void Move(Vector2 amount)
        {
            Position += amount;
            Editor.Move(amount);
        }

        public Matrix Get_Transformation(GraphicsDevice graphicsDevice)
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) * Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) * Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return Transform;
        }
    }
}
