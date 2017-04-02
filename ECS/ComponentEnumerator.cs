
namespace HarpoonFishing.Ecs
{
    using System.Collections;
    using System.Collections.Generic;
    using HarpoonFishing.Ecs.Components;

    partial class World
    {
        private class ComponentEnuumerator2<T, U> : IEnumerable<(T, U)> where T : Component where U : Component
        {
            public ComponentEnuumerator2(World world)
            {
                _world = world;
            }

            public IEnumerator<(T, U)> GetEnumerator()
            {
                foreach ((EntityId id, T t) in _world.GetComponentEnumerator<T>())
                {
                    U u = _world.GetComponent<U>(id); ;
                    if (u != null)
                    {
                        yield return (t, u);
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private World _world;
        }
    }
}
