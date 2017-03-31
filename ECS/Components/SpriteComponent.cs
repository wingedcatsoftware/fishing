
namespace HarpoonFishing.Ecs.Components
{
    using Microsoft.Xna.Framework.Graphics;

    class SpriteComponent : Component
    {
        public SpriteComponent(World world, EntityId id) : base(world, id)
        {
        }

        public Texture2D Texture { get; set; }
    }
}
