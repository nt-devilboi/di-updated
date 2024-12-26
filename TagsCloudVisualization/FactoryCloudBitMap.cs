using TagsCloudVisualization.Result;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

public class FactoryCloudBitMap(TagCloudSettings cloudSettings)
{
    public Result<ITagCloudImage> Create()
    {
        var result = Validate(cloudSettings.Size.Width, cloudSettings.Size.Height, cloudSettings.PathDirectory,
            cloudSettings.NamePhoto);
        if (!result.IsSuccess) return new Result<ITagCloudImage>(result.Error);

        return new CloudBitMap(cloudSettings).AsResult<ITagCloudImage>();
    }

    private static Result<None> Validate(int width, int height, string path,
        string fileName)
    {
        return Result.Result
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