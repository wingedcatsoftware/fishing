
namespace HarpoonFishing.Ecs
{
    using global::System;
    using System.Collections.Generic;
    using Systems;

    class SystemRegistration
    {
        public SystemRegistration()
        {
            Requirements = new List<(Type, ComponentUse)>();
        }

        public System System { get; set; }
        public List<(Type, ComponentUse)> Requirements { get; set; }
    }

}
