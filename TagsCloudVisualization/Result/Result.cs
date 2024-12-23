namespace TagsCloudVisualization.Result;

// это не Result в привычном пониманий. эта монада должна работать только с провекрами условий, не больше. я потом упрощу код, чтоб это было явно видно по коду
public class None
{
    private None()
    {
    }
}

public class Result<TResult>
{
    public string? Error;
    public TResult Value;

    public Result(string? error, TResult result = default)
    {
        Error = error;
        Value = result;
    }

    public bool IsSuccess => Error == null;
}

public static class Result
{
    public static Result<T> AsResult<T>(this T value)
    {
        return Ok(value);
    }

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(null, value);
    }

    public static Result<None> StartCheck(bool isCorrect, string error)
    {
        if (isCorrect) return Ok();

        return Fail<None>(error);
    }

    public static Result<T> AndCheck<T>(this Result<T> result, bool isCorrect, string error)
    {
        if (isCorrect) return result;
        if (result.IsSuccess) return Fail<T>(error);

        return result.RefineError(error);
    }

    public static Result<None> Ok()
    {
        return Ok<None>(null);
    }

    public static Result<T> Fail<T>(string error)
    {
        return new Result<T>(error);
    }

    public static Result<TInput> RefineError<TInput>(
        this Result<TInput> input,
        string errorMessage)
    {
        return input.ReplaceError(err => errorMessage + ". " + err);
    }

    public static Result<TInput> ReplaceError<TInput>(
        this Result<TInput> input,
        Func<string, string> replaceError)
    {
        if (input.IsSuccess) return input;
        return Fail<TInput>(replaceError(input.Error));
    }
}