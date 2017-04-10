
namespace MonoGameEcs
{
    using global::System;
    using global::System.Threading;

    public enum UpdatePhase
    {
        Main,
        Render,
    }

    public struct EntityId
    {
        public static EntityId NewId()
        {
            EntityId newId;
            newId._id = _random.Value.Next();
            return newId;
        }

        private int _id;

        private static int _seed = Environment.TickCount;
        private static readonly ThreadLocal<Random> _random = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));
    }
}
