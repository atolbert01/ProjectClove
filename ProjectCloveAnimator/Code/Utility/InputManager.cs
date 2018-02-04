using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ProjectCloveAnimator
{
    class InputManager
    {
        public KeyboardState KeyState{ get; set; }
        public KeyboardState PrevKeyState { get; set; }
        public MouseState MouseState { get; set; }
        private Cursor cursor { get; set; }
        public AnimationGroup AnimGroup { get; set; }
        public Camera2D Camera { get; set; }
        private Animation currentAnimation;
        public InputManager(AnimationGroup animGroup, Camera2D camera)
        {
            AnimGroup = animGroup;
            Camera = camera;
        }

        public void Update(Cursor cursor, AnimationGroup animGroup, Camera2D camera)
        {
            KeyState = Keyboard.GetState();
            this.cursor = cursor;
            AnimGroup = animGroup;
            Camera = camera;
            currentAnimation = animGroup.CurrentAnimation;

            if (cursor.State != CursorState.TextEdit)
            {
                if (KeyState.IsKeyDown(Keys.NumPad4))
                {
                    //camera.Position += new Vector2(-4, 0);
                    foreach (Frame frame in currentAnimation.Frames)
                    {
                        foreach (Part part in frame.Parts)
                        {
                            part.Position += new Vector2(-4, 0);
                        }
                    }
                }
                else if (KeyState.IsKeyDown(Keys.NumPad6))
                {
                    //camera.Position += new Vector2(4, 0);
                    foreach (Frame frame in currentAnimation.Frames)
                    {
                        foreach (Part part in frame.Parts)
                        {
                            part.Position += new Vector2(4, 0);
                        }
                    }
                }

                if (KeyState.IsKeyDown(Keys.NumPad8))
                {
                    //camera.Position += new Vector2(0, -4);
                    foreach (Frame frame in currentAnimation.Frames)
                    {
                        foreach (Part part in frame.Parts)
                        {
                            part.Position += new Vector2(0, -4);
                        }
                    }
                }
                else if (KeyState.IsKeyDown(Keys.NumPad2))
                {
                    //camera.Position += new Vector2(0, 4);
                    foreach (Frame frame in currentAnimation.Frames)
                    {
                        foreach (Part part in frame.Parts)
                        {
                            part.Position += new Vector2(0, 4);
                        }
                    }
                }
                if (KeyState.IsKeyDown(Keys.OemPlus) && PrevKeyState.IsKeyUp(Keys.OemPlus)) { camera.Zoom += 0.25f; }
                if (KeyState.IsKeyDown(Keys.OemMinus) && PrevKeyState.IsKeyUp(Keys.OemMinus)) { camera.Zoom -= 0.25f; }
                if (KeyState.IsKeyDown(Keys.Q) && PrevKeyState.IsKeyUp(Keys.Q)) { animGroup.CurrentAnimation.DrawBounds = !animGroup.CurrentAnimation.DrawBounds; }
                if (KeyState.IsKeyDown(Keys.W) && PrevKeyState.IsKeyUp(Keys.W))
                {
                    animGroup.DuplicateAnimation();
                }
            }

            if (KeyState != null) { PrevKeyState = KeyState; }
        }

    }
}
