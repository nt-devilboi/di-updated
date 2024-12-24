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
        var frequency = new Dictionary<string, int>();
        
        var line = reader.ReadLine();
        while (!string.IsNullOrEmpty(line))
        {
            if (!IsBoring(line))
            {
                var lower = line.Split('=').First().ToLower();
                if (wordList.Check(lower))
                {
                    frequency.TryAdd(lower, 0);
                    frequency[lower]++;
                }
            }


            line = reader.ReadLine();
        }

        var result = ParseToWordPopular(frequency);
        result.Sort((prev, cur) => cur.Count.CompareTo(prev.Count));
        return result;
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