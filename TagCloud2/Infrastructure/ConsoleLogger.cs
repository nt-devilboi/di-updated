using TagCloud2.Abstract;

namespace TagCloud2.Infrastructure;

public class ConsoleLogger : ILogger
{
    public void WriteLine(string line)
    {
        Console.WriteLine(line);
    }
}