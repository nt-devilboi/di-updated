using System.Text;
using TagCloud2.Abstract;
using static System.String;

namespace TagsCloudVisuliazation.Test;

public class Logger : ILogger
{
    private readonly StringBuilder _logger = new();

    public void WriteLine(string line)
    {
        _logger.Append(line);
    }

    public string[] GetData()
    {
        if (_logger.Length == 0) return [];
        return _logger.ToString().Split('`');
    }
}