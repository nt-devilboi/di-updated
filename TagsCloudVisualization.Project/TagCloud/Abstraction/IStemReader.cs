namespace TagsCloudVisualization;

public interface IStemReader : IDisposable
{
    public string ReadLine();

    public IEnumerable<string> ReadLines();
}