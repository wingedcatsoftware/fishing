﻿
namespace HarpoonFishing.Components
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGameEcs;

    class SpriteComponent : Component
    {
        public Texture2D Texture { get; set; }

        public Point Size { get; set; }

        public Point SheetSize { get; set; }

        public Point PositionInSheet { get; set; }
    }
}