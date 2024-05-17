using NUnit.Framework;

namespace PureMonads.Tests;

public static class GeneralAssertions
{
    public static TValue ItIs<TValue>(this TValue value, TValue expectedValue)
    {
        Assert.That(value, Is.EqualTo(expectedValue));
        return value;
    }
}