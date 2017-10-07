﻿using Microsoft.Xna.Framework;

namespace ProjectClove
{
    public enum PlayerState { StandR, StandL, RunR, RunL }
    class Player : GameActor
    {
        private PlayerState _state;
        //private GameState _gameState;
        public PlayerState State
        {
            get { return _state; }
            set
            {
                if (GameState == GameState.Playtest)
                {
                    if (_state != value)
                    {
                        LoggingState = value.GetHashCode();
                    }
                }
                _state = value;
            }
        }
        public PlayerState PrevState { get; set; }
        private InputManager _input { get; set; }
        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)Location.X - Anim.Bounds.Width / 2, (int)Location.Y - Anim.Bounds.Height / 2, Anim.Bounds.Width, Anim.Bounds.Height); }
        }
        public Player(Animation[] anims, Vector2 loc, InputManager input) : base(anims, loc)
        {
            _input = input;
            NewLogEntry = new TestLogEntry(null, State.GetHashCode());
            LogList.Add(NewLogEntry);
        }

        public override void Update(GameTime gameTime, GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Play:
                    if (_input.State == InputState.Left)
                    {
                        State = PlayerState.RunL;
                    }
                    else if (_input.State == InputState.Right)
                    {
                        State = PlayerState.RunR;
                    }
                    else if (_input.State == InputState.None)
                    {
                        if (PrevState == PlayerState.RunR || PrevState == PlayerState.StandR)
                        {
                            State = PlayerState.StandR;
                        }
                        else if (PrevState == PlayerState.RunL || PrevState == PlayerState.StandL)
                        {
                            State = PlayerState.StandL;
                        }
                    }
                    PrevState = State;
                    break;
                case GameState.Playtest:
                    if (_input.State == InputState.Left)
                    {
                        State = PlayerState.RunL;
                    }
                    else if (_input.State == InputState.Right)
                    {
                        State = PlayerState.RunR;
                    }
                    else if (_input.State == InputState.None)
                    {
                        if (PrevState == PlayerState.RunR || PrevState == PlayerState.StandR)
                        {
                            State = PlayerState.StandR;
                        }
                        else if (PrevState == PlayerState.RunL || PrevState == PlayerState.StandL)
                        {
                            State = PlayerState.StandL;
                        }
                    }
                    PrevState = State;
                    break;
                case GameState.RunLog:
                    State = (PlayerState)LoggingState;
                    break;
                case GameState.Edit:
                    break;
            }

            switch (State)
            {
                case PlayerState.StandR:
                    Anim = Animations[0];
                    break;
                case PlayerState.StandL:
                    Anim = Animations[2];
                    break;
                case PlayerState.RunR:
                    if (BoundingBox.X < 794 - BoundingBox.Width)
                    {
                        Location += new Vector2(6, 0);
                        Anim = Animations[1];
                    }
                    else
                    {
                        Anim = Animations[0];
                    }
                    break;
                case PlayerState.RunL:
                    if (BoundingBox.X > 6)
                    {
                        Location += new Vector2(-6, 0);
                        Anim = Animations[3];
                    }
                    else
                    {
                        Anim = Animations[2];
                    }
                    break;
            }

            base.Update(gameTime, gameState);
        }
    }
}
