using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncResult;

[TestFixture(TestName = "AsyncResult Tests")]
public partial class AsyncResultTests
{
    [Test(Description = "Tests Eq")]
    public void TestsEquality()
    {
        var Value = Value<int, string>;
        var Error = Error<int, string>;

        Value(1.AsTask()).Equals(Value(1.AsTask())).ItIs(true);
        Value(1.AsTask()).Equals(Value(2.AsTask())).ItIs(false);

        Error("err1").Equals(Error("err1")).ItIs(true);
        Error("err1").Equals(Error("err2")).ItIs(false);

        Value(1.AsTask()).Equals(Error("err1")).ItIs(false);
    }

    [Test(Description = "Tests ==")]
    public void TestsEqOperator()
    {
        var Value = Value<int, string>;
        var Error = Error<int, string>;

        (Value(1.AsTask()) == Value(1.AsTask())).ItIs(true);
        (Value(1.AsTask()) == Value(2.AsTask())).ItIs(false);

        (Error("err1") == Error("err1")).ItIs(true);
        (Error("err1") == Error("err2")).ItIs(false);

        (Value(1.AsTask()) == Error("err1")).ItIs(false);
    }

    [Test(Description = "Tests !=")]
    public void TestsInEqOperator()
    {
        var Value = Value<int, string>;
        var Error = Error<int, string>;

        (Value(1.AsTask()) != Value(1.AsTask())).ItIs(false);
        (Value(1.AsTask()) != Value(2.AsTask())).ItIs(true);

        (Error("err1") != Error("err1")).ItIs(false);
        (Error("err1") != Error("err2")).ItIs(true);

        (Value(1.AsTask()) != Error("err1")).ItIs(true);
    }

    [Test(Description = "Tests Match")]
    public async Task TestsMatch()
    {
        (
            await Value<int, string>(1.AsTask())
                .Match(
                    async task => $"value: {await task}",
                    error => $"error: {error}".AsTask())
        ).ItIs("value: 1");

        (
            await Error<int, string>("err!")
                .Match(
                    async task => $"value: {await task}",
                    error => $"error: {error}".AsTask())
        ).ItIs("error: err!");
    }
}