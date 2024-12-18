using System.Drawing;

namespace TagCloud2;

public record SettingsTagCloud(Size Size, string PathDirectory, string NameFile)
{
    public Point Center => new Point(Size.Width / 2, Size.Height / 2);
}