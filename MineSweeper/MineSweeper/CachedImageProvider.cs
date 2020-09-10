using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MineSweeper
{
    public class CachedImageProvider : IImageProvider
    {
        private Dictionary<string, Image> _cache = new Dictionary<string, Image>();

        public Image GetImage(string imageName)
        {
            var extension = "";
            if (!imageName.Contains("."))
                extension = ".png";

            if (!_cache.ContainsKey(imageName))
                _cache.Add(imageName, Image.FromFile($"../../Images/{imageName}{extension}"));

            return _cache[imageName];
        }
    }
}