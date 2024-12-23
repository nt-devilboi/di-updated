using System.Diagnostics;
using System.Text;
using TagsCloudVisualization.Settings;
using WeCantSpell.Hunspell;

namespace TagsCloudVisualization;

public class FileWordLoader(WordLoaderSettings wordLoaderSettings) : IWordLoader
{
    public List<WordPopular> LoadWord()
    {
        var dict = new Dictionary<string, int>();
        var stem = MyStem();

        using var reader = stem.StandardOutput;
        var wordList = WordList.CreateFromFiles("./../../../ru_Ru.dic");
        var line = reader.ReadLine();
        while (line != null)
        {
            if (!IsBoring(line))
            {
                var lower = line.Split('=').First().ToLower();
                if (wordList.Check(lower))
                {
                    dict.TryAdd(lower, 0);
                    dict[lower]++;
                }
            }


            line = reader.ReadLine();
        }

        stem.Close();
        var result = ParseToWordPopular(dict);
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

    private Process MyStem()
    {
        var stem = new Process
        {
            StartInfo =
            {
                FileName = wordLoaderSettings.PathStem,
                Arguments = $"-nli {wordLoaderSettings.Path}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8
            }
        };

        stem.Start();

        return stem;
    }
}