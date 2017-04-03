
namespace HarpoonFishing.Ecs.Systems
{
    using global::System.Collections.Generic;
    using HarpoonFishing.Ecs.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class SpriteRenderSystem : System
    {
        public SpriteRenderSystem(World world, GraphicsDeviceManager graphics, SpriteBatch spriteBatch) :
            base(UpdatePhase.Render)
        {
            _graphics = graphics;
            _spriteBatch = spriteBatch;

            _entityEnumerator = world.RegisterSystem<TransformComponent, SpriteComponent>(this, ComponentUse.Read, ComponentUse.Read);
        }

        public override void Update(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw the sprite.
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            foreach ((TransformComponent transformComponent, SpriteComponent spriteComponent) in _entityEnumerator)
            {
                Point spriteSheetPositions = new Point(spriteComponent.SheetSize.X / spriteComponent.Size.X, spriteComponent.SheetSize.Y / spriteComponent.Size.Y);

                Rectangle sourceRectangle = new Rectangle(0, 0, spriteComponent.Size.X, spriteComponent.Size.Y);
                sourceRectangle.Offset(sourceRectangle.Width * spriteComponent.PositionInSheet.X, sourceRectangle.Height * spriteComponent.PositionInSheet.Y);

                _spriteBatch.Draw(
                    texture : spriteComponent.Texture, 
                    position : transformComponent.Position, 
                    sourceRectangle : sourceRectangle,
                    color : Color.White,
                    rotation : transformComponent.Rotation,
                    origin : new Vector2(0.0f, 0.0f),
                    scale : transformComponent.Scale,
                    effects : SpriteEffects.None,
                    layerDepth : 0.0f);
            }

            _spriteBatch.End();
        }

        private IEnumerable<(TransformComponent, SpriteComponent)> _entityEnumerator;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
    }
}
