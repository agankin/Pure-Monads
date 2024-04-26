namespace PureMonads;

/// <summary>
/// Represents a result that can either be a value or an exception.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
public readonly struct Result<TValue> : IComonad2<TValue, Exception, Result>
{
    private readonly bool _hasValue;
    private readonly TValue _value;
    private readonly Exception _error;

    internal Result(TValue value)
    {
        _hasValue = true;
        _value = value;
        _error = default!;
    }

    internal Result(Exception error)
    {
        _hasValue = false;
        _value = default!;
        _error = error;
    }

    /// <inheritdoc/>
    public IComonad2<TNewValue, TNewError, Result> Unit<TNewValue, TNewError>(TNewValue value) =>
        new Result<TNewValue, TNewError>(value);

    /// <inheritdoc/>
    public IComonad2<TNewValue, TNewError, Result> Unit<TNewValue, TNewError>(TNewError error) =>
        new Result<TNewValue, TNewError>(error);

    /// <inheritdoc/>
    public TResult Extract<TResult>(Func<TValue, TResult> mapValue, Func<Exception, TResult> mapError) =>
        _hasValue ? mapValue(_value) : mapError(_error);

    public static implicit operator Result<TValue>(TValue value) => new Result<TValue>(value);

    public static implicit operator Result<TValue>(Exception error) => new Result<TValue>(error);
}