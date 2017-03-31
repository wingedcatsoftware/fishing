
namespace HarpoonFishing.Ecs
{
    using HarpoonFishing.Ecs.Components;
    using ComponentList = System.Collections.Generic.List<Components.Component>;

    class Entity
    {
        public Entity(EntityId id, ComponentList components)
        {
            this.Id = id;
            this.Components = components;
        }

        public EntityId Id { get; set; }

        public ComponentList Components { get; private set; }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in Components)
            {
                var result = component as T;
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
