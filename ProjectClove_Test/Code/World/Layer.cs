﻿using Microsoft.Xna.Framework;
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
        public Terrain[] TerrainTiles { get; set; }

        public Layer()
        {
            Actors = new GameActor[1];
            TerrainTiles = new Terrain[1];
        }

        public void Update(GameTime gameTime, GameState gameState)
        {
            foreach (GameActor actor in Actors)
            {
                if (actor != null)
                {
                    actor.Update(gameTime, gameState);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameActor actor in Actors)
            {
                if (actor != null)
                {
                    actor.Draw(spriteBatch);
                }
            }

            foreach (Terrain t in TerrainTiles)
            {
                if (t != null)
                {
                    t.Draw(spriteBatch);
                }
            }
        }
    }
}
