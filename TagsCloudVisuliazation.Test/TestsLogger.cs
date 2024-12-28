using System.Text;
using TagCloud2;
using TagCloud2.Abstract;
using static System.String;

namespace TagsCloudVisuliazation.Test;

public class TestsLogger : ILogger
{
    private readonly StringBuilder _logger = new();

    public void WriteLine(string line)
    {
        _logger.AppendLine(line);
    }

    public string GetData()
    {
        return Join("", _logger);
    }
}