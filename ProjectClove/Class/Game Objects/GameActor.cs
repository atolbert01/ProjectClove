using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ProjectClove
{
    /// <summary>
    /// GameActors are characters, destructibles, animated traps, etc. Anything that is animated.
    /// GameActors also create a test log which consists of a list of TestLogEntries storing the GameActor's state, state start time, and state end time.
    /// </summary>
    class GameActor
    {
        public List<TestLogEntry> LogList { get; set; }
        public TestLogEntry CurrentLogEntry { get; set; }
        public int CurrentLogIndex { get; set; }
        public Animation[] Animations { get; set; }
        public Animation Anim { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Start { get; set; }
        public float ImageScale { get; set; }
        private GameTime _gameTime;
        private float _playbackTime;
        private int _state;
        public GameState GameState { get; set; }
        public TestLogEntry NewLogEntry { get; set; }
        public int LoggingState
        {
            get { return _state; }
            set
            {
                if (GameState == GameState.Playtest)
                {
                    if (_state != value)
                    {
                        if (NewLogEntry == null)
                        {
                            NewLogEntry = new TestLogEntry(_gameTime, value.GetHashCode());
                        }
                        else
                        {
                            NewLogEntry.EndTime = (float)_gameTime.TotalGameTime.TotalMilliseconds;
                            NewLogEntry = new TestLogEntry(_gameTime, value.GetHashCode());
                        }
                        LogList.Add(NewLogEntry);
                    }
                }
                _state = value;
            }
        }

        public GameActor(Animation[] anims, float imageScale, Vector2 loc)
        {
            CurrentLogIndex = -1;
            LogList = new List<TestLogEntry>();
            Animations = anims;
            Anim = Animations[0];
            ImageScale = imageScale;
            Position = loc;
            Start = loc;
        }

        public virtual void Update(GameTime gameTime, GameState gameState)
        {
            _gameTime = gameTime;

            // If we have just changed state to test mode then we need to make sure we store the end time for the current log entry
            if (GameState != gameState)
            {
                if (gameState == GameState.RunLog)
                {
                    if (NewLogEntry.EndTime == 0)
                    {
                        NewLogEntry.EndTime = (float)_gameTime.TotalGameTime.TotalMilliseconds;
                    }
                }
            }
            GameState = gameState;

            switch (gameState)
            {
                case GameState.Play:
                    break;
                case GameState.Playtest:
                    break;
                case GameState.RunLog:
                    if (CurrentLogIndex == -1)
                    {
                        LoggingState = 0;
                        _playbackTime = 0;
                        GetNextLogEntry();
                    }

                    if (CurrentLogIndex == 0)
                    {
                        Position = Start;
                    }

                    if (_playbackTime < CurrentLogEntry.EndTime)
                    {
                        LoggingState = CurrentLogEntry.State;
                        _playbackTime += 16.6667f;
                    }
                    else
                    {
                        GetNextLogEntry();
                        _playbackTime = CurrentLogEntry.StartTime;
                    }
                    break;
                case GameState.Edit:
                    break;
            }

                Anim.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Anim.Draw(spriteBatch, Position);
        }

        public void GetNextLogEntry()
        {
            if (LogList.Count >= 1)
            {
                if (CurrentLogIndex + 1 < LogList.Count)
                {
                    CurrentLogIndex++;
                }
                else
                {
                    CurrentLogIndex = 0;
                }
                CurrentLogEntry = LogList[CurrentLogIndex];
            }
            else
            {
                CurrentLogEntry = null;
            }
        }
    }
}
