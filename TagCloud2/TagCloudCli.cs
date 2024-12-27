using CommandLine;
using TagCloud2.Options;
using TagsCloudVisualization;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Settings;

namespace TagCloud2;

public class TagCloudCli(TagCloud tagCloud, AppSettings appSettings, AbstractFactoryBitMap factoryCloudBitMap)
    : ITagCloudController
{
    public void Run()
    {
        var data = Console.ReadLine();
        var args = data?.Split(" ");
        Parser.Default.ParseArguments<CreateTagCloud>(args)
            .MapResult(CreateCloud,
                errors => new List<Error>(errors));
    }

    private IEnumerable<Error> CreateCloud(CreateTagCloud createTagCloud)
    {
        SetParameters(createTagCloud);

        var bitMapImage = factoryCloudBitMap.Create();
        if (!bitMapImage.IsSuccess)
        {
            Console.WriteLine(bitMapImage.Error);
            return new List<Error>();
        }

        var image = tagCloud.GenerateCloud(bitMapImage.Value, (ISizeWord)bitMapImage.Value);
        if (!image.IsSuccess) Console.WriteLine(image.Error);

        image.Value.Save();

        return new List<Error>();
    }

    private void SetParameters(CreateTagCloud createTagCloud)
    {
        appSettings.TagCloudSettings.PathDirectory = createTagCloud.GetDirectory();
        appSettings.TagCloudSettings.Size = createTagCloud.GetSize();
        appSettings.TagCloudSettings.NamePhoto = createTagCloud.NamePhoto;
        appSettings.TagCloudSettings.EmSize = int.Parse(createTagCloud.EmSize);
        appSettings.TagCloudSettings.ColorWords = createTagCloud.Color;
        appSettings.TagCloudSettings.BackGround = createTagCloud.BackgrondColor;
        appSettings.TagCloudSettings.ImageFormat = createTagCloud.GetImageFormat();
        appSettings.TagCloudSettings.Font = createTagCloud.Font;

        appSettings.WordLoaderSettings.Path = createTagCloud.PathToWords;
        appSettings.WordLoaderSettings.PathStem = createTagCloud.StemPath;
    }
}