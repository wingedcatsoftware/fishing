
namespace MonoGameEcs
{
    using global::System.Collections;
    using global::System.Collections.Generic;

    public partial class World
    {
        private class ComponentEnumerator<T> : ComponentEnumeratorBase, IEnumerable<T> where T : Component
        {
            public ComponentEnumerator(World world) : base(world)
            {
            }

            public IEnumerator<T> GetEnumerator()
            {
                foreach ((EntityId id, T t) in World.GetComponentEnumerator<T>())
                {
                    yield return t;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class ComponentEnumerator2<T, U> : ComponentEnumeratorBase, IEnumerable<(T, U)> where T : Component where U : Component
        {
            public ComponentEnumerator2(World world) : base(world)
            {
            }

            public IEnumerator<(T, U)> GetEnumerator()
            {
                foreach ((EntityId id, T t) in World.GetComponentEnumerator<T>())
                {
                    U u = World.GetComponent<U>(id); ;
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
        }

        private class ComponentEnumerator3<T, U, V> : ComponentEnumeratorBase, IEnumerable<(T, U, V)> where T : Component where U : Component where V : Component
        {
            public ComponentEnumerator3(World world) : base(world)
            {
            }

            public IEnumerator<(T, U, V)> GetEnumerator()
            {
                foreach ((EntityId id, T t) in World.GetComponentEnumerator<T>())
                {
                    U u = World.GetComponent<U>(id); ;
                    if (u != null)
                    {
                        V v = World.GetComponent<V>(id); ;
                        if (v != null)
                        {
                            yield return (t, u, v);
                        }
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class ComponentEnumerator4<T, U, V, W> : ComponentEnumeratorBase, IEnumerable<(T, U, V, W)> where T : Component where U : Component where V : Component where W : Component
        {
            public ComponentEnumerator4(World world) : base(world)
            {
            }

            public IEnumerator<(T, U, V, W)> GetEnumerator()
            {
                foreach ((EntityId id, T t) in World.GetComponentEnumerator<T>())
                {
                    U u = World.GetComponent<U>(id); ;
                    if (u != null)
                    {
                        V v = World.GetComponent<V>(id); ;
                        if (v != null)
                        {
                            W w = World.GetComponent<W>(id); ;
                            if (w != null)
                            {
                                yield return (t, u, v, w);
                            }
                        }
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class ComponentEnumerator5<T, U, V, W, X> : ComponentEnumeratorBase, IEnumerable<(T, U, V, W, X)> where T : Component where U : Component where V : Component where W : Component where X : Component
        {
            public ComponentEnumerator5(World world) : base(world)
            {
            }

            public IEnumerator<(T, U, V, W, X)> GetEnumerator()
            {
                foreach ((EntityId id, T t) in World.GetComponentEnumerator<T>())
                {
                    U u = World.GetComponent<U>(id); ;
                    if (u != null)
                    {
                        V v = World.GetComponent<V>(id); ;
                        if (v != null)
                        {
                            W w = World.GetComponent<W>(id); ;
                            if (w != null)
                            {
                                X x = World.GetComponent<X>(id); ;
                                if (x != null)
                                {
                                    yield return (t, u, v, w, x);
                                }
                            }
                        }
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class ComponentEnumeratorBase
        {
            public ComponentEnumeratorBase(World world)
            {
                this.World = world;
            }

            protected World World { get; private set; }
        }
    }
}
