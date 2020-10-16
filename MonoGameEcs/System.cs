
namespace MonoGameEcs
{
    using Microsoft.Xna.Framework;
    using global::System.Collections.Generic;

    public class System
    {
        public System(UpdatePhase phase)
        {
            this.UpdatePhase = phase;
        }

        public UpdatePhase UpdatePhase { get; private set; }

        public virtual void Update(GameTime gameTime, Queue<ICommand> startOfFrameQueue)
        {
        }
    }
}
