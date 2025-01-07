using System.Collections.Immutable;

namespace TagsCloudVisualization;

public interface IWordLoader
{
    IEnumerable<FrequencyWord> LoadWords();
}