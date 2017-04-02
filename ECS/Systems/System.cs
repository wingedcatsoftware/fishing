
namespace HarpoonFishing.Ecs.Systems
{
    using Microsoft.Xna.Framework;

    class System
    {
        public System(UpdatePhase phase)
        {
            this.UpdatePhase = phase;
        }

        public UpdatePhase UpdatePhase { get; private set; }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
