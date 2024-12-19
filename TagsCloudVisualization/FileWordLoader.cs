using TagsCloudVisualization.Settings;

namespace TagCloud2;

public class FileWordLoader : ITagCloudWordLoader
{
    private IWordLoaderSettings _wordLoaderSettings;
    public FileWordLoader(AppSettings appSettings)
    {
        _wordLoaderSettings = appSettings.WordLoaderSettings;
    }
    public List<WordPopular> LoadWord()
    {
        var dict = new Dictionary<string, int>();
        using var reader = new StreamReader(_wordLoaderSettings.Path);

        var line = reader.ReadLine();
        while (line != null)
        {
            var lower = line.ToLower();

            dict.TryAdd(lower, 0);
            dict[lower]++;

            line = reader.ReadLine();
        }

        var result = ParseToWordPopular(dict);
        result.Sort((prev, cur) => cur.Count.CompareTo(prev.Count));
        return result;
    }

    private List<WordPopular> ParseToWordPopular(Dictionary<string, int> dictionary) =>
        dictionary.Select(keyPair => new WordPopular(keyPair.Key, keyPair.Value)).ToList();
}