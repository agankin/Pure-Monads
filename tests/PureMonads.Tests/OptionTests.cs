using NUnit.Framework;

namespace PureMonads.Tests;

using static Option;

[TestFixture(TestName = "Option Tests")]
public class OptionTests
{
    [Test(Description = "Tests Match")]
    public void TestsMatch()
    {
        "value".Some()
            .Match(value => value + " 2", () => "none").ItIs("value 2");
        None<string>()
            .Match(value => value + " 2", () => "none").ItIs("none");
    }

    [Test(Description = "Tests Map")]
    public void TestsMap()
    {
        "value".Some()
            .Map(value => value + " 2").IsSome("value 2");
        None<string>()
            .Map(value => value + " 2").IsNone();
    }

    [Test(Description = "Tests FlatMap")]
    public void TestsFlatMap()
    {
        "value".Some()
            .FlatMap<string, string>(value => value + " 2").IsSome("value 2");
        "value".Some()
            .FlatMap(value => None<string>()).IsNone();

        None<string>()
            .FlatMap<string, string>(value => value + " 2").IsNone();
        None<string>()
            .FlatMap(value => None<string>()).IsNone();
    }

    [Test(Description = "Tests Or")]
    public void TestsOr()
    {
        // Some value or an alternative value.
        "value".Some()
            .Or("other").ItIs("value");
        None<string>()
            .Or("other").ItIs("other");

        // Some value or an alternative value from a factory function.
        "value".Some()
            .Or(() => "other").ItIs("value");
        None<string>()
            .Or(() => "other").ItIs("other");

        // Some value or an alternative option.
        "value".Some()
            .Or("other".Some()).IsSome("value");
        "value".Some()
            .Or(None<string>()).IsSome("value");
        None<string>()
            .Or("other".Some()).IsSome("other");
        None<string>()
            .Or(None<string>()).IsNone();

        // Some value or an alternative option from a factory function.
        "value".Some()
            .Or(() => "other".Some()).IsSome("value");
        "value".Some()
            .Or(() => None<string>()).IsSome("value");
        None<string>()
            .Or(() => "other".Some()).IsSome("other");
        None<string>()
            .Or(() => None<string>()).IsNone();
    }

    [Test(Description = "Tests NullAsNone")]
    public void TestsNullAsNone()
    {
        string? @null = null;

        "value".NullAsNone().IsSome("value");
        @null.NullAsNone().IsNone();
    }
}