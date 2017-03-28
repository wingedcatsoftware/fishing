
namespace HarpoonFishing.Ecs
{
    using System.Collections.Generic;
    using Components;
    using ECS.Systems;
    using Microsoft.Xna.Framework;
    using System;

    class World
    {
        public World()
        {
            _systems = new List<Tuple<System, ComponentRequirements>>();
            _entities = new Dictionary<EntityId, List<Component>>();
        }

        // Note: Order of system registration matters.  Systems are Updated in the 
        //   order they are registered.
        public void RegisterSystem(System system, ComponentRequirements requirements)
        {
            _systems.Add(Tuple.Create(system, requirements));
        }

        public void AddEntity(EntityId id, List<Component> data)
        {
            _entities.Add(id, data);
        }

        public T GetComponent<T>(EntityId id) where T : Component
        {
            if (_entities.TryGetValue(id, out List<Component> components))
            {
                foreach (Component component in components)
                {
                    var result = component as T;
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return null;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var systemAndRequirements in _systems)
            {
                var system = systemAndRequirements.Item1;
                system.Update(gameTime);
            }
        }

        private List<Tuple<System, ComponentRequirements>> _systems;
        private Dictionary<EntityId, List<Component>> _entities;
    }
}
