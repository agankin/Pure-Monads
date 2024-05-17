using System;
using NUnit.Framework;

namespace PureMonads.Tests;

public static class ResultAssertions
{
    public static void IsValue<TValue, TError>(this Result<TValue, TError> result, TValue expectedValue)
    {
        Assert.That(result.HasValue, Is.EqualTo(true));

        var _ = result.Match(
            value => value.ItIs(expectedValue),
            _ =>
            {
                Assert.Fail("Result is Error.");
                return default!;
            });
    }

    public static void IsValue<TValue>(this Result<TValue> result, TValue expectedValue)
    {
        Assert.That(result.HasValue, Is.EqualTo(true));

        var _ = result.Match(
            value => value.ItIs(expectedValue),
            _ =>
            {
                Assert.Fail("Result is Error.");
                return default!;
            });
    }

    public static void IsError<TValue, TError>(this Result<TValue, TError> result, TError expectedError)
    {
        Assert.That(result.HasValue, Is.EqualTo(false));

        var _ = result.Match(
            _ =>
            {
                Assert.Fail("Result is Value.");
                return default!;
            },
            error => error.ItIs(expectedError));
    }

    public static void IsError<TValue>(this Result<TValue> result, Exception expectedException)
    {
        Assert.That(result.HasValue, Is.EqualTo(false));
        
        var _ = result.Match(
            _ =>
            {
                Assert.Fail("Result is Value.");
                return default!;
            },
            error => error.ItIs(expectedException));
    }
}