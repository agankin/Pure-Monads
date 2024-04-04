using NUnit.Framework;
using SlimMonads.Tests.Utils;

namespace SlimMonads.Tests;

[TestFixture]
public class OptionTests
{
    [Test(Description = "Tests Match Some.")]
    public void TestsMatchSome()
    {
        var mapSomeCallAssert = new MethodCallAssert<int, int>(value => value + 1);
        var onNoneCallAssert = new MethodCallAssert<int, int>(value => value);
        
        var result = 1.Some().Match(
            mapSome: mapSomeCallAssert.Method,
            onNone: () => onNoneCallAssert.Method(0));

        result.ItIs(2);
        mapSomeCallAssert.CalledTimes(1).NthCall(nth: 0, expectedArg: 1);
        onNoneCallAssert.CalledTimes(0);
    }

    [Test(Description = "Tests Match None.")]
    public void TestsMatchNone()
    {
        var mapSomeCallAssert = new MethodCallAssert<int, int>(value => value + 1);
        var onNoneCallAssert = new MethodCallAssert<int, int>(value => value);
        
        var result = Option<int>.None().Match(
            mapSome: mapSomeCallAssert.Method,
            onNone: () => onNoneCallAssert.Method(0));

        result.ItIs(0);
        mapSomeCallAssert.CalledTimes(0);
        onNoneCallAssert.CalledTimes(1);
    }

    [Test(Description = "Tests Map Some.")]
    public void TestsMapSome()
    {
        var callAssert = new MethodCallAssert<int, int>(value => value + 1);
        var someMappedValue = 1.Some().Map(callAssert.Method);

        someMappedValue.IsSome();
        callAssert.CalledTimes(1).NthCall(nth: 0, expectedArg: 1);
    }

    [Test(Description = "Tests Map None.")]
    public void TestsMapNone()
    {
        var callAssert = new MethodCallAssert<int, int>(value => value + 1);
        var noneMapped = Option<int>.None().Map(callAssert.Method);

        noneMapped.IsNone();
        callAssert.CalledTimes(0);
    }

    [Test(Description = "Tests FlatMap Some")]
    public void TestsFlatMapSome()
    {
        var call1Assert = new MethodCallAssert<int, Option<int>>(value => (value + 1).Some());
        var someValue = 1.Some().FlatMap(call1Assert.Method);

        someValue.IsSome();
        call1Assert.CalledTimes(1).NthCall(nth: 0, expectedArg: 1);

        var call2Assert = new MethodCallAssert<int, Option<int>>(value => Option<int>.None());
        var noneValue = 1.Some().FlatMap(call2Assert.Method);

        noneValue.IsNone();
        call2Assert.CalledTimes(1).NthCall(0, expectedArg: 1);
    }

    [Test(Description = "Tests FlatMap None")]
    public void TestsFlatMapNone()
    {
        var callAssert = new MethodCallAssert<int, Option<int>>(value => (value + 1).Some());
        var noneValue = Option<int>.None().FlatMap(callAssert.Method);

        noneValue.IsNone();
        callAssert.CalledTimes(0);
    }
}