using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ProjectClove
{
    public enum InputState { None, Play, Playtest, RunLog, Edit}
    class InputManager
    {
        public InputState State { get; set; }
        public InputState PrevState { get; set; }
        public KeyboardState KeyState { get; set; }
        public KeyboardState PrevKeyState { get; set; }
        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool Up { get; set; }
        public bool Down { get; set; }
        public InputManager(){ }
        public void Update()
        {
            KeyState = Keyboard.GetState();

            // Directional controls
            // Horizontal
            if (KeyState.IsKeyDown(Keys.Left))
            {
                Left = true;
                Right = false;
            }
            else if (KeyState.IsKeyDown(Keys.Right))
            {
                Left = false;
                Right = true;
            }

            if (KeyState.IsKeyUp(Keys.Left))
            {
                Left = false;
            }

            if (KeyState.IsKeyUp(Keys.Right))
            {
                Right = false;
            }

            // Vertical
            if (KeyState.IsKeyDown(Keys.Up))
            {
                Up = true;
                Down = false;
            }
            else if (KeyState.IsKeyDown(Keys.Down))
            {
                Up = false;
                Down = true;
            }

            if (KeyState.IsKeyUp(Keys.Up))
            {
                Up = false;
            }

            if (KeyState.IsKeyUp(Keys.Down))
            {
                Down = false;
            }


            // Control game states
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
