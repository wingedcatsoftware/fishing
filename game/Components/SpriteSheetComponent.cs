
namespace Game.Components
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGameEcs;

    class SpriteSheetEntry
    {
        public string Name { get; set; }

        public Point Position { get; set; }

        public Point Size { get; set; }
    }

    class SpriteSheetComponent : Component
    {
        public string Name { get; set; }

        public Texture2D Texture { get; set; }

        public SpriteSheetEntry[] Entries { get; set; }
    }

}
