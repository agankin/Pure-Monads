namespace Memtest
{
    /// <summary>
    /// An option that can be either Some containing a value or None without value.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    public readonly struct Option<TValue> : IOption<TValue>
    {
        private readonly bool _hasValue;
        private readonly TValue? _value;

        private Option(bool hasValue, TValue? value) => (_hasValue, _value) = (hasValue, value);

        /// <summary>
        /// Creates an instance of None option.
        /// </summary>
        /// <returns>An instance of None option.</returns>
        public static Option<TValue> None() => new(hasValue: false, default);

        /// <inheritdoc/>
        public TResult Match<TResult>(Func<TValue, TResult> mapSome, Func<TResult> mapNone) =>
            _hasValue ? mapSome(_value.NotNull()) : mapNone();

        /// <summary>
        /// If the current option is Some returns the result from <paramref name="mapSome"/> invocation wrapped into Some option.
        /// Otherwise returns None.
        /// </summary>
        /// <typeparam name="TResult">Result value type.</typeparam>
        /// <param name="mapSome">A delegate invoked if the current option is Some.</param>
        /// <returns>Some result value or None.</returns>
        public Option<TResult> Map<TResult>(Func<TValue, TResult> mapSome) =>
            Match(value => mapSome(value.NotNull()).Some(), Option<TResult>.None);

        /// <summary>
        /// If the current option is Some returns the result option from <paramref name="mapSome"/> invocation.
        /// Otherwise returns None.
        /// </summary>
        /// <typeparam name="TResult">Result value type.</typeparam>
        /// <param name="mapSome">A delegate invoked if the current option is Some.</param>
        /// <returns>Some result value or None.</returns>
        public Option<TResult> FlatMap<TResult>(Func<TValue, Option<TResult>> mapSome) =>
             Match(value => mapSome(value.NotNull()), Option<TResult>.None);

        public static implicit operator Option<TValue>(TValue value) => value.Some();
    }
}