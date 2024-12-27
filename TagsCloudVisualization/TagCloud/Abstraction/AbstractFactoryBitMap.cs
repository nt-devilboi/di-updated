using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Abstraction;

public abstract class AbstractFactoryBitMap(TagCloudSettings cloudSettings)
{
    protected abstract ITagCloudImage CreateBitMap(TagCloudSettings tagCloudSettings);

    public Results<ITagCloudImage> Create()
    {
        var result = Validate(cloudSettings.Size.Width, cloudSettings.Size.Height, cloudSettings.PathDirectory,
            cloudSettings.NamePhoto);
        if (!result.IsSuccess) return new Results<ITagCloudImage>(result.Error);

        return CreateBitMap(cloudSettings).AsResult();
    }

    private static Results<None> Validate(int width, int height, string path,
        string fileName)
    {
        return Results
            .StartCheck(Directory.Exists(path),
                $"This Directory Not Exists  {path}")
            .AndCheck(width > 0 && height > 0,
                "size of image should be with positive number")
            .AndCheck(path.EndsWith('/'),
                $@"Path: {path}.  Should Be Directory. Add '/' in end")
            .AndCheck(!File.Exists($"{path} + {fileName}"),
                $"The file named {fileName} already exists");
    }
}