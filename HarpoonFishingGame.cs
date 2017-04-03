
namespace HarpoonFishing
{
    using System;
    using HarpoonFishing.Ecs;
    using HarpoonFishing.Ecs.Systems;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using HarpoonFishing.Ecs.Components;

    using ComponentList = System.Collections.Generic.List<Ecs.Components.Component>;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class HarpoonFishingGame : Game
    {
        public HarpoonFishingGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _world = new World();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Systems register themselves with the world, so no need to hang on to them.
            new SpriteRenderSystem(_world, _graphics, _spriteBatch);
            new FlipBookAnimationSystem(_world);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Texture2D greenFishTexture = Content.Load<Texture2D>("green-fish-rest-to-right-sheet");

            // TEMP create a test entity
            EntityId id = EntityId.NewId();
            var transformComponent = new TransformComponent(_world, id);
            transformComponent.Position = new Vector2(50.0f, 50.0f);
            transformComponent.Scale = new Vector2(0.25f, 0.25f);

            var spriteComponent = new SpriteComponent(_world, id);
            spriteComponent.Texture = greenFishTexture;
            spriteComponent.Size = new Point(256, 256);
            spriteComponent.SheetSize = new Point(1536, 256);
            spriteComponent.PositionInSheet = new Point(1, 0);

            var flipBookAnimationComponent = new FlipBookAnimationComponent(_world, id);
            flipBookAnimationComponent.AnimationFrameTime = TimeSpan.FromMilliseconds(100);

            ComponentList components = new ComponentList();
            components.Add(transformComponent);
            components.Add(spriteComponent);
            components.Add(flipBookAnimationComponent);

            _world.AddEntity(id, components);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _world.ProcessPhase(gameTime, UpdatePhase.Main);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            _world.ProcessPhase(gameTime, UpdatePhase.Render);

            base.Draw(gameTime);
        }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private World _world;
    }
}
