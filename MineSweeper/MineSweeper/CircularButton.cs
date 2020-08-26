using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MineSweeper
{
    /// <summary>
    /// Makes a circular version of the Forms Button.
    /// </summary>
    internal class CircularButton : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            Region = new System.Drawing.Region(grPath);
            base.OnPaint(pevent);
            grPath.Dispose();
        }
    }
}
