using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectSkelAnimator
{
    enum PartState { Preview, Selected, Idle };
    class Part
    {
        public Part Parent { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle SourceRect { get; set; }
        public Rectangle DestRect { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 WorldOrigin { get; set; }
        public Vector2 Position { get; set; }

        public Color Tint = Color.White;
        public PartState State = PartState.Preview;
        
        public Part(Texture2D texture, Rectangle sourceRect, Rectangle destRect)
        {
            this.Texture = texture;
            this.DestRect = destRect;
            this.SourceRect = sourceRect;
            Origin = new Vector2(DestRect.Width / 2, DestRect.Height / 2);
            Position = new Vector2(DestRect.X, DestRect.Y);
            WorldOrigin = new Vector2(DestRect.X + (DestRect.Width / 2), DestRect.Y + (DestRect.Height / 2));
            Scale = 1f;
            Rotation = 0f;
        }

        public void Update(Cursor cursor)
        {
            switch (State)
            {
                case PartState.Preview:
                    Scale = 0.5f;
                    if (cursor.DestRect.Intersects(DestRect))
                    {
                        Tint = Color.DarkRed;
                        if (cursor.MouseState.LeftButton == ButtonState.Pressed)
                        {
                            cursor.StandbyPart = new Part(Texture, SourceRect, DestRect);
                        }
                    }
                    else
                    {
                        Tint = Color.White;
                    }
                    break;
                case PartState.Selected:
                    if (cursor.State != CursorState.Arrow)
                    {
                        Tint = Color.DarkRed;
                    }
                    else
                    {
                        Tint = Color.White;
                    }

                    break;
                case PartState.Idle:
                    Tint = Color.White;
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SourceRect, Tint, Rotation, Origin, Scale, SpriteEffects.None, 0);
        }

        public void SetParent(Part parent)
        {
            Parent = parent;
        }
    }
}
