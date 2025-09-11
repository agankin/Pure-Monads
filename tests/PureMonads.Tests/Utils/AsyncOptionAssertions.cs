using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

public static class AsyncOptionAssertions
{
    public static async Task IsSomeAsync<TValue>(this AsyncOption<TValue> option, TValue expectedValue)
    {
        Assert.That(option.HasValue, Is.EqualTo(true));
        await option.Match(
            async asyncValue => (await asyncValue).ItIs(expectedValue),
            () =>
            {
                Assert.Fail("AsyncOption is None.");
                return Task.CompletedTask;
            });
    }

    public static void IsNone<TValue>(this AsyncOption<TValue> option) => Assert.That(option.HasValue, Is.EqualTo(false));
}