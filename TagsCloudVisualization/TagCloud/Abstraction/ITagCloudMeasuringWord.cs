using System.Drawing;

namespace TagsCloudVisualization.Settings;

public interface ITagCloudMeasuringWord
{
    SizeF GetSizeWord(WordPopular word);
}