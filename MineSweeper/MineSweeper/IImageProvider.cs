using System.Drawing;

namespace MineSweeper
{
    public interface IImageProvider
    {
        Image GetImage(string imageName);
    }
}