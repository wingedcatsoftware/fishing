
namespace HarpoonFishing
{
    using HarpoonFishing.Components;
    using HarpoonFishing.Systems;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoGameEcs;

    using ComponentList = System.Collections.Generic.List<MonoGameEcs.Component>;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class HarpoonFishingGame : Game
    {
        public HarpoonFishingGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _world = new World();

            Content.RootDirectory = "Content";
            _textureSource = new TextureSource(Content);
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
            new FishPopulationSystem(_world, _textureSource);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _textureSource.Load("green-fish-rest-to-right-sheet");

            // Don't create the demographics until all the assets are loaded as that will start fish spawning.
            EntityId id = EntityId.NewId();
            var fishDemographicsComponent = new FishDemographicsComponent();
            fishDemographicsComponent.PopulationMax = 10;
            fishDemographicsComponent.SpawnChancePerSecond = 0.1;
            _world.AddEntity(id, new ComponentList() { fishDemographicsComponent });
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

            _world.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            _world.Render(gameTime);

            base.Draw(gameTime);
        }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private World _world;
        private TextureSource _textureSource;
    }
}
