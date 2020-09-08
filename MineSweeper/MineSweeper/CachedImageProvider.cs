using System.Collections.Generic;
using System.Drawing;

namespace MineSweeper
{
    public class CachedImageProvider : IImageProvider
    {
        private Dictionary<string, Image> _cache = new Dictionary<string, Image>();

        public Image GetImage(string imageName)
        {
            if (!_cache.ContainsKey(imageName))
                _cache.Add(imageName, Image.FromFile($"../..Images/{imageName}.png"));

            return _cache[imageName];
        }
    }
}