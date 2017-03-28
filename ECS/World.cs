
namespace HarpoonFishing.Ecs
{
    using System;
    using System.Collections.Generic;
    using Components;
    using ECS.Systems;
    using Microsoft.Xna.Framework;

    class World
    {
        public World()
        {
            _systems = new List<System>();
            _componentTypeToComponentInstanceMap = new Dictionary<Type, Dictionary<EntityId, Component>>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (System system in _systems)
            {
                system.Update(gameTime);
            }
        }

        public T GetComponent<T>(EntityId entityId) where T : Component
        {
            if (_componentTypeToComponentInstanceMap.TryGetValue(typeof(T), out Dictionary<EntityId, Component> componentInstanceMap))
            {
                if (componentInstanceMap.TryGetValue(entityId, out Component component))
                {
                    return (T)component;
                }
            }

            return null;
        }

        private List<System> _systems;
        private Dictionary<Type, Dictionary<EntityId, Component>> _componentTypeToComponentInstanceMap;
    }
}
