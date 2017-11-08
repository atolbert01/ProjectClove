using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    /// <summary>
    /// A Level can contain one or more Rooms with transitions between each Room.
    /// Each room can have one or more transitions linking to other rooms in the level.
    /// Rooms are identified by a unique ID.
    /// </summary>
    class Room
    {
        public Layer[] Layers { get; set; }
        public int ID { get; set; }
        
        /// <summary>
        /// Room Width is expressed as the number of horizontal screens that a room occupies. 
        /// If a room has a Width of 2 and the resolution is 1920 x 1080, then the room will be 1920*2 pixels wide.
        /// Defaults to 1.
        /// </summary>
        public int Width { get; set; }
        
        /// <summary>
        /// Room Height is expressed as the number of vertical screens that a room occupies. 
        /// If a room has a Height of 2 and the resolution is 1920 x 1080, then the room will be 1080*2 pixels high.
        /// Defaults to 1.
        /// </summary>
        public int Height { get; set; }

        // A list of the level IDs of the rooms that are connected to this room
        public int[] Transitions { get; set; }

        public Room(int id)
        {
            ID = id;
            Width = 1;
            Height = 1;
        }
        public void Update(GameTime gameTime, GameState gameState)
        {
            foreach (Layer layer in Layers)
            {
                layer.Update(gameTime, gameState);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Layer layer in Layers)
            {
                layer.Draw(spriteBatch);
            }
        }

    }
}
