using System.Diagnostics;
using System.Text;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

public class StemReader : IProcessOutputReader
{
    private readonly Process _process;
    private readonly StreamReader _streamReader;

    public StemReader(WordLoaderSettings wordLoaderSettings)
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


        _process = stem;
        _process.Start();
        _streamReader = _process.StandardOutput;
    }

    public void Dispose()
    {
        _process.Close();
    }

    public string ReadLine()
    {
        return _streamReader.ReadLine();
    }

    public IEnumerable<string> ReadLines()
    {
        var line = _streamReader.ReadLine();
        while (!string.IsNullOrEmpty(line))
        {
            yield return line;
            line = ReadLine();
        }
    }
}