
namespace HarpoonFishing.Ecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HarpoonFishing.Ecs.Components;

    class ComponentRequirements
    {
        public ComponentRequirements()
        {
            ReadOnlyComponents = new List<Type>();
            ReadWriteComponents = new List<Type>();
        }

        public List<Type> ReadOnlyComponents { get; private set; }
        public List<Type> ReadWriteComponents { get; private set; }

        public bool AreSatisfiedBy(Entity entity)
        {
            foreach (Type desiredComonentType in ReadOnlyComponents.Concat(ReadWriteComponents))
            {
                bool matched = false;

                foreach (Component component in entity.Components)
                {
                    if (desiredComonentType == component.GetType())
                    {
                        matched = true;
                        break;
                    }
                }

                if (!matched)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
