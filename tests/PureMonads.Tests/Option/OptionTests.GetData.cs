using System;
using NUnit.Framework;

namespace PureMonads.Tests;

using static Option;

public partial class OptionTests
{
    [Test(Description = "Tests ValueOrFailure")]
    public void TestsValueOrFailure()
    {
        "value1".Some().ValueOrFailure().ItIs("value1");

        var exception = Assert.Throws<Exception>(() =>
        {
            None<string>().ValueOrFailure("It is None.");
        });

        exception.NotNull().Message.ItIs("It is None.");
    }

    [Test(Description = "Tests ValueOrFailure (custom exception)")]
    public void TestsValueOrFailureCustom()
    {
        "value1".Some().ValueOrFailure(() => new InvalidOperationException()).ItIs("value1");

        var exception = Assert.Throws<InvalidOperationException>(() =>
        {
            None<string>().ValueOrFailure(() => new InvalidOperationException("It is None."));
        });

        exception.NotNull().Message.ItIs("It is None.");
    }
}