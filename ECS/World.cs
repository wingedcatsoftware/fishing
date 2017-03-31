
namespace HarpoonFishing.Ecs
{
    using HarpoonFishing.Ecs.Components;
    using HarpoonFishing.Ecs.Systems;
    using Microsoft.Xna.Framework;

    using SystemList = System.Collections.Generic.List<Systems.System>;
    using EntityMap = System.Collections.Generic.Dictionary<EntityId, Entity>;
    using SystemMap = System.Collections.Generic.Dictionary<UpdatePhase, System.Collections.Generic.List<Systems.System>>;


    partial class World
    {
        public World()
        {
            _systems = new SystemMap();
            _entities = new EntityMap();
        }

        // Note: Order of system registration matters.  Systems are Updated in the 
        //   order they are registered.
        public void RegisterSystem(System system)
        {
            SystemList phaseSystems;

            if (!_systems.TryGetValue(system.UpdatePhase, out phaseSystems))
            {
                phaseSystems = new SystemList();
                _systems.Add(system.UpdatePhase, phaseSystems);
            }

            phaseSystems.Add(system);
        }

        public void ProcessPhase(GameTime gameTime, UpdatePhase phase)
        {
            if (_systems.TryGetValue(phase, out SystemList phaseSystems))
            {
                foreach (System system in phaseSystems)
                {
                    EntitySet relevantEntities = new EntitySet(this, system.ComponentRequirements);
                    system.Update(gameTime, relevantEntities);
                }
            }
        }

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity.Id, entity);
        }

        public T GetComponent<T>(EntityId id) where T : Component
        {
            if (_entities.TryGetValue(id, out Entity entity))
            {
                foreach (Component component in entity.Components)
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

        public Entity GetEntity(EntityId id)
        {
            Entity result;

            if (!_entities.TryGetValue(id, out result))
            {
                result = null;
            }

            return result;
        }

        private SystemMap _systems;
        private EntityMap _entities;
    }
}
