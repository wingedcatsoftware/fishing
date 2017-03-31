
namespace HarpoonFishing.Ecs.Systems
{
    using global::System.Collections.Generic;
    using Microsoft.Xna.Framework;

    class System
    {
        public System(UpdatePhase phase, ComponentRequirements componentRequirements)
        {
            this.UpdatePhase = phase;
            this.ComponentRequirements = componentRequirements;
        }

        public ComponentRequirements ComponentRequirements { get; private set; }

        public UpdatePhase UpdatePhase { get; private set; }

        public virtual void Update(GameTime gameTime, IEnumerable<Entity> relevantEntites)
        {
        }
    }
}
