namespace TagsCloudVisualization.Extensions;

public static class PopularWordExtension
{
    public static IEnumerable<WordPopular> ToWordPopular(this IEnumerable<string> s)
    {
        var dict = new Dictionary<string, WordPopular>();
        foreach (var line in s)
        {
            if (dict.TryAdd(line, new WordPopular(line, 0)))
            {
                yield return dict[line];
            }

            dict[line].Count++;
        }
    }
}