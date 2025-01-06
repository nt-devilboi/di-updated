using TagCloud2.Abstract;

namespace TagCloud2.Infrastructure;

public class InputData(string[] args) : IInputData
{
    public string[] GetArgs()
    {
        return args;
    }
}