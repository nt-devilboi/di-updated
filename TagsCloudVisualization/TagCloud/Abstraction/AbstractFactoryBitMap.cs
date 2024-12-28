using TagsCloudVisualization.Result;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Abstraction;

public abstract class AbstractFactoryBitMap(TagCloudSettings cloudSettings)
{
    protected abstract ITagCloudImage CreateBitMap(TagCloudSettings cloudSettings);

    public Result<ITagCloudImage> Create()
    {
        return ValidateDirectory(cloudSettings)
            .Then(ValidateSizeImage)
            .Then(ValidateFullPath)
            .Then(ValidatePathNamed)
            .Then(CreateBitMap);
    }

    private Result<TagCloudSettings> ValidateDirectory(TagCloudSettings tagCloudSettings) =>
        Validate(tagCloudSettings, t => Directory.Exists(t.PathDirectory),
            t => $"This Directory Not Exists  {t.PathDirectory}");

    private Result<TagCloudSettings> ValidateSizeImage(TagCloudSettings tagCloudSettings) =>
        Validate(tagCloudSettings, t => t.Size is { Width: > 0, Height: > 0 },
            t => $"size of image should be with positive number, now: {t.Size}");

    private Result<TagCloudSettings> ValidatePathNamed(TagCloudSettings tagCloudSettings)
        => Validate(tagCloudSettings, t => t.PathDirectory.EndsWith('/'),
            t => $"Path: {t.PathDirectory}.  Should Be Directory. Add '/' in end");

    private Result<TagCloudSettings> ValidateFullPath(TagCloudSettings tagCloudSettings)
        => Validate(tagCloudSettings, t => !File.Exists($"{Path.Combine(t.PathDirectory, t.NamePhoto)}"),
            t => $"The file named {t.NamePhoto} already exists");

    private Result<T> Validate<T>(T obj, Func<T, bool> predicate, Func<T, string> error) =>
        predicate(obj) ? Result.Result.Ok(obj) : Result.Result.Fail<T>(error(obj));
}