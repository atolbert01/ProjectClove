using Microsoft.Xna.Framework;

namespace ProjectClove
{
    /// <summary>
    /// When the game is in PlayTest mode, GameActors/Sprites create a TestLogEntry for each frame.
    /// The TestLogEntry stores the elapsed time and the current state.
    /// The log will be saved after a play session and can then be used to recreate the session for later viewing.
    /// </summary>
    class TestLogEntry
    {
        public float StartTime { get; set; }
        public float EndTime { get; set; }
        public int State { get; set; }
        public TestLogEntry(GameTime gameTime, int state)
        {
            if (gameTime == null)
            {
                StartTime = 0;
            }
            else
            {
                StartTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
            }
            State = state;
        }
    }
}
