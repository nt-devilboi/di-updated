namespace TagCloud2;

public class WordPopular
{
    public string Word { get; }
    public int Count { get;  }

    public WordPopular(string word, int count)
    {
        Word = word;
        Count = count;
    }
}