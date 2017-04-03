
namespace HarpoonFishing.Ecs.Components
{
    using System;

    class FlipBookAnimationComponent : Component
    {
        public FlipBookAnimationComponent(World world, EntityId id) : base(world, id)
        {
        }

        public TimeSpan AnimationFrameTime { get; set; }

        public TimeSpan LastFlipTime { get; set; }
    }
}
