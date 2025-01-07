namespace TagsCloudVisualization.Extensions;

public static class PopularWordExtension
{
    public static IEnumerable<FrequencyWord> ToFrequencyPopular(this IEnumerable<string> s)
    {
        var dict = new Dictionary<string, FrequencyWord>();
        foreach (var line in s)
        {
            if (dict.TryAdd(line, new FrequencyWord(line, 0))) yield return dict[line];

            dict[line].Count++;
        }
    }
}