
namespace HarpoonFishing.Ecs.Components
{
    using System;

    class FlipBookAnimationComponent : Component
    {
        public TimeSpan AnimationFrameTime { get; set; }

        public TimeSpan LastFlipTime { get; set; }
    }
}
