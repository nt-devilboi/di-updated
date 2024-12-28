using FakeItEasy;
using FluentAssertions;
using TagCloud2;
using TagCloud2.Abstract;
using TagsCloudVisualization;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisuliazation.Test;

public class FullWorkTests
{
    private TagCloudCli _tagCloudCli;
    private IInputData _inputData;
    private Logger _logger;
    [SetUp]
    public void SetUp()
    {
        var tagCloudSettings = new TagCloudSettings();
        var loadWordSettings = new WordLoaderSettings();

        var wordLoader = new FileWordLoader(
            new Lazy<IProcessOutputReader>(() => new StemReader(loadWordSettings)));
        var cloudLayouter = new CircularCloudLayouter(tagCloudSettings);

        var tagCloud = new TagCloud(cloudLayouter, wordLoader, tagCloudSettings, new MeasureString(tagCloudSettings));
        var factory = new FactoryBitMap(tagCloudSettings);
        _logger = new Logger();

        _inputData = A.Fake<IInputData>();
        _tagCloudCli = new TagCloudCli(tagCloud,
            new AppSettings(tagCloudSettings, loadWordSettings),
            factory,
            _logger,
            _inputData);
    }

    [Test]
    public void TagCloudCli_WorkCorrect()
    {
        SetLineForReadLine("create -s 1920x1680 -d ./../../../photos/ -n TestCli -w ./../../../text.txt -a ./../../../mystem.exe -e 50 -c yellow -b white -f bpm -t arial");
        
        _tagCloudCli.Run();

        _logger.GetData().Should().BeEmpty();
        File.Exists("./../../../photos/tagCloud-(TestCli).Bmp").Should().BeTrue();
        File.Delete("./../../../photos/tagCloud-(TestCli).Bmp");
    }
    
    
    [Test]
    public void TagCloudCli_SizeImage_ShouldBeMoreThanZero()
    {
        SetLineForReadLine("create -s 0x0 -d ./../../../photos/ -n TestCli -w ./../../../text.txt -a ./../../../mystem.exe -e 50 -c yellow -b white -f bpm -t arial");
        
        _tagCloudCli.Run();

        _logger.GetData().Should().Be("size of image should be with positive number, now: {Width=0, Height=0}\r\n");
        File.Exists("./../../../photos/tagCloud-(TestCli).Bmp").Should().BeFalse();
    }

    private void SetLineForReadLine(string line)
    {
        A.CallTo(() => _inputData.ReadArgs())
            .Returns(line.Split());
    }
    
    
   
    
}