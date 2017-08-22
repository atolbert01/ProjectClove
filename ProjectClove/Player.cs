using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace ProjectClove
{
    class Player
    {
        private Animation animStanding { get; set; }
        private GamePadState gamePadState;
        private Vector2 v2Position;
        public Player(Texture2D tex)
        {
            animStanding = new Animation(tex, 8, 8);
            v2Position = new Vector2(0, 0);
        }

        public void Update()
        {
            gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.ThumbSticks.Left.X < 0.0f)
            {
                v2Position.X -= 8;
            }
            if (gamePadState.ThumbSticks.Left.X > 0.0f)
            {
                v2Position.X += 8;
            }
            if (gamePadState.ThumbSticks.Left.Y > 0.0f)
            {
                v2Position.Y -= 8;
            }
            if (gamePadState.ThumbSticks.Left.Y < 0.0f)
            {
                v2Position.Y += 8;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            animStanding.Draw(spriteBatch, v2Position);
        }
    }
}
