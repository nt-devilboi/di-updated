using System.Drawing;

namespace TagCloud2;

public class RectangleTagCloud
{
    public Font font = new("Aria", 24, FontStyle.Bold);
    public string text { get; }
    public Rectangle Rectangle;

    public RectangleTagCloud(Rectangle rectangle, string text)
    {
        Rectangle = rectangle;
        this.text = text;
    }
}