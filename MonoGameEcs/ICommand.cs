namespace MonoGameEcs
{
    using global::System;

    using ComponentMap = global::System.Collections.Generic.Dictionary<EntityId, Component>;
    using ComponentTypeToMap = global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.Dictionary<EntityId, Component>>;

    public interface ICommand
    {
        void Execute(ComponentTypeToMap components);
    }

    public class RemoveComponentCommand : ICommand
    {
        public EntityId Id { get; set; }

        public Component Component { get; set; }

        public void Execute(ComponentTypeToMap components)
        {
            var componentMap = components[Component.GetType()];
            componentMap.Remove(Id);
        }
    }

    public class AddComponentCommand : ICommand
    {
        public EntityId Id { get; set; }

        public Component Component { get; set; }

        public void Execute(ComponentTypeToMap componentTypeMap)
        {
            ComponentMap componentMap;
            if (!componentTypeMap.TryGetValue(Component.GetType(), out componentMap))
            {
                componentMap = new ComponentMap();
                componentTypeMap.Add(Component.GetType(), componentMap);
            }

            componentMap.Add(Id, Component);
        }
    }
}
