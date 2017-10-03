using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ProjectClove
{
    /// <summary>
    /// A level is a container for Rooms which contain object Layers.
    /// </summary>
    class Level
    {
        public Dictionary<int, Room> Rooms { get; set; }
        public Room CurrentRoom { get; set; }
        public Level()
        {
            Rooms = new Dictionary<int, Room>();
        }
        public void Update(GameTime gameTime)
        {
            CurrentRoom.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentRoom.Draw(spriteBatch);
        }
    }
}
