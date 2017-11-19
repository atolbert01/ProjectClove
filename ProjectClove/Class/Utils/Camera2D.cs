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
        private int _windowSizeHor = 360;
        private int _boundsWidth, _boundsHeight, _camX, _camY;
        private Player _player;
        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, _boundsWidth, _boundsHeight); }
        }

        public Camera2D(float imageScale, Level currentLevel, Player player)
        {
            _imageScale = imageScale;
            _boundsWidth = (int)(1920 * _imageScale);
            _boundsHeight = (int)(1080 * _imageScale);
            _player = player;

            zoom = imageScale;
            Rotation = 0.0f;
            CurrentLevel = currentLevel;

            RoomWidth = _boundsWidth * CurrentLevel.CurrentRoom.Width;
            RoomHeight = _boundsHeight * CurrentLevel.CurrentRoom.Height;
            _windowSizeHor = (int)(_imageScale * _windowSizeHor);


            _camY = (int)_player.Position.Y;
            SetDefaultCamPos();
            Position = PlayerRelativePos();
        }

        public void SetDefaultCamPos()
        {
            if (_player.Position.X  > _boundsWidth * 0.5
                        && _player.Position.X < RoomWidth - (_boundsWidth * 0.5))
            {
                _camX = (int)_player.Position.X;
            }
            else if (_player.Position.X < _boundsWidth * 0.5)
            {
                _camX = (int)(_boundsWidth * 0.5);
            }
            else if (_player.Position.X > RoomWidth - (_boundsWidth * 0.5))
            {
                _camX = (int)(RoomWidth - (_boundsWidth * 0.5));
            }
        }
        public Vector2 PlayerRelativePos()
        {
            Vector2 newPos;

            if (_player.Position.X + _windowSizeHor > _boundsWidth * 0.5  && _player.Position.X - _windowSizeHor < RoomWidth - (_boundsWidth * 0.5))
            {
                if (_camX < (int)_player.Position.X - _windowSizeHor)
                {
                    _camX = (int)MathHelper.Lerp(_player.Position.X - _windowSizeHor, _camX, 0.3f);
                }
                else if (_camX > (int)_player.Position.X + _windowSizeHor)
                {
                    _camX = (int)MathHelper.Lerp(_camX, _player.Position.X + _windowSizeHor, 0.3f);
                }
            }
            
            newPos = new Vector2(_camX, _camY);
            return newPos;
        }

        public void Update(GameState gameState, InputManager input)
        {
            switch (gameState)
            {
                case GameState.Play:
                    Position = PlayerRelativePos();
                    break;
                case GameState.Playtest:
                    break;
                case GameState.RunLog:
                    break;
                case GameState.Edit:
                    if (input.Left)
                    {
                        if (Position.X > 0)
                        {
                            Move(new Vector2(-16, 0) * _imageScale); //*_imageScale
                        }
                    }
                    else if (input.Right)
                    {
                        if (Position.X < RoomWidth)
                        {
                            Move(new Vector2(16, 0) * _imageScale);
                        }
                    }

                    if (input.Up)
                    {
                        if (Position.Y > 0)
                        {
                            Move(new Vector2(0, -16) * _imageScale); //*_imageScale
                        }
                    }
                    else if (input.Down)
                    {
                        if (Position.Y < RoomHeight)
                        {
                            Move(new Vector2(0, 16) * _imageScale);
                        }
                    }
                    break;
            }
        }

        public void Move(Vector2 direction)
        {
            Position += direction;
            //Editor.Move(direction);
        }

        public Matrix Get_Transformation(GraphicsDevice graphicsDevice)
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) * Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) * Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return Transform;
        }
    }
}
