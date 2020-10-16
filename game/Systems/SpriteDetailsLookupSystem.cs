namespace Game.Systems
{
    using global::Game.Components;
    using global::System;
    using global::System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using MonoGameEcs;

    class SpriteDetailsLookupSystem : System
    {
        public SpriteDetailsLookupSystem(World world) :
           base(UpdatePhase.Main)
        {
            _spriteSheetsEnumerator = world.RegisterDependencies<SpriteSheetComponent>(this, ComponentUse.Read);
            _entityEnumerator = world.RegisterDependencies<SpriteComponent>(this, ComponentUse.ReadWrite);
        }

        public override void Update(GameTime gameTime, Queue<ICommand> startOfFrameQueue)
        {
            foreach ((EntityId id, SpriteComponent spriteComponent) in _entityEnumerator)
            {
                var spriteSheetComponent = LookupSpriteSheet(spriteComponent.SpriteSheetName);

                var spriteSheetEntry = LookupSpriteSheetEntry(spriteSheetComponent, spriteComponent.SpriteEntryName);

                var spriteDetailsComponent = new SpriteDetailsComponent()
                {
                    Position = spriteSheetEntry.Position,
                    Size = spriteSheetEntry.Size,
                    Texture = spriteSheetComponent.Texture
                };

                startOfFrameQueue.Enqueue(new RemoveComponentCommand() { Id = id, Component = spriteComponent });
                startOfFrameQueue.Enqueue(new AddComponentCommand() { Id = id, Component = spriteDetailsComponent });
            }
        }

        private SpriteSheetComponent LookupSpriteSheet(string name)
        {
            foreach (var (EntityId, spriteSheetComponent) in _spriteSheetsEnumerator)
            {
                if (spriteSheetComponent.Name == name) return spriteSheetComponent;
            }
            throw new ArgumentOutOfRangeException("name");
        }

        private SpriteSheetEntry LookupSpriteSheetEntry(SpriteSheetComponent spriteSheetComponent, string name)
        {
            foreach (var spriteSheetEntry in spriteSheetComponent.Entries)
            {
                if (spriteSheetEntry.Name == name)
                {
                    return spriteSheetEntry;
                }
            }
            throw new ArgumentOutOfRangeException("name");
        }

        private IEnumerable<(EntityId, SpriteSheetComponent)> _spriteSheetsEnumerator;
        private IEnumerable<(EntityId, SpriteComponent)> _entityEnumerator;
    }
}
