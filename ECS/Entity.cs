
namespace HarpoonFishing.Ecs
{
    using ComponentList = System.Collections.Generic.List<Components.Component>;

    class Entity
    {
        Entity(EntityId id, ComponentList components)
        {
            this.Id = id;
            this.Components = components;
        }

        public EntityId Id { get; set; }

        public ComponentList Components { get; private set; }
    }
}
