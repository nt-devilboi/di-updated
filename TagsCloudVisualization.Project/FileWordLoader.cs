using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization;

public class FileWordLoader(
    FactoryStem steamReader)
    : IWordLoader
{
    public IEnumerable<FrequencyWord> LoadWords()
    {
        return steamReader.Create().GetValueOrThrow()
            .ReadLines()
            .Where(ValidateLexeme)
            .Select(GetLemma)
            .ToFrequencyPopular()
            .ToList();
    }

    private static bool ValidateLexeme(string x)
    {
        return !string.IsNullOrEmpty(x) && !IsBoring(x);
    }

    private static string GetLemma(string l)
    {
        return l.Split('=').First().ToLower();
    }

    private static bool IsBoring(string line)
    {
        return line.Contains("=PR=") || line.Contains("=PART=") || line.Contains("=CONJ=");
    }
}