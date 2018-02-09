using Microsoft.Xna.Framework;

namespace ProjectClove
{
    /// <summary>
    /// Base class for all UI controls.
    /// </summary>
    class UIControl
    {
        public Rectangle Bounds { get; set; }
        public virtual void Update() { }
    }
}
