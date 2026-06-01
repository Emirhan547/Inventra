namespace Inventra.Application.Common.Results;

public class Result
{
    public bool Success { get; set; }

    public string Message { get; set; }

    public List<string>? Errors { get; set; }

    public static Result SuccessResult(
        string message = "")
    {
        return new Result
        {
            Success = true,
            Message = message
        };
    }

    public static Result Failure(
        string error)
    {
        return new Result
        {
            Success = false,
            Errors = new List<string>
            {
                error
            }
        };
    }

    public static Result Failure(
        List<string> errors)
    {
        return new Result
        {
            Success = false,
            Errors = errors
        };
    }
}

public class Result<T> : Result
{
    public T? Data { get; set; }

    public static Result<T> SuccessResult(
        T data,
        string message = "")
    {
        return new Result<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    public new static Result<T> Failure(
        string error)
    {
        return new Result<T>
        {
            Success = false,
            Errors = new List<string>
            {
                error
            }
        };
    }

    public new static Result<T> Failure(
        List<string> errors)
    {
        return new Result<T>
        {
            Success = false,
            Errors = errors
        };
    }
}