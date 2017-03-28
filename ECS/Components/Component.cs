
namespace HarpoonFishing.Ecs.Components
{
    class Component
    {
        public Component(World world, EntityId id)
        {
            _world = world;
            _id = id;
        }

        public T GetSibling<T>() where T : Component
        {
            return _world.GetComponent<T>(_id);
        }

        private World _world;
        private EntityId _id;
    }
}
