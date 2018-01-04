using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectCloveAnimator
{
    enum TextState { Edit, None};
    class TextField
    {
        public TextState State = TextState.None;
        public TextState PrevState = TextState.None;
        public SpriteFont Font { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 LabelPosition { get; set; }
        public Vector2 Size { get; set; }
        public Rectangle Bounds { get; set; }
        public Texture2D FieldTexture { get; set; }
        public string Text { get; set; }
        public string Label { get; set; }
        public int MaxChars { get; set; }
        public TextField(SpriteFont font, Vector2 position, string text, int maxChars, Texture2D texture)
        {
            Text = text;
            Font = font;
            Position = position;
            Size = Font.MeasureString(Text);
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, 96, Font.LineSpacing);
            MaxChars = maxChars;
            FieldTexture = texture;
        }

        public TextField(SpriteFont font, Vector2 position, string text, string label, int maxChars, Texture2D texture)
        {
            Text = text;
            Label = label;
            Font = font;
            Position = position;
            LabelPosition = new Vector2(Position.X, Position.Y - Font.LineSpacing);
            Size = Font.MeasureString(Text);
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, 96, Font.LineSpacing);
            MaxChars = maxChars;
            FieldTexture = texture;
        }

        public void Update(Cursor cursor)
        {
            if (State == TextState.None)
            {
                if (cursor.DestRect.Intersects(Bounds))
                {
                    if (cursor.State != CursorState.TextEdit)
                    {
                        if (cursor.MouseState.LeftButton == ButtonState.Pressed)
                        {
                            cursor.EditingField = this;
                            cursor.Text = Text;
                            cursor.State = CursorState.TextEdit;
                            //PrevState = State;
                            State = TextState.Edit;
                        }
                    }
                }
            }
            else
            {
                Text = cursor.Text;
            }
            PrevState = State;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(FieldTexture, Bounds, Color.Black * 0.33f);
            if (Label != null) { spriteBatch.DrawString(Font, Label, LabelPosition, Color.White); }
            spriteBatch.DrawString(Font, Text, Position, Color.White);
        }
    }
}
