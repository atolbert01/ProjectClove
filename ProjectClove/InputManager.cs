using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ProjectClove
{
    public enum InputState { None, Left, Right, Play, Playtest, RunLog, Edit}
    class InputManager
    {
        public InputState State { get; set; }
        public InputState PrevState { get; set; }
        public KeyboardState KeyState { get; set; }
        public KeyboardState PrevKeyState { get; set; }
        public InputManager(){ }
        public void Update()
        {
            KeyState = Keyboard.GetState();

            // Testing collision checking.
            if (KeyState.IsKeyDown(Keys.Left))
            {
                State = InputState.Left;
            }
            else if (KeyState.IsKeyDown(Keys.Right))
            {
                State = InputState.Right;
            }
            else if (KeyState.IsKeyUp(Keys.Right) && KeyState.IsKeyUp(Keys.Left))
            {
                State = InputState.None;
            }

            if (KeyState.IsKeyDown(Keys.F1) && PrevKeyState.IsKeyUp(Keys.F1))
            {
                State = InputState.Play;
            }
            else if (KeyState.IsKeyDown(Keys.F5) && PrevKeyState.IsKeyUp(Keys.F5))
            {
                State = InputState.Playtest;
            }
            else if (KeyState.IsKeyDown(Keys.F8) && PrevKeyState.IsKeyUp(Keys.F8))
            {
                State = InputState.RunLog;
            }
            else if (KeyState.IsKeyDown(Keys.F12) && PrevKeyState.IsKeyUp(Keys.F12))
            {
                State = InputState.Edit;
            }

            PrevKeyState = KeyState;
            PrevState = State;
        }
    }
}
