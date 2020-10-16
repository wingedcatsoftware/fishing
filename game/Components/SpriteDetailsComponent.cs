
namespace Game.Components
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGameEcs;

    class SpriteDetailsComponent : Component
    {
        public Texture2D Texture { get; set; }

        public Point Position { get; set; }

        public Point Size { get; set; }
    }
}
