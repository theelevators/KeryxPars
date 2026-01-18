

namespace KeryxPars.Core.Models;

public record Result<TError>
{
    public bool IsSuccess { get; }
    public TError? Error { get; }

    protected Result(bool isSuccess, TError? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result<TError> Success() => new(true, default);
    public static Result<TError> Failure(TError error) => new(false, error ?? throw new ArgumentNullException(nameof(error)));

    public static implicit operator Result<TError>(TError error) => Failure(error);
}

public record Result<TValue, TError> : Result<TError>
{
    public TValue? Value { get; }
    private Result(TValue value) : base(true, default) => Value = value;
    private Result(TError error) : base(false, error) { }

    public static implicit operator Result<TValue, TError>(TValue value) => new(value);

    public static implicit operator Result<TValue, TError>(TError error) => new(error);
    public static Result<TValue, TError> Success(TValue value) => new(value);
    public new static Result<TValue, TError> Failure(TError error) => new(error ?? throw new ArgumentNullException(nameof(error)));
}