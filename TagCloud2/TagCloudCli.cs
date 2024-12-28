using CommandLine;
using TagCloud2.Abstract;
using TagCloud2.Options;
using Tagloud2.Abstract;
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
    IInputData inputData)
    : ITagCloudController
{
    public void Run()
    {
        Parser.Default.ParseArguments<CreateTagCloud>(inputData.ReadArgs())
            .WithParsed(CreateCloud)
            .WithNotParsed(HanderError);
    }

    private void HanderError(IEnumerable<Error> errors)
    {
        throw new NotImplementedException();
    }

    private void CreateCloud(CreateTagCloud createTagCloud)
    {
        SetParameters(createTagCloud)
            .Then(CreateBitMap)
            .Then(tagCloud.GenerateCloud)
            .Then(SaveCloud)
            .OnFail(logger.WriteLine);
    }

    private Result<None> SaveCloud(ITagCloudImage tagCloudImage)
    {
        tagCloudImage.Save();
        return Result.Ok();
    }

    private Result<ITagCloudImage> CreateBitMap(None obj)
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

        appSettings.WordLoaderSettings.Path = createTagCloud.PathToWords;
        appSettings.WordLoaderSettings.PathStem = createTagCloud.StemPath;

        return Result.Ok();
    }
}