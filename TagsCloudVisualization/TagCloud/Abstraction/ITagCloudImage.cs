using System.Drawing;

namespace TagsCloudVisualization;

public interface ITagCloudImage : IDisposable
{
    Size Size();
    void DrawString(RectangleTagCloud rec);
    void Save();
}