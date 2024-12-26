namespace TagsCloudVisualization;

public interface IProcessOutputReader : IDisposable
{
    public string ReadLine();

    public IEnumerable<string> ReadLines();
}