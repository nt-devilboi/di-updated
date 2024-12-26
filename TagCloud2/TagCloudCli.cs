using System.Drawing;
using CommandLine;
using TagCloud2.Options;
using TagsCloudVisualization;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Settings;

namespace TagCloud2;

public class TagCloudCli : ITagCloudController
{
    private readonly AppSettings _appSettings;
    private readonly FactoryCloudBitMap _factoryCloudBitMap;
    private readonly TagCloud _tagCloud;

    public TagCloudCli(TagCloud tagCloud, AppSettings appSettings, FactoryCloudBitMap factoryCloudBitMap)
    {
        _tagCloud = tagCloud;
        _appSettings = appSettings;
        _factoryCloudBitMap = factoryCloudBitMap;
    }

    public void Run()
    {
        var data = Console.ReadLine();
        var args = data?.Split(" ");
        while (args != null)
        {
            Parser.Default.ParseArguments<CreateTagCloud>(args)
                .MapResult(CreateCloud,
                    errors => new List<Error>(errors));

            args = Console.ReadLine().Split(" ");
        }
    }

    private IEnumerable<Error> CreateCloud(CreateTagCloud createTagCloud)
    {
        SetParameters(createTagCloud);

        // todo: проверить, что можно много облаков делать и все корректо
        var BitMapImage = _factoryCloudBitMap.Create();
        if (!BitMapImage.IsSuccess)
        {
            Console.WriteLine(BitMapImage.Error);
            return new List<Error>();
        }

        var image = _tagCloud.GenerateCloud(BitMapImage.Value, (ISizeWord)BitMapImage.Value);
        if (!image.IsSuccess)
        {
            Console.WriteLine(image.Error);
        }

        image.Value.Save();

        return new List<Error>();
    }

    private void SetParameters(CreateTagCloud createTagCloud)
    {
        _appSettings.TagCloudSettings.PathDirectory = createTagCloud.Directory;
        _appSettings.TagCloudSettings.Size = createTagCloud.GetSize();
        _appSettings.TagCloudSettings.NamePhoto = createTagCloud.NamePhoto;
        _appSettings.TagCloudSettings.EmSize = int.Parse(createTagCloud.EmSize);
        _appSettings.TagCloudSettings.ColorWords = createTagCloud.Color;
        _appSettings.TagCloudSettings.BackGround = createTagCloud.BackgrondColor;
        _appSettings.TagCloudSettings.ImageFormat = createTagCloud.GetImageFormat();
        _appSettings.TagCloudSettings.Font = createTagCloud.Font;

        _appSettings.WordLoaderSettings.Path = createTagCloud.PathToWords;
        _appSettings.WordLoaderSettings.PathStem = createTagCloud.StemPath;
    }
}