
namespace MonoGameEcs
{
    using global::System.Collections.Generic;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class TextureSource
    {
        public TextureSource(ContentManager contentManager)
        {
            _contentManager = contentManager;
            _nameToTextureMap = new Dictionary<string, Texture2D>();
        }

        public void Load(string textureName)
        {
            Texture2D texture = _contentManager.Load<Texture2D>(textureName);
            _nameToTextureMap.Add(textureName, texture);
        }

        public Texture2D Lookup(string textureName)
        {
            return _nameToTextureMap[textureName];
        }

        private ContentManager _contentManager;
        private Dictionary<string, Texture2D> _nameToTextureMap;
    }
}
