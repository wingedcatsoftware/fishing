
namespace HarpoonFishing.Ecs.Components
{
    class LayerComponent : Component
    {
        public LayerComponent(World world, EntityId id) : base(world, id)
        {
        }

        public int Layer { get; set;  }
    }
}
