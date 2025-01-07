using System.Drawing;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Settings;

namespace TagCloud2.Infrastructure;

public class MeasureString(TagCloudSettings tagCloudSettings) : ISizeWord
{
    private readonly Graphics _graphics = Graphics.FromImage(new Bitmap(1, 1));

    public Size GetSizeWord(string word, int emSize)
    {
        return _graphics.MeasureString(word, GetFont(emSize)).ToSize();
    }

    private Font GetFont(int emSize)
    {
        return new Font(tagCloudSettings.Font, emSize);
    }
}