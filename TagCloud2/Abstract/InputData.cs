using TagCloud2.Infrastructure;

namespace TagCloud2.Abstract;

public interface IInputData
{
    string[] GetArgs();

   InputData GetVersion() // это можно использовать как штука, в которой будут использованы разные методы отсюда. по сути просто абстрактынй класс в таком случае)) но методы по дефолту все virtual. 
    {
        return new InputData([]);
    }
}