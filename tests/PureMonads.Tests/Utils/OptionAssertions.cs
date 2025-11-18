using NUnit.Framework;

namespace PureMonads.Tests;

public static class OptionAssertions
{
    public static void IsSome<TValue>(this Option<TValue> option, TValue expectedValue)
    {
        Assert.That(option.HasValue, Is.EqualTo(true));
        
        var _ = option.Match(
            value => value.ItIs(expectedValue),
            () =>
            {
                Assert.Fail("Option is None.");
                return default!;
            });
    }

    public static void IsNone<TValue>(this Option<TValue> option) => Assert.That(option.HasValue, Is.EqualTo(false));
}