
namespace HarpoonFishing.Ecs
{
    using System;
    using System.Collections.Generic;

    class ComponentRequirements
    {
        public ComponentRequirements()
        {
            ReadOnlyComponents = new List<Type>();
            ReadWriteComponents = new List<Type>();
        }

        public List<Type> ReadOnlyComponents { get; private set; }
        public List<Type> ReadWriteComponents { get; private set; }
    }
}
