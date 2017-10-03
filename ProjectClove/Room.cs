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

        // A list of the level IDs of the rooms that are connected to this room
        public int[] Transitions { get; set; }

        public Room(int id)
        {
            ID = id;
        }
        public void Update(GameTime gameTime)
        {
            foreach (Layer layer in Layers)
            {
                layer.Update(gameTime);
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
