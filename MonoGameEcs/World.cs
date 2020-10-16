
namespace MonoGameEcs
{
    using global::System;
    using global::System.Linq;
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
            _startOfFrameQueue = new Queue<ICommand>();
            _deferredCreationQueue = new Queue<(EntityId, ComponentList)>();
            _systems = new SystemMap();
            _components = new ComponentTypeToMap();
        }

        public void RegisterSystem(System system)
        {
            // Implement
        }

        // Note: Order of system registration matters.  Systems are Updated in the 
        //   order they are registered.
        public IEnumerable<(EntityId, T)> RegisterDependencies<T>(System system, ComponentUse tUse) where T : Component
        {
            RegisterDependenciesInternal(system, (typeof(T), tUse));
            return new ComponentEnumerator<T>(this);
        }

        public IEnumerable<(EntityId, T, U)> RegisterDependencies<T, U>(System system, ComponentUse tUse, ComponentUse uUse) where T : Component where U : Component
        {
            RegisterDependenciesInternal(system, (typeof(T), tUse), (typeof(U), uUse));
            return new ComponentEnumerator2<T, U>(this);
        }

        public IEnumerable<(EntityId, T, U, V)> RegisterDependencies<T, U, V>(System system, ComponentUse tUse, ComponentUse uUse, ComponentUse vUse) where T : Component where U : Component where V : Component
        {
            RegisterDependenciesInternal(system, (typeof(T), tUse), (typeof(U), uUse), (typeof(V), vUse));
            return new ComponentEnumerator3<T, U, V>(this);
        }

        public IEnumerable<(EntityId, T, U, V, W)> RegisterDependencies<T, U, V, W>(System system, ComponentUse tUse, ComponentUse uUse, ComponentUse vUse, ComponentUse wUse) where T : Component where U : Component where V : Component where W : Component
        {
            RegisterDependenciesInternal(system, (typeof(T), tUse), (typeof(U), uUse), (typeof(V), vUse), (typeof(W), wUse));
            return new ComponentEnumerator4<T, U, V, W>(this);
        }

        public IEnumerable<(EntityId, T, U, V, W, X)> RegisterDependencies<T, U, V, W, X>(System system, ComponentUse tUse, ComponentUse uUse, ComponentUse vUse, ComponentUse wUse, ComponentUse xUse) where T : Component where U : Component where V : Component where W : Component where X : Component
        {
            RegisterDependenciesInternal(system, (typeof(T), tUse), (typeof(U), uUse), (typeof(V), vUse), (typeof(W), wUse), (typeof(X), xUse));
            return new ComponentEnumerator5<T, U, V, W, X>(this);
        }

        public void Update(GameTime gameTime)
        {
            while (_startOfFrameQueue.Count > 0)
            {
                var command = _startOfFrameQueue.Dequeue();
                command.Execute(_components);
            }

            AddDeferredEntities();

            ProcessPhase(gameTime, UpdatePhase.Main);
        }

        public void Render(GameTime gameTime)
        {
            ProcessPhase(gameTime, UpdatePhase.Render);
        }

        public void AddEntity(EntityId id, params Component[] args)
        {
            _deferredCreationQueue.Enqueue((id, args.ToList()));
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
                    registration.System.Update(gameTime, _startOfFrameQueue);
                }
            }
        }

        private void RegisterDependenciesInternal(System system, params (Type, ComponentUse)[] args)
        {
            SystemRegistration systemRegistration = new SystemRegistration();
            systemRegistration.System = system;

            foreach (var arg in args)
            {
                systemRegistration.Requirements.Add(arg);
            }

            SystemRegistrationList phaseSystems;
            if (_systems.TryGetValue(system.UpdatePhase, out phaseSystems))
            {
                foreach (var existingSystemRegistration in phaseSystems)
                {
                    if (existingSystemRegistration.System == system)
                    {
                        // System is already registered.
                        return;
                    }
                }
            }
            else
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

        private Queue<ICommand> _startOfFrameQueue;
        private Queue<(EntityId, ComponentList)> _deferredCreationQueue;
        private SystemMap _systems;
        private ComponentTypeToMap _components;
    }
}
