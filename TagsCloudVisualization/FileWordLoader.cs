using TagsCloudVisuliazation.Test.Extension;
using WeCantSpell.Hunspell;

namespace TagsCloudVisualization;

public class FileWordLoader(
    WordList wordList,
    Lazy<IProcessOutputReader> steamReader)
    : IWordLoader
{
    public List<WordPopular> LoadWord()
    {
        var reader = steamReader.Value;
        var result = GetCorrectWord(reader).ToList();
        result.Sort((prev, cur) => cur.Count.CompareTo(prev.Count));
        return result;
    }

    private IEnumerable<WordPopular> GetCorrectWord(IProcessOutputReader reader)
    {
        return reader.ReadLines().Where(x => !string.IsNullOrEmpty(x) && !IsBoring(x))
            .Select(GetWord).Where(wordList.Check).ToWordPopular();
    }

    private WordPopular? ToWordPopular(IDictionary<string, WordPopular> wordPopulars, string s)
    {
        if (wordPopulars.TryAdd(s, new WordPopular(s, 0)))
        {
            wordPopulars[s].Count++;
            return wordPopulars[s];
        }

        wordPopulars[s].Count++;
        return null;
    }

    private static string GetWord(string l)
    {
        return l.Split('=').First().ToLower();
    }

    private static bool IsBoring(string line)
    {
        return line.Contains("PR") || line.Contains("PART") || line.Contains("CONJ");
    }

    private List<WordPopular> ParseToWordPopular(Dictionary<string, int> dictionary)
    {
        return dictionary.Select(keyPair => new WordPopular(keyPair.Key, keyPair.Value)).ToList();
    }
}