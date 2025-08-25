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

        var task1 = Task.FromResult(1);
        var task2 = Task.FromResult(2);

        Value(task1).Equals(Value(task1)).ItIs(true);
        Value(task1).Equals(Value(task2)).ItIs(false);

        Error("err1").Equals(Error("err1")).ItIs(true);
        Error("err1").Equals(Error("err2")).ItIs(false);

        Value(task1).Equals(Error("err1")).ItIs(false);
    }

    [Test(Description = "Tests ==")]
    public void TestsEqOperator()
    {
        var Value = Value<int, string>;
        var Error = Error<int, string>;

        var task1 = Task.FromResult(1);
        var task2 = Task.FromResult(2);

        (Value(task1) == Value(task1)).ItIs(true);
        (Value(task1) == Value(task2)).ItIs(false);

        (Error("err1") == Error("err1")).ItIs(true);
        (Error("err1") == Error("err2")).ItIs(false);

        (Value(task1) == Error("err1")).ItIs(false);
    }

    [Test(Description = "Tests !=")]
    public void TestsInEqOperator()
    {
        var Value = Value<int, string>;
        var Error = Error<int, string>;

        var task1 = Task.FromResult(1);
        var task2 = Task.FromResult(2);

        (Value(task1) != Value(task1)).ItIs(false);
        (Value(task1) != Value(task2)).ItIs(true);

        (Error("err1") != Error("err1")).ItIs(false);
        (Error("err1") != Error("err2")).ItIs(true);

        (Value(task1) != Error("err1")).ItIs(true);
    }

    [Test(Description = "Tests Match")]
    public async Task TestsMatch()
    {
        var task1 = Task.FromResult(1);

        (
            await Value<int, string>(task1)
                .Match(
                    async task => $"value: {await task}",
                    error => Task.FromResult($"error: {error}"))
        ).ItIs("value: 1");

        (
            await Error<int, string>("err!")
                .Match(
                    async task => $"value: {await task}",
                    error => Task.FromResult($"error: {error}"))
        ).ItIs("error: err!");
    }
}