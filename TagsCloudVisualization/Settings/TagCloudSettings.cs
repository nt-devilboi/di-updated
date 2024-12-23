using System.Drawing;

namespace TagsCloudVisualization.Settings;

public class TagCloudSettings
{
    public Size Size { get; set; }

    public string PathDirectory { get; set; }

    public string NamePhoto { get; set; }

    public Point Center => new(Size.Width / 2, Size.Height / 2);
}