using NUnit.Framework;

namespace SlimMonads.Tests;

public static class OptionAssertions
{
    public static void IsSome<TValue>(this Option<TValue> option) => Assert.That(option.HasValue, Is.EqualTo(true));

    public static void IsNone<TValue>(this Option<TValue> option) => Assert.That(option.HasValue, Is.EqualTo(false));
}