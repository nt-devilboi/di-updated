using FakeItEasy;
using FluentAssertions;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Result;

namespace TagsCloudVisualization.Tests;

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
       SetWords(["hello", "hello"]);
        var words = _fileWordLoader.LoadWords().ToArray();

        words[0].Should().BeEquivalentTo(new FrequencyWord("hello", 2));
    }

    [Test]
    public void WordLoader_WordNotInDic()
    {
       SetWords([]);

        var words = _fileWordLoader.LoadWords();
        words.Should().BeEmpty();
    }

    [Test]
    public void WordLoader_WordInLowerCase()
    {
        SetWords(["HELLO", "hello"]);
        var words = _fileWordLoader.LoadWords().ToArray();

        words[0].Should().BeEquivalentTo(new FrequencyWord("hello", 2));
    }


    [Test]
    public void WordLoader_CheckWithTwoWord()
    {
        SetWords(["hello", "hello", "andrey"]);
        var words = _fileWordLoader.LoadWords().ToArray();

        words[0].Should().BeEquivalentTo(new FrequencyWord("hello", 2));
        words[1].Should().BeEquivalentTo(new FrequencyWord("andrey", 1));
        words.Length.Should().Be(2);
    }

    [Test]
    public void WordLoader_ShouldSkipBoringWord()
    {
        SetWords([
            "на=PR=|на=PART=",
            "и=CONJ=|и=INTJ=|и=S",
            "hello",
            "andrey", "от=PR="
        ]);

        var words = _fileWordLoader.LoadWords().ToArray();
        words[0].Should().BeEquivalentTo(new FrequencyWord("hello", 1));
        words[1].Should().BeEquivalentTo(new FrequencyWord("andrey", 1));
        words.Length.Should().Be(2);
    }

    private void SetWords(string[] words)
    {
        A.CallTo(() => _fakeStemReader.ReadLines()).ReturnsNextFromSequence(words);
    }

    [TearDown]
    public void Dispose()
    {
        _fakeStemReader.Dispose();
    }
}