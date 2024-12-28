using TagCloud2.Abstract;

namespace TagCloud2;

public class InputData : IInputData
{
    public string[] ReadArgs() => Console.ReadLine().Split(" ");
}