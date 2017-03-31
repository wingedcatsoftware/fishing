
namespace HarpoonFishing.Ecs
{
    using System.Collections;
    using System.Collections.Generic;

    using ComponentList = System.Collections.Generic.List<Components.Component>;

    partial class World
    {
        private class EntitySet : IEnumerable<Entity>
        {
            public EntitySet(World world, ComponentRequirements componentRequirements)
            {
                _world = world;
                _componentRequirements = componentRequirements;
            }

            public IEnumerator<Entity> GetEnumerator()
            {
                foreach (Entity entity in _world._entities.Values)
                {
                    if (_componentRequirements.AreSatisfiedBy(entity))
                    {
                        yield return entity;
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private World _world;
            private ComponentRequirements _componentRequirements;
        }
    }
}
