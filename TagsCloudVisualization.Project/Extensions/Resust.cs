using TagsCloudVisualization.Result;

namespace TagsCloudVisualization.Extensions;

public static class ResultExtension
{
    public static Result<T> Validate<T>(this T obj, Func<T, bool> predicate, Func<T, string> error) =>
        predicate(obj) ? Result.Result.Ok(obj) : Result.Result.Fail<T>(error(obj));
}