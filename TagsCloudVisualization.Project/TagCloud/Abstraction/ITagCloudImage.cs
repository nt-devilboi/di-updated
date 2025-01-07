using System.Drawing;

namespace TagsCloudVisualization.Abstraction;

public interface ITagCloudImage : IDisposable
{
    Size Size();
    void DrawString(RectangleTagCloud rec);
}