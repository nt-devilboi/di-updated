using System.Drawing;

namespace TagsCloudVisualization.Abstraction;

public interface ISizeWord
{
    Size GetSizeWord(string word, int emSize);
}