
namespace Game
{
    using global::Game.Components;
    using global::Game.Systems;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using MonoGameEcs;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        public Game()
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
            new SpriteDetailsLookupSystem(_world);
            new SpriteRenderSystem(_world, _graphics, _spriteBatch);
//            new FlipBookAnimationSystem(_world);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _textureSource.Load("forest_pack");
            _textureSource.Load("green-fish-rest-to-right-sheet");

            // Spawn some test entities
            var id = EntityId.NewId();
            var spriteSheetComponent = new SpriteSheetComponent()
            {
                Name = "terrain",
                Texture = _textureSource.Lookup("forest_pack"),
                Entries = new SpriteSheetEntry[]
                {
                    new SpriteSheetEntry {
                        Name = "ground middle",
                        Position = new Point(152, 144),
                        Size = new Point(128, 128)
                    }
                }
            };
            _world.AddEntity(id, spriteSheetComponent);


            id = EntityId.NewId();
            var spriteComponent = new SpriteComponent()
            {
                SpriteSheetName = "terrain",
                SpriteEntryName = "ground middle"
            };
            var transformComponent = new TransformComponent()
            {
                Position = new Vector2(10, 10)
            };
            var floorComponent = new FloorComponent();
            _world.AddEntity(id, spriteComponent, transformComponent, floorComponent);
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
