
namespace HarpoonFishing.Ecs.Systems
{
    using global::System.Collections.Generic;
    using HarpoonFishing.Ecs.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class SpriteRenderSystem : System
    {
        public SpriteRenderSystem(ComponentRequirements componentRequirements, GraphicsDeviceManager graphics, SpriteBatch spriteBatch) :
            base(UpdatePhase.Render, componentRequirements)
        {
            _graphics = graphics;
            _spriteBatch = spriteBatch;
        }

        public override void Update(GameTime gameTime, IEnumerable<Entity> relevantEntites)
        {
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw the sprite.
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            foreach (Entity entity in relevantEntites)
            {
                SpriteComponent spriteComponent = entity.GetComponent<SpriteComponent>();
                TransformComponent positionComponent = entity.GetComponent<TransformComponent>();

                _spriteBatch.Draw(
                    texture : spriteComponent.Texture, 
                    position : positionComponent.Position, 
                    sourceRectangle : null,
                    color : Color.White,
                    rotation : positionComponent.Rotation,
                    origin : new Vector2(0.0f, 0.0f),
                    scale : positionComponent.Scale,
                    effects : SpriteEffects.None,
                    layerDepth : 0.0f);
            }

            _spriteBatch.End();
        }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
    }
}
