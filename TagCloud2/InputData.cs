using TagCloud2.Abstract;

namespace TagCloud2;

public class InputData(string[] args) : IInputData
{
    public string[] ReadArgs() => args;
}