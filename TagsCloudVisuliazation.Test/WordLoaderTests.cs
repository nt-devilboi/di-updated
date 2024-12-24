using FakeItEasy;
using FluentAssertions;
using TagsCloudVisualization;
using WeCantSpell.Hunspell;

namespace TagsCloudVisuliazation.Test;

public class WordLoaderTests
{
    [Test]
    public void LoadWord()
    {
        var fakeReader = A.Fake<IProcessOutputReader>();
        var steamReader = new Lazy<IProcessOutputReader>(() => fakeReader);

        A.CallTo(() => steamReader.Value.ReadLine()).ReturnsNextFromSequence("hello");


        var wordList = WordList.CreateFromWords(["hello"]);
   
        var wordLoader = new FileWordLoader(wordList, steamReader);
        var words = wordLoader.LoadWord();

        words[0].Should().Be(new WordPopular("hello", 1));
    }
}