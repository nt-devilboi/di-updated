using System.Drawing;

namespace TagsCloudVisualization.Settings;

public interface ITagCloudSettings
{
    public Point Center => new Point(Size.Width / 2, Size.Height / 2);
    public Size Size { get; set; }
    public string PathDirectory { get; set; }
    public string NameFile { get; set; }
}