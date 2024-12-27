// это не Result в привычном пониманий. эта монада должна работать только с провекрами условий, не больше



public class None
{
    private None()
    {
    }
}

public class Results<TResult>(string? error, TResult result = default)
{
    public string? Error = error;
    public TResult Value = result;

    public bool IsSuccess => Error == null;
}

public static class Results
{
    public static Results<T> AsResult<T>(this T value)
    {
        return Ok(value);
    }

    private static Results<T> Ok<T>(T value)
    {
        return new Results<T>(null, value);
    }

    public static Results<None> StartCheck(bool isCorrect, string error)
    {
        if (isCorrect) return Ok();

        return Fail<None>(error);
    }

    public static Results<T> AndCheck<T>(this Results<T> results, bool isCorrect, string error)
    {
        if (isCorrect) return results;
        if (results.IsSuccess) return Fail<T>(error);

        return results.RefineError(error);
    }

    private static Results<None> Ok()
    {
        return Ok<None>(null);
    }

    public static Results<T> Fail<T>(string error)
    {
        return new Results<T>(error);
    }

    private static Results<TInput> RefineError<TInput>(
        this Results<TInput> input,
        string errorMessage)
    {
        return input.ReplaceError(err => errorMessage + ". " + err);
    }

    private static Results<TInput> ReplaceError<TInput>(
        this Results<TInput> input,
        Func<string, string> replaceError)
    {
        if (input.IsSuccess) return input;
        return Fail<TInput>(replaceError(input.Error));
    }
}