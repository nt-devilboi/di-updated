using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization;

public class FactoryCloudBitMap
{
    private readonly TagCloudSettings _cloudSettings;

    public FactoryCloudBitMap(TagCloudSettings cloudSettings)
    {
        _cloudSettings = cloudSettings;
    }

    public Result<ITagCloudImage> Create()
    {
        var result = Validate(_cloudSettings.Size.Width, _cloudSettings.Size.Height, _cloudSettings.PathDirectory,
            _cloudSettings.NamePhoto);
        if (!result.IsSuccess) return new Result<ITagCloudImage>(result.Error);

        return new CloudBitMap(_cloudSettings).AsResult<ITagCloudImage>();
    }

    private static Result<None> Validate(int width, int height, string path,
        string fileName) // todo: проверка на то, что файла не существуте.
    {
        return Result
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