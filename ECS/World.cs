
namespace HarpoonFishing.Ecs
{
    using System;
    using System.Collections.Generic;
    using HarpoonFishing.Ecs.Components;
    using HarpoonFishing.Ecs.Systems;
    using Microsoft.Xna.Framework;

    using ComponentList = System.Collections.Generic.List<Components.Component>;
    using ComponentMap = System.Collections.Generic.Dictionary<EntityId, Components.Component>;
    using ComponentTypeToMap = System.Collections.Generic.Dictionary<System.Type, System.Collections.Generic.Dictionary<EntityId, Components.Component>>;
    using SystemList = System.Collections.Generic.List<Systems.System>;
    using SystemMap = System.Collections.Generic.Dictionary<UpdatePhase, System.Collections.Generic.List<Systems.System>>;

    partial class World
    {
        public World()
        {
            _systems = new SystemMap();
            _components = new ComponentTypeToMap();
        }

        // Note: Order of system registration matters.  Systems are Updated in the 
        //   order they are registered.
        public IEnumerable<(T, U)> RegisterSystem2<T, U>(System system) where T : Component where U : Component
        {
            SystemList phaseSystems;

            if (!_systems.TryGetValue(system.UpdatePhase, out phaseSystems))
            {
                phaseSystems = new SystemList();
                _systems.Add(system.UpdatePhase, phaseSystems);
            }

            phaseSystems.Add(system);

            return new ComponentEnuumerator2<T, U>(this);
        }

        public void ProcessPhase(GameTime gameTime, UpdatePhase phase)
        {
            if (_systems.TryGetValue(phase, out SystemList phaseSystems))
            {
                foreach (System system in phaseSystems)
                {
                    system.Update(gameTime);
                }
            }
        }

        public void AddEntity(EntityId id, ComponentList components)
        {
            foreach (Component component in components)
            {
                Type componentType = component.GetType();
                ComponentMap componentMap = null;

                if (!_components.TryGetValue(componentType, out componentMap))
                {
                    componentMap = new ComponentMap();
                    _components[componentType] = componentMap;
                }

                componentMap.Add(id, component);
            }
        }

        public T GetComponent<T>(EntityId id) where T : Component
        {
            if (_components.TryGetValue(typeof(T), out ComponentMap componentMap))
            {
                if (componentMap.TryGetValue(id, out Component component))
                {
                    return (T)component;
                }
            }

            return null;
        }

        public IEnumerable<(EntityId, T)> GetComponentEnumerator<T>() where T : Component 
        {
            if (_components.TryGetValue(typeof(T), out ComponentMap componentMap))
            {
                foreach (var item in componentMap)
                {
                    yield return (item.Key, (T)item.Value);
                }
            }
        }

        private SystemMap _systems;
        private ComponentTypeToMap _components;
    }
}
