using FakeItEasy;
using FluentAssertions;
using TagsCloudVisualization;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Result;

namespace TagsCloudVisuliazation.Test;

public class WordLoaderTests
{
    private FileWordLoader _fileWordLoader;
    private IStemReader _fakeStemReader;

    [SetUp]
    public void SetUp()
    {
        var fakeFactory = A.Fake<FactoryStem>();
        _fakeStemReader = A.Fake<IStemReader>();
        _fileWordLoader = new FileWordLoader(fakeFactory);

        A.CallTo(() => fakeFactory.Create()).Returns(_fakeStemReader.AsResult());
        
    }

    [Test]
    public void WordLoader_LoadWord()
    {
        A.CallTo(() => _fakeStemReader.ReadLines()).Returns(["hello", "hello"]);
        
        var words = _fileWordLoader.LoadWord();

        words[0].Should().BeEquivalentTo(new WordPopular("hello", 2));
    }

    [Test]
    public void WordLoader_WordNotInDic()
    {
        A.CallTo(() => _fakeStemReader.ReadLines()).Returns([]);

        var words = _fileWordLoader.LoadWord();
        words.Should().BeEmpty();
    }

    [Test]
    public void WordLoader_WordInLowerCase()
    {
        A.CallTo(() => _fakeStemReader.ReadLines()).ReturnsNextFromSequence(["HELLO", "hello"]);

        var words = _fileWordLoader.LoadWord();

        words[0].Should().BeEquivalentTo(new WordPopular("hello", 2));
    }

    [Test]
    public void WordLoader_CheckWithTwoWord()
    {
        A.CallTo(() => _fakeStemReader.ReadLines()).Returns(["hello", "hello", "andrey"]);
        var words = _fileWordLoader.LoadWord();

        words[0].Should().BeEquivalentTo(new WordPopular("hello", 2));
        words[1].Should().BeEquivalentTo(new WordPopular("andrey", 1));
    }
    
    [Test]
    public void WordLoader_ShouldSkipBoringWord()
    {
        A.CallTo(() => _fakeStemReader.ReadLines()).Returns(["на=PR=|на=PART=", 
            "и=CONJ=|и=INTJ=|и=S",
            "hello",
            "andrey", "от=PR="]);
        var words = _fileWordLoader.LoadWord();

        words[0].Should().BeEquivalentTo(new WordPopular("hello", 1));
        words[1].Should().BeEquivalentTo(new WordPopular("andrey", 1));
    }

    [TearDown]
    public void Dispose()
    {
        _fakeStemReader.Dispose();
    }
}