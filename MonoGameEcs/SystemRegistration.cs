
namespace MonoGameEcs
{
    using global::System;
    using global::System.Collections.Generic;

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
