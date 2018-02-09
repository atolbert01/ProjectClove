using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectClove
{
    class Editor
    {
        public ArrayList<UIControl> Controls { get; set; }
        public TexturePanel TexPanel { get; set; }
        public Cursor EditorCursor { get; set; }
        public Editor(TexturePanel texPanel, Cursor cursor)
        {
            Controls = new ArrayList<UIControl>();
            TexPanel = texPanel;

            // gotta changet this. Can't be adding every single sub control to the list
            Controls.Add(TexPanel);
            Controls.Add(TexPanel.HBorder);

            EditorCursor = cursor;
            EditorCursor.Controls = Controls;
        }

        public void Update()
        {
            //TexPanel.Update();

            for (int i = 0; i < Controls.Count; i++)
            {
                Controls.Items[i].Update();
            }
            EditorCursor.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TexPanel.Draw(spriteBatch);
            EditorCursor.Draw(spriteBatch);
        }
    }
}
