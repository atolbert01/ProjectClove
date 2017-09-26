using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectClove
{
    class Actor
    {
        public Animation[] Anims { get; set; }
        public Actor(Animation[] anims)
        {
            Anims = anims;
        }
    }
}
