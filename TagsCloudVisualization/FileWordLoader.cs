using System.Collections.Immutable;
using TagsCloudVisualization.Extensions;
using WeCantSpell.Hunspell;

namespace TagsCloudVisualization;

public class FileWordLoader(
    WordList wordList,
    Lazy<IProcessOutputReader> steamReader)
    : IWordLoader
{
    public ImmutableArray<WordPopular> LoadWord()
    {
        var reader = steamReader.Value;
        var result = GetCorrectWord(reader).ToList();
        result.Sort((prev, cur) => cur.Count.CompareTo(prev.Count));
        return [..result];
    }

    private IEnumerable<WordPopular> GetCorrectWord(IProcessOutputReader reader)
    {
        return reader.ReadLines().Where(x => !string.IsNullOrEmpty(x) && !IsBoring(x))
            .Select(GetWord).Where(wordList.Check).ToWordPopular();
    }

    private static string GetWord(string l)
    {
        return l.Split('=').First().ToLower();
    }

    private static bool IsBoring(string line)
    {
        return line.Contains("PR") || line.Contains("PART") || line.Contains("CONJ");
    }
}