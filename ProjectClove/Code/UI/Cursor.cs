using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectClove
{

    enum CursorState { Point, Open, Grab, Pinch, Draw, Cut, ThumbsUp, ThumbsDown }
    /// <summary>
    /// Interacts with UI elements.     
    /// </summary>
    class Cursor
    {
        public CursorState CState { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle PointerArea
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, 1, 1); }
        }
        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)(Position.X), (int)(Position.Y), (int)(TexAtlas.SpriteSize * TexAtlas.Scale), (int)(TexAtlas.SpriteSize * TexAtlas.Scale)); }
        }
        public Rectangle SourceRect { get; set; }
        public MouseState MState { get; set; }
        public MouseState PrevMState { get; set; }
        public Color Tint { get; set; }
        public TextureAtlas TexAtlas { get; set; }
        public ArrayList<UIControl> Controls { get; set;}
        public bool IsClicked { get; set; }

        public Cursor(TextureAtlas sprites)
        {
            TexAtlas = sprites;
            Tint = Color.White;
            CState = 0;
            //TexAtlas.SpriteIndex = 0;
            Controls = new ArrayList<UIControl>();
            //TexAtlas.Scale = 0.66f;
        }
        public void Update()
        {
            if (MState != null)
            {
                PrevMState = MState;
            }
            MState = Mouse.GetState();
            Position = new Vector2(MState.X, MState.Y);

            HandleUIInteraction();

            TexAtlas.SpriteIndex = (int)CState;
        }

        /// <summary>
        /// Loop through all of the UI controls to see if the cursor is interacting with them and assign the appropriate actions.
        /// </summary>
        public void HandleUIInteraction()
        {
            foreach (UIControl ctl in Controls.Items)
            {
                if (ctl.Bounds != null && ctl.Bounds.Intersects(BoundingBox))
                {
                    if (ctl is IGrabbableUI)
                    {
                        IGrabbableUI grabbable = (IGrabbableUI) ctl;

                        CState = CursorState.Open;
                        if(MState.LeftButton == ButtonState.Pressed)
                        {
                            CState = CursorState.Grab;
                            grabbable.Grab();
                        }
                    }
                }
                else
                {
                    CState = 0;
                }

                //switch (CState)
                //{
                //    case CursorState.Point:
                //        break;
                //    case CursorState.Open:
                //        break;
                //    case CursorState.Grab:
                //        break;
                //    case CursorState.Pinch:
                //        break;
                //    case CursorState.Cut:
                //        break;
                //    case CursorState.ThumbsUp:
                //        break;
                //    case CursorState.ThumbsDown:
                //        break;
                //}
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TexAtlas.Draw(spriteBatch, Position);
        }
    }
}
