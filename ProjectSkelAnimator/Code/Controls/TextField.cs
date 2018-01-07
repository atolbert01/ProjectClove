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
        public SpriteFont FieldFont { get; set; }
        public SpriteFont LabelFont { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 LabelPosition { get; set; }
        public Vector2 Size { get; set; }
        public Rectangle Bounds { get; set; }
        public Texture2D FieldTexture { get; set; }
        public string Text { get; set; }
        public string Label { get; set; }
        public int MaxChars { get; set; }
        public TextField(SpriteFont fieldFont, SpriteFont labelFont, Vector2 position, string text, int maxChars, Texture2D texture)
        {
            Text = text;
            FieldFont = fieldFont;
            Position = position;
            Size = FieldFont.MeasureString(Text);
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, 96, FieldFont.LineSpacing);
            MaxChars = maxChars;
            FieldTexture = texture;
        }

        public TextField(SpriteFont fieldFont, SpriteFont labelFont, Vector2 position, string text, string label, int maxChars, Texture2D texture)
        {
            Text = text;
            Label = label;
            FieldFont = fieldFont;
            LabelFont = labelFont;
            Position = position;
            LabelPosition = new Vector2(Position.X, Position.Y - LabelFont.LineSpacing);
            Size = FieldFont.MeasureString(Text);
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, 96, FieldFont.LineSpacing);
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
            if (Label != null) { spriteBatch.DrawString(LabelFont, Label, LabelPosition, Color.White); }
            spriteBatch.DrawString(FieldFont, Text, Position, Color.White);
        }
    }
}
