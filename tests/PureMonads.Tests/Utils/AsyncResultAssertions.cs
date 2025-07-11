using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

public static class AsyncResultAssertions
{
    public static async Task IsValueAsync<TValue, TError>(this AsyncResult<TValue, TError> result, TValue expectedValue)
    {
        Assert.That(result.HasValue, Is.EqualTo(true));

        await result.Match(
            async task => (await task).ItIs(expectedValue),
            _ =>
            {
                Assert.Fail("Result is Error.");
                return default!;
            });
    }

    public static async Task IsValueValue<TValue>(this AsyncResult<TValue> result, TValue expectedValue)
    {
        Assert.That(result.HasValue, Is.EqualTo(true));

        await result.Match(
            async task => (await task).ItIs(expectedValue),
            _ =>
            {
                Assert.Fail("Result is Error.");
                return default!;
            });
    }

    public static void IsError<TValue, TError>(this AsyncResult<TValue, TError> result, TError expectedError)
    {
        Assert.That(result.HasValue, Is.EqualTo(false));

        var _ = result.Match(
            _ =>
            {
                Assert.Fail("AsyncResult is Value.");
                return default!;
            },
            error => error.ItIs(expectedError));
    }

    public static void IsError<TValue>(this AsyncResult<TValue> result, Exception expectedException)
    {
        Assert.That(result.HasValue, Is.EqualTo(false));
        
        var _ = result.Match(
            _ =>
            {
                Assert.Fail("AsyncResult is Value.");
                return default!;
            },
            error => error.ItIs(expectedException));
    }
}