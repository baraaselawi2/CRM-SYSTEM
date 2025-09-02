public class Result<T>
{
    public bool Success { get; }
    public string Error { get; }
    public T? Value { get; }

    private Result(T value)
    {
        Success = true;
        Value = value;
        Error = string.Empty;
    }

    private Result(string error)
    {
        Success = false;
        Value = default;
        Error = error ?? string.Empty;
    }

    public static Result<T> Ok(T value) => new Result<T>(value);
    public static Result<T> Fail(string error) => new Result<T>(error);
}
