using System.Collections.Immutable;

namespace TagsCloudVisualization;

public interface IWordLoader
{
    ImmutableArray<WordPopular> LoadWord();
}