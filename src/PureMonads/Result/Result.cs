namespace PureMonads;

/// <summary>
/// Represents a result that can be either a value or an error.
/// </summary>
/// <typeparam name="TValue">Value type.</typeparam>
/// <typeparam name="TError">Error type.</typeparam>
public readonly struct Result<TValue, TError> : IComonad2<TValue, TError, Result>
{
    private readonly bool _hasValue;
    private readonly TValue _value;
    private readonly TError _error;

    internal Result(TValue value)
    {
        _hasValue = true;
        _value = value;
        _error = default!;
    }

    internal Result(TError error)
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
    public TResult Extract<TResult>(Func<TValue, TResult> mapValue, Func<TError, TResult> mapError) =>
        _hasValue ? mapValue(_value) : mapError(_error);

    public static implicit operator Result<TValue, TError>(TValue value) => new Result<TValue, TError>(value);

    public static implicit operator Result<TValue, TError>(TError error) => new Result<TValue, TError>(error);
}