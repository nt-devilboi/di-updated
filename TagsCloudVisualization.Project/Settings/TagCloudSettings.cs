using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.Settings;

public class TagCloudSettings
{
    public Size Size { get; set; }

    public string PathDirectory { get; set; }

    public string NamePhoto { get; set; }

    public Point Center => new(Size.Width / 2, Size.Height / 2);

    public int EmSize { get; set; }

    public Color ColorWords { get; set; }

    public Color BackGround { get; set; }

    public ImageFormat ImageFormat { get; set; }

    public string Font { get; set; }
}