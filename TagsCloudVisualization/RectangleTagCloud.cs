using System.Drawing;

namespace TagsCloudVisualization;

public class RectangleTagCloud
{
    public Font font = new("Times New Roman", 24, FontStyle.Bold);
    public Rectangle Rectangle;

    public RectangleTagCloud(Rectangle rectangle, string text)
    {
        Rectangle = rectangle;
        this.text = text;
    }

    public string text { get; }
}