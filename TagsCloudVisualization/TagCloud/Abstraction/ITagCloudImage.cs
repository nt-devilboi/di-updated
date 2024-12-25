using System.Drawing;

namespace TagsCloudVisualization;

public interface ITagCloudImage : IDisposable
{
    Size Size();
    void Draw(Rectangle rec);
    void DrawString(RectangleTagCloud rec);
    void Save();
}