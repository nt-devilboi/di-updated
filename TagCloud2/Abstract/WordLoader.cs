namespace TagCloud2;

public interface ITagCloudWordLoader
{
    List<WordPopular> LoadWord(string path);
}