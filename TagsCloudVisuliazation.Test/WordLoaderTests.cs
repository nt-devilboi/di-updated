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

        A.CallTo(() => steamReader.Value.ReadLine()).ReturnsNextFromSequence("hello", "hello");
        
        var wordList = WordList.CreateFromWords(["hello"]);
   
        var wordLoader = new FileWordLoader(wordList, steamReader);
        var words = wordLoader.LoadWord();

        words[0].Should().Be(new WordPopular("hello", 2));
    }
    
    [Test]
    public void WordLoader_WordNotInDic()
    {
        var fakeReader = A.Fake<IProcessOutputReader>();
        var steamReader = new Lazy<IProcessOutputReader>(() => fakeReader);

        A.CallTo(() => steamReader.Value.ReadLine()).ReturnsNextFromSequence("hello", "hello");
        
        var wordList = WordList.CreateFromWords([]);
   
        var wordLoader = new FileWordLoader(wordList, steamReader);
        var words = wordLoader.LoadWord();

        words.Should().BeEmpty();
    }
    
    [Test]
    public void WordLoader_WordInLowerCase()
    {
        var fakeReader = A.Fake<IProcessOutputReader>();
        var steamReader = new Lazy<IProcessOutputReader>(() => fakeReader);

        A.CallTo(() => steamReader.Value.ReadLine()).ReturnsNextFromSequence("HELLO", "hello");
        
        var wordList = WordList.CreateFromWords(["hello"]);
   
        var wordLoader = new FileWordLoader(wordList, steamReader);
        var words = wordLoader.LoadWord();

        words[0].Should().Be(new WordPopular("hello", 2));
    }
    
    [Test]
    public void WordLoader_CheckOtherWord()
    {
        var fakeReader = A.Fake<IProcessOutputReader>();
        var steamReader = new Lazy<IProcessOutputReader>(() => fakeReader);

        A.CallTo(() => steamReader.Value.ReadLines()).Returns(["hello", "hello", "andrey"]);
        
        var wordList = WordList.CreateFromWords(["hello", "andrey"]);
   
        var wordLoader = new FileWordLoader(wordList, steamReader);
        var words = wordLoader.LoadWord();

        words[0].Should().BeEquivalentTo(new WordPopular("hello", 2));
        words[1].Should().BeEquivalentTo(new WordPopular("andrey", 1));
    }
   
}