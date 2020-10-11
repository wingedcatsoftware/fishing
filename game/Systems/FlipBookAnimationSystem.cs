
namespace HarpoonFishing.Systems
{
    using global::System;
    using global::System.Collections.Generic;
    using HarpoonFishing.Components;
    using Microsoft.Xna.Framework;
    using MonoGameEcs;

    class FlipBookAnimationSystem : System
    {
        public FlipBookAnimationSystem(World world) :
            base(UpdatePhase.Main)
        {
            _entityEnumerator = world.RegisterSystem<FlipBookAnimationComponent, SpriteComponent>(this, ComponentUse.ReadWrite, ComponentUse.ReadWrite);
        }

        public override void Update(GameTime gameTime)
        {
            foreach ((FlipBookAnimationComponent flipBookAnimationComponent, SpriteComponent spriteComponent) in _entityEnumerator)
            {
                TimeSpan timeSinceFlip = gameTime.TotalGameTime - flipBookAnimationComponent.LastFlipTime;
                if (timeSinceFlip >= flipBookAnimationComponent.AnimationFrameTime)
                {
                    Point spriteSheetPositions = new Point(spriteComponent.SheetSize.X / spriteComponent.Size.X, spriteComponent.SheetSize.Y / spriteComponent.Size.Y);
                    int currentSpriteIndex = spriteSheetPositions.X * spriteComponent.PositionInSheet.Y + spriteComponent.PositionInSheet.X;
                    int newSpriteIndex = (currentSpriteIndex + 1) % (spriteSheetPositions.X * spriteSheetPositions.Y);

                    // Update the position in the sprite sheet to read the sprite from as well as the last flip time
                    spriteComponent.PositionInSheet = new Point(newSpriteIndex % spriteSheetPositions.X, newSpriteIndex / spriteSheetPositions.X);
                    flipBookAnimationComponent.LastFlipTime = gameTime.TotalGameTime;
                }
            }
        }

        private IEnumerable<(FlipBookAnimationComponent, SpriteComponent)> _entityEnumerator;
    }
}
