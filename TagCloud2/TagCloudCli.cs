using CommandLine;
using TagCloud2.Abstract;
using TagCloud2.Options;
using TagsCloudVisualization;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Result;
using TagsCloudVisualization.Settings;

namespace TagCloud2;

public class TagCloudCli(
    TagCloud tagCloud,
    AppSettings appSettings,
    AbstractFactoryBitMap factoryCloudBitMap,
    ILogger logger,
    IInputData consoleData)
    : ITagCloudController
{
    public void Run()
    {
        Parser.Default.ParseArguments<CreateTagCloud>(consoleData.GetArgs())
            .WithParsed(CreateCloud);
    }

    private void CreateCloud(CreateTagCloud createTagCloud)
    {
        SetParameters(createTagCloud)
            .Then(CreateBitMap)
            .Then(tagCloud.GenerateCloud)
            .Then(SaveCloud)
            .OnFail(logger.WriteLine);
    }

    private static Result<None> SaveCloud(ITagCloudSave tagCloudImage)
    {
        tagCloudImage.Save();
        return Result.Ok();
    }

    private Result<ITagCloudImage> CreateBitMap()
    {
        return factoryCloudBitMap.Create();
    }

    private Result<None> SetParameters(CreateTagCloud createTagCloud)
    {
        appSettings.TagCloudSettings.PathDirectory = createTagCloud.GetDirectory();
        appSettings.TagCloudSettings.Size = createTagCloud.GetSize();
        appSettings.TagCloudSettings.NamePhoto = createTagCloud.NamePhoto;
        appSettings.TagCloudSettings.EmSize = int.Parse(createTagCloud.EmSize);
        appSettings.TagCloudSettings.ColorWords = createTagCloud.Color;
        appSettings.TagCloudSettings.BackGround = createTagCloud.BackgrondColor;
        appSettings.TagCloudSettings.ImageFormat = createTagCloud.GetImageFormat();
        appSettings.TagCloudSettings.Font = createTagCloud.Font;

        appSettings.WordLoaderSettings.PathTextFile = createTagCloud.PathToWords;
        // appSettings.WordLoaderSettings.PathStem = createTagCloud.StemPath;

        return Result.Ok();
    }
}