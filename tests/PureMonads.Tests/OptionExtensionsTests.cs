using NUnit.Framework;

namespace PureMonads.Tests;

[TestFixture]
public class OptionExtensionsTests
{
    [Test(Description = "Tests NullAsNone.")]
    public void TestsNullAsNone()
    {
        "string".NullAsNone()
            .IsSome("string");
        ((string?)null).NullAsNone()
            .IsNone();
    }

    [Test(Description = "Tests Option Or.")]
    public void TestsOptionOr()
    {
        // Some value or an alternative value.
        1.Some().Or(2)
            .ItIs(1);
        Option<int>.None().Or(2)
            .ItIs(2);

        // Some value or an alternative value from a factory function.
        1.Some().Or(() => 2)
            .ItIs(1);
        Option<int>.None().Or(() => 2)
            .ItIs(2);

        // Some value or an alternative option.
        1.Some().Or(2.Some())
            .IsSome(1);
        Option<int>.None().Or(2.Some())
            .IsSome(2);

        // Some value or an alternative option from a factory function.
        1.Some().Or(() => 2.Some())
            .IsSome(1);
        Option<int>.None().Or(() => 2.Some())
            .IsSome(2);
    }
}