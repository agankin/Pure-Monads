using NUnit.Framework;

namespace PureMonads.Tests;

public static class EitherAssertions
{
    public static void IsLeft<TLeft, TRight>(this Either<TLeft, TRight> either, TLeft expectedLeft)
    {
        Assert.That(either.IsLeft, Is.EqualTo(true));

        var _ = either.Match(
            left => left.ItIs(expectedLeft),
            _ =>
            {
                Assert.Fail("Either is Right.");
                return default!;
            });
    }

    public static void IsRight<TLeft, TRight>(this Either<TLeft, TRight> either, TRight expectedRight)
    {
        Assert.That(either.IsLeft, Is.EqualTo(false));

        var _ = either.Match(
            _ =>
            {
                Assert.Fail("Either is Left.");
                return default!;
            },
            right => right.ItIs(expectedRight));
    }
}