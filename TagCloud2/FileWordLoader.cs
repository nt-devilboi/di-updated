namespace TagCloud2;

public class FileWordLoader : ITagCloudWordLoader
{
    public List<WordPopular> LoadWord(string path)
    {
        var dict = new Dictionary<string, int>();
        using var reader = new StreamReader(path);

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