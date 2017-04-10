
namespace HarpoonFishing.Systems
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using HarpoonFishing.Components;
    using Microsoft.Xna.Framework;
    using MonoGameEcs;

    using ComponentList = global::System.Collections.Generic.List<MonoGameEcs.Component>;

    class FishPopulationSystem : System
    {
        public FishPopulationSystem(World world, TextureSource textureSource) :
            base(UpdatePhase.Main)
        {
            _world = world;
            _textureSource = textureSource;
            _fishDemographicsEnumerator = world.RegisterSystem<FishDemographicsComponent>(this, ComponentUse.ReadWrite);
            _entityEnumerator = world.RegisterSystem<FishDemographicDescriptionComponent>(this, ComponentUse.ReadWrite);
            _random = new Random();
        }

        public override void Update(GameTime gameTime)
        {
            if (_fishDemographicsEnumerator.Any())
            {
                FishDemographicsComponent demographics = _fishDemographicsEnumerator.First();

                if (_entityEnumerator.Count() < demographics.PopulationMax)
                {
                    double secondsForFrame = gameTime.ElapsedGameTime.TotalSeconds;
                    if (_random.NextDouble() <= demographics.SpawnChancePerSecond * secondsForFrame)
                    {
                        SpawnFish();
                    }
                }
            }
        }

        private void SpawnFish()
        {
            EntityId id = EntityId.NewId();
            var transformComponent = new TransformComponent();
            transformComponent.Position = new Vector2(200.0f + (float)_random.NextDouble() * 400.0f - 200.0f, 200.0f + (float)_random.NextDouble() * 400.0f - 200.0f);
            transformComponent.Scale = new Vector2(0.25f, 0.25f);

            var spriteComponent = new SpriteComponent();
            spriteComponent.Texture = _textureSource.Lookup("green-fish-rest-to-right-sheet");
            spriteComponent.Size = new Point(256, 256);
            spriteComponent.SheetSize = new Point(1536, 256);
            spriteComponent.PositionInSheet = new Point(_random.Next(spriteComponent.SheetSize.X / spriteComponent.Size.X), 0);

            var flipBookAnimationComponent = new FlipBookAnimationComponent();
            flipBookAnimationComponent.AnimationFrameTime = TimeSpan.FromMilliseconds(100);

            var fishDemographicDescriptionComponent = new FishDemographicDescriptionComponent();

            _world.AddEntity(id, new ComponentList() { transformComponent, spriteComponent, flipBookAnimationComponent, fishDemographicDescriptionComponent });
        }

        private World _world;
        private TextureSource _textureSource;
        private IEnumerable<FishDemographicsComponent> _fishDemographicsEnumerator;
        private IEnumerable<FishDemographicDescriptionComponent> _entityEnumerator;
        private Random _random;
    }
}
