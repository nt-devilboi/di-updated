using TagCloud2.Abstract;

namespace TagCloud2;

public class ConsoleLogger : ILogger
{
    public void WriteLine(string line)
    {
        Console.WriteLine(line); 
    }
}