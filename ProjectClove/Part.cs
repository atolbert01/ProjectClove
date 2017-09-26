using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectClove
{
    class Part
    {
        public int ID { get; set; }
        public bool IsFlipped { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 WorldOrigin { get; set; }
        public Vector2 Position { get; set; }
        public double Scale { get; set; }
        public double Rotation { get; set; }
        public Texture2D Texture { get; set; }
        public int TexID { get; set; }
        public Rectangle SourceRect { get; set; }
        public Rectangle DestRect { get; set; }
    }
}
