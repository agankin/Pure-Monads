using System.Runtime.CompilerServices;

namespace Memtest
{
    internal static class NullGuards
    {
        /// <summary>
        /// Ensures that the passed value is not null or throws <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <param name="value">A value.</param>
        /// <param name="valueName">
        /// The name of the value.
        /// By default catches name of a passed into the <paramref name="value"/> parameter expression.</param>
        /// <returns>The value passed in the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        public static TValue NotNull<TValue>(this TValue? value, [CallerArgumentExpression(nameof(value))]string valueName = "") =>
            value ?? throw new ArgumentNullException(valueName);
    }
}