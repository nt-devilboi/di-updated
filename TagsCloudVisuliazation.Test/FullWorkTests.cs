using System.Drawing;
using System.Drawing.Imaging;
using FluentAssertions;
using TagCloud2;
using TagCloud2.Abstract;
using TagsCloudVisualization;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisuliazation.Test;

public class FullWorkTests
{
    [Test]
    public void TagCloud_CreateCloud()
    {
        var tagCloudSettings = new TagCloudSettings()
        {
            Size = new Size(1920, 1680),
            EmSize = 50,
            NamePhoto = "TestCreateCloud",
            PathDirectory = "./../../../photos/",
            Font = "arial",
            ImageFormat = ImageFormat.Png,
            BackGround = Color.White,
            ColorWords = Color.Black
        };

        var loadWordSettings = new WordLoaderSettings()
        {
            Path = "./../../../text.txt",
            PathStem = "./../../../mystem.exe"
        };


        var wordList = WeCantSpell.Hunspell.WordList.CreateFromFiles("./../../../ru_RU.dic");
        var wordLoader = new FileWordLoader(wordList,
            new Lazy<IProcessOutputReader>(() => new StemReader(loadWordSettings)));
        var cloudLayouter = new CircularCloudLayouter(tagCloudSettings);

        var tagCloud = new TagCloud(cloudLayouter, wordLoader, tagCloudSettings);
        var image = new CloudBitMap(tagCloudSettings);


        var completedImage = tagCloud.GenerateCloud(image, image);

        completedImage.GetValueOrThrow().Save();


        File.Exists("./../../../photos/tagCloud-(TestCreateCloud).png").Should().BeTrue();
        File.Delete("./../../../photos/tagCloud-(TestCreateCloud).png");
    }


    [Test]
    public void TagCloud_CliTest()
    {
        var tagCloudSettings = new TagCloudSettings()
        {
            Size = new Size(1920, 1680),
            EmSize = 50,
            NamePhoto = "TestCreateCloud",
            PathDirectory = "./../../../photos/",
            Font = "arial",
            ImageFormat = ImageFormat.Png,
            BackGround = Color.White,
            ColorWords = Color.Black
        };

        var loadWordSettings = new WordLoaderSettings()
        {
            Path = "./../../../text.txt",
            PathStem = "./../../../mystem.exe"
        };


        var wordList = WeCantSpell.Hunspell.WordList.CreateFromFiles("./../../../ru_RU.dic");
        var wordLoader = new FileWordLoader(wordList,
            new Lazy<IProcessOutputReader>(() => new StemReader(loadWordSettings)));
        var cloudLayouter = new CircularCloudLayouter(tagCloudSettings);

        var tagCloud = new TagCloud(cloudLayouter, wordLoader, tagCloudSettings);
        var factory = new FactoryBitMap(tagCloudSettings);
        var logger = new TestsLogger();
        var cli = new TagCloudCli(tagCloud, new AppSettings(tagCloudSettings, loadWordSettings), factory, logger);

        using (resource)
        {
            
        }
        cli.Run();

        logger.GetData().Should().BeEmpty();
        File.Exists("./../../../photos/tagCloud-(TestCreateCloud).png").Should().BeTrue();
        File.Delete("./../../../photos/tagCloud-(TestCreateCloud).png");
    }
}