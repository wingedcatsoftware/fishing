
namespace HarpoonFishing.Ecs.Components
{
    using Microsoft.Xna.Framework;

    class PositionComponent : Component
    {
        public PositionComponent(World world, EntityId id) : base(world, id)
        {
        }

        public Vector2 Position { get; set; }
    }
}
