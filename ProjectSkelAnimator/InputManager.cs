using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ProjectSkelAnimator
{
    class InputManager
    {
        public KeyboardState KeyState{ get; set; }
        public KeyboardState PrevKeyState { get; set; }
        public MouseState MouseState { get; set; }
        public AnimationGroup AnimGroup { get; set; }
        public Camera2D Camera { get; set; }
        private Animation _currentAnimation;
        public InputManager(AnimationGroup animGroup, Camera2D camera)
        {
            AnimGroup = animGroup;
            Camera = camera;
        }

        public void Update(AnimationGroup animGroup, Camera2D camera)
        {
            KeyState = Keyboard.GetState();
            AnimGroup = animGroup;
            Camera = camera;
            _currentAnimation = animGroup.CurrentAnimation;
            if (KeyState.IsKeyDown(Keys.NumPad4))
            {
                //camera.Position += new Vector2(-4, 0);
                foreach (Frame frame in _currentAnimation.Frames)
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
                foreach (Frame frame in _currentAnimation.Frames)
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
                foreach (Frame frame in _currentAnimation.Frames)
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
                foreach (Frame frame in _currentAnimation.Frames)
                {
                    foreach (Part part in frame.Parts)
                    {
                        part.Position += new Vector2(0, 4);
                    }
                }
            }
            if (KeyState.IsKeyDown(Keys.OemPlus) && PrevKeyState.IsKeyUp(Keys.OemPlus)) { camera.Zoom += 0.25f; }
            if (KeyState.IsKeyDown(Keys.OemMinus) && PrevKeyState.IsKeyUp(Keys.OemMinus)) { camera.Zoom -= 0.25f; }
            if (KeyState.IsKeyDown(Keys.Q) && PrevKeyState.IsKeyUp(Keys.Q)) { AnimGroup.CurrentAnimation.DrawBounds = !AnimGroup.CurrentAnimation.DrawBounds; }
            //if (KeyState.IsKeyDown(Keys.W) && PrevKeyState.IsKeyUp(Keys.W))
            //{
            //    Animation newAnim = new Animation(pixelTexture);
            //    newAnim.Bounds = new Rectangle(AnimGroup.CurrentAnimation.Bounds.X, AnimGroup.CurrentAnimation.Bounds.Y, AnimGroup.CurrentAnimation.Bounds.Width, AnimGroup.CurrentAnimation.Bounds.Height);
            //    foreach (Frame frame in AnimGroup.CurrentAnimation.Frames)
            //    {
            //        newAnim.AddFrame(new Frame(frame, true, newAnim.Bounds.Center));
            //    }
            //    AnimGroup.AddAnimation(newAnim);
            //    animGroupTextFields = AddNewTextField(animGroupTextFields, new Vector2(36, 152 + ((consolas.LineSpacing + 2) * AnimGroup.Animations.Length)));
            //    if (animGroupTextFields[animGroup.Animations.Length - 1] != null)
            //    {
            //        LoadEyeButtons();
            //    }
            //}

            if (KeyState != null) { PrevKeyState = KeyState; }
        }

    }
}
