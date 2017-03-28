
namespace HarpoonFishing.Ecs
{
    using System;
    using System.Threading;

    struct EntityId
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
