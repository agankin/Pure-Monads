using NUnit.Framework;

namespace PureMonads.Tests;

public static class GeneralAssertions
{
    public static TValue ItIs<TValue>(this TValue value, TValue expectedValue)
    {
        Assert.That(value, Is.EqualTo(expectedValue));
        return value;
    }

    public static TValue NotNull<TValue>(this TValue? value)
    {
        Assert.That(value, Is.Not.EqualTo(null));
        return value!;
    }
}