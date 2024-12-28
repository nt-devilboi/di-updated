using System.Collections.Immutable;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization;

public class FileWordLoader(
    Lazy<IProcessOutputReader> steamReader)
    : IWordLoader
{
    public ImmutableArray<WordPopular> LoadWord()
    {
        return Sort(steamReader.Value.ReadLines()
            .Where(ValidateLexeme)
            .Select(GetLemma)
            .ToWordPopular()
            .ToList());
    }
    
    private ImmutableArray<WordPopular> Sort(List<WordPopular> list)
    {
        list.Sort((prev, cur) => cur.Count.CompareTo(prev.Count));
        return [..list];
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
        return line.Contains("PR") || line.Contains("PART") || line.Contains("CONJ");
    }
}