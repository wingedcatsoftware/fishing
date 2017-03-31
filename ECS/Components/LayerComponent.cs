
namespace HarpoonFishing.Ecs.Components
{
    class LayerComponent : Component
    {
        public LayerComponent(World world, EntityId id) : base(world, id)
        {
        }

        int Layer { get; set;  }
    }
}
