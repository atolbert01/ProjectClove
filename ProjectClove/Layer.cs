using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ProjectClove
{
    /// <summary>
    /// Game objects exist on Layers. All game objects are drawn during their respective Layer Draw() call.
    /// This allows for visual layering (e.g. background images drawn behind parallax images drawn behind actors, etc.).
    /// </summary>
    class Layer
    {
        public GameActor[] Actors { get; set; }

        public Layer()
        {
            Actors = new GameActor[1];
        }

        public void Update(GameTime gameTime, GameState gameState)
        {
            foreach (GameActor actor in Actors)
            {
                actor.Update(gameTime, gameState);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameActor actor in Actors)
            {
                actor.Draw(spriteBatch);
            }
        }
    }
}
