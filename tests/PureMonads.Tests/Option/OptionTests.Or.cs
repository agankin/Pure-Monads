using NUnit.Framework;

namespace PureMonads.Tests;

using static Option;

public partial class OptionTests
{
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
}