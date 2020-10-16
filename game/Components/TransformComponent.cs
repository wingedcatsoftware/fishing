
namespace Game.Components
{
    using Microsoft.Xna.Framework;
    using MonoGameEcs;

    class TransformComponent : Component
    {
        public TransformComponent()
        {
            this.Position = new Vector2(0.0f, 0.0f);
            this.Rotation = 0.0f;
            this.Scale = new Vector2(1.0f, 1.0f);
        }

        public Vector2 Position { get; set; }

        public float Rotation { get; set; }

        public Vector2 Scale { get; set; }
    }
}
