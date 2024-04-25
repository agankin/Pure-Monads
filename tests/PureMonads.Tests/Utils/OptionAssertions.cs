using System;
using NUnit.Framework;

namespace PureMonads.Tests;

public static class OptionAssertions
{
    public static void IsSome<TValue>(this Option<TValue> option) => Assert.That(option.HasValue, Is.EqualTo(true));

    public static void IsSome<TValue>(this Option<TValue> option, TValue expectedValue)
    {
        Assert.That(option.HasValue, Is.EqualTo(true));
        var value = option.Match(value => value, () => throw new Exception("Option is None."));

        value.ItIs(expectedValue);
    }

    public static void IsNone<TValue>(this Option<TValue> option) => Assert.That(option.HasValue, Is.EqualTo(false));
}