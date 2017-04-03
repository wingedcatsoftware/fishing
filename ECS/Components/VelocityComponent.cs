
namespace HarpoonFishing.Ecs.Components
{
    using Microsoft.Xna.Framework;

    class VelocityComponent : Component
    {
        public VelocityComponent(World world, EntityId id) : base(world, id)
        {
        }

        Vector2 Velocity { get; set; }
    }
}
