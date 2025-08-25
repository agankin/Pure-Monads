using NUnit.Framework;

namespace PureMonads.Tests;

public partial class OptionTests
{
    [Test(Description = "Tests NullAsNone")]
    public void TestsNullAsNone()
    {
        string? @null = null;

        "value".NullAsNone().IsSome("value");
        @null.NullAsNone().IsNone();
    }
}