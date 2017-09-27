using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectClove
{
    class Actor
    {
        public Animation[] Animations { get; set; }
        public Animation Anim { get; set; }
        public Actor(Animation[] anims)
        {
            Animations = anims;
            Anim = Animations[1];
        }

        public void Update()
        {
            Anim.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Anim.Draw(spriteBatch);
        }
    }
}
