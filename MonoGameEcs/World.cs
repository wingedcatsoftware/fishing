
namespace MonoGameEcs
{
    using global::System;
    using global::System.Collections.Generic;
    using Microsoft.Xna.Framework;

    using ComponentList = global::System.Collections.Generic.List<Component>;
    using ComponentMap = global::System.Collections.Generic.Dictionary<EntityId, Component>;
    using ComponentTypeToMap = global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.Dictionary<EntityId, Component>>;
    using SystemRegistrationList = global::System.Collections.Generic.List<SystemRegistration>;
    using SystemMap = global::System.Collections.Generic.Dictionary<UpdatePhase, global::System.Collections.Generic.List<SystemRegistration>>;
    
    public enum ComponentUse
    {
        Read,
        Write,
        ReadWrite
    }

    public partial class World
    {
        public World()
        {
            _deferredCreationQueue = new Queue<(EntityId, ComponentList)>();
            _systems = new SystemMap();
            _components = new ComponentTypeToMap();
        }

        // Note: Order of system registration matters.  Systems are Updated in the 
        //   order they are registered.
        public IEnumerable<T> RegisterSystem<T>(System system, ComponentUse tUse) where T : Component
        {
            RegisterSystemInternal(system, (typeof(T), tUse));
            return new ComponentEnumerator<T>(this);
        }

        public IEnumerable<(T, U)> RegisterSystem<T, U>(System system, ComponentUse tUse, ComponentUse uUse) where T : Component where U : Component
        {
            RegisterSystemInternal(system, (typeof(T), tUse), (typeof(U), uUse));
            return new ComponentEnumerator2<T, U>(this);
        }

        public IEnumerable<(T, U, V)> RegisterSystem<T, U, V>(System system, ComponentUse tUse, ComponentUse uUse, ComponentUse vUse) where T : Component where U : Component where V : Component
        {
            RegisterSystemInternal(system, (typeof(T), tUse), (typeof(U), uUse), (typeof(V), vUse));
            return new ComponentEnumerator3<T, U, V>(this);
        }

        public IEnumerable<(T, U, V, W)> RegisterSystem<T, U, V, W>(System system, ComponentUse tUse, ComponentUse uUse, ComponentUse vUse, ComponentUse wUse) where T : Component where U : Component where V : Component where W : Component
        {
            RegisterSystemInternal(system, (typeof(T), tUse), (typeof(U), uUse), (typeof(V), vUse), (typeof(W), wUse));
            return new ComponentEnumerator4<T, U, V, W>(this);
        }

        public IEnumerable<(T, U, V, W, X)> RegisterSystem<T, U, V, W, X>(System system, ComponentUse tUse, ComponentUse uUse, ComponentUse vUse, ComponentUse wUse, ComponentUse xUse) where T : Component where U : Component where V : Component where W : Component where X : Component
        {
            RegisterSystemInternal(system, (typeof(T), tUse), (typeof(U), uUse), (typeof(V), vUse), (typeof(W), wUse), (typeof(X), xUse));
            return new ComponentEnumerator5<T, U, V, W, X>(this);
        }

        public void Update(GameTime gameTime)
        {
            AddDeferredEntities();

            ProcessPhase(gameTime, UpdatePhase.Main);
        }

        public void Render(GameTime gameTime)
        {
            ProcessPhase(gameTime, UpdatePhase.Render);
        }

        public void AddEntity(EntityId id, ComponentList components)
        {
            _deferredCreationQueue.Enqueue((id, components));
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

        private void ProcessPhase(GameTime gameTime, UpdatePhase phase)
        {
            if (_systems.TryGetValue(phase, out SystemRegistrationList phaseSystemRegistrations))
            {
                foreach (SystemRegistration registration in phaseSystemRegistrations)
                {
                    registration.System.Update(gameTime);
                }
            }
        }

        private void RegisterSystemInternal(System system, params (Type, ComponentUse)[] args)
        {
            SystemRegistration systemRegistration = new SystemRegistration();
            systemRegistration.System = system;

            foreach (var arg in args)
            {
                systemRegistration.Requirements.Add(arg);
            }

            SystemRegistrationList phaseSystems;
            if (!_systems.TryGetValue(system.UpdatePhase, out phaseSystems))
            {
                phaseSystems = new SystemRegistrationList();
                _systems.Add(system.UpdatePhase, phaseSystems);
            }

            phaseSystems.Add(systemRegistration);
        }

        private void AddDeferredEntities()
        {
            while (_deferredCreationQueue.Count > 0)
            {
                (EntityId id, ComponentList components) = _deferredCreationQueue.Dequeue();

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
        }

        private Queue<(EntityId, ComponentList)> _deferredCreationQueue;
        private SystemMap _systems;
        private ComponentTypeToMap _components;
    }
}
