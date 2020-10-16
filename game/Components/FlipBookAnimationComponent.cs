
namespace Game.Components
{
    using global::System;
    using MonoGameEcs;

    class FlipBookAnimationComponent : Component
    {
        public TimeSpan AnimationFrameTime { get; set; }

        public TimeSpan LastFlipTime { get; set; }
    }
}
