
namespace Game.Systems
{
    using global::System.Collections.Generic;
    using global::Game.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGameEcs;

    class SpriteRenderSystem : System
    {
        public SpriteRenderSystem(World world, GraphicsDeviceManager graphics, SpriteBatch spriteBatch) :
            base(UpdatePhase.Render)
        {
            _graphics = graphics;
            _spriteBatch = spriteBatch;

            _entityEnumerator = world.RegisterDependencies<TransformComponent, SpriteDetailsComponent>(this, ComponentUse.Read, ComponentUse.Read);
        }

        public override void Update(GameTime gameTime, Queue<ICommand> startOfFrameQueue)
        {
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw the sprite.
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            foreach ((EntityId id, TransformComponent transformComponent, SpriteDetailsComponent spriteDetailsComponent) in _entityEnumerator)
            {
                Rectangle sourceRectangle = new Rectangle(0, 0, spriteDetailsComponent.Size.X, spriteDetailsComponent.Size.Y);

                sourceRectangle.Offset(spriteDetailsComponent.Position.X, spriteDetailsComponent.Position.Y);

                _spriteBatch.Draw(
                    texture : spriteDetailsComponent.Texture, 
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

        private IEnumerable<(EntityId, TransformComponent, SpriteDetailsComponent)> _entityEnumerator;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
    }
}
