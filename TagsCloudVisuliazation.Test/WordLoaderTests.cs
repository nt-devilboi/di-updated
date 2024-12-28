using FakeItEasy;
using FluentAssertions;
using TagsCloudVisualization;
using WeCantSpell.Hunspell;

namespace TagsCloudVisuliazation.Test;

public class WordLoaderTests
{
    [Test]
    public void WordLoader_LoadWord()
    {
        var fakeReader = A.Fake<IProcessOutputReader>();
        var steamReader = new Lazy<IProcessOutputReader>(() => fakeReader);

        A.CallTo(() => steamReader.Value.ReadLines()).Returns(["hello", "hello"]);
        
        var wordLoader = new FileWordLoader(steamReader);
        var words = wordLoader.LoadWord();

        words[0].Should().BeEquivalentTo(new WordPopular("hello", 2));
    }
    
    [Test]
    public void WordLoader_WordNotInDic()
    {
        var fakeReader = A.Fake<IProcessOutputReader>();
        var steamReader = new Lazy<IProcessOutputReader>(() => fakeReader);

        A.CallTo(() => steamReader.Value.ReadLines()).Returns([]);
        
        var wordLoader = new FileWordLoader(steamReader);
        var words = wordLoader.LoadWord();

        words.Should().BeEmpty();
    }
    
    [Test]
    public void WordLoader_WordInLowerCase()
    {
        var fakeReader = A.Fake<IProcessOutputReader>();
        var steamReader = new Lazy<IProcessOutputReader>(() => fakeReader);

        A.CallTo(() => steamReader.Value.ReadLines()).ReturnsNextFromSequence(["HELLO", "hello"]);
        
        var wordLoader = new FileWordLoader(steamReader);
        var words = wordLoader.LoadWord();

        words[0].Should().BeEquivalentTo(new WordPopular("hello", 2));
    }
    
    [Test]
    public void WordLoader_CheckWithTwoWord()
    {
        var fakeReader = A.Fake<IProcessOutputReader>();
        var steamReader = new Lazy<IProcessOutputReader>(() => fakeReader);

        A.CallTo(() => steamReader.Value.ReadLines()).Returns(["hello", "hello", "andrey"]);
        
        var wordLoader = new FileWordLoader(steamReader);
        var words = wordLoader.LoadWord();

        words[0].Should().BeEquivalentTo(new WordPopular("hello", 2));
        words[1].Should().BeEquivalentTo(new WordPopular("andrey", 1));
    }
   
}