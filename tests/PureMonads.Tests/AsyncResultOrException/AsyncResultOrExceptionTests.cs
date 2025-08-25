using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PureMonads.Tests;

using static AsyncResult;

[TestFixture(TestName = "AsyncResult or Exception Tests")]
public partial class AsyncResultOrExceptionTests
{
    [Test(Description = "Tests Eq")]
    public void TestsEquality()
    {
        AsyncResult<int> AsyncValue(int val) => Value(Task.FromResult(val));
        var Error = Error<int>;

        var err1 = new Exception("err1");
        var err2 = new Exception("err2");

        AsyncValue(1).Equals(AsyncValue(1)).ItIs(true);
        AsyncValue(1).Equals(AsyncValue(2)).ItIs(false);

        Error(err1).Equals(Error(err1)).ItIs(true);
        Error(err1).Equals(Error(err2)).ItIs(false);

        AsyncValue(1).Equals(Error(err1)).ItIs(false);
    }

    [Test(Description = "Tests ==")]
    public void TestsEqOperator()
    {
        AsyncResult<int> AsyncValue(int val) => Value(Task.FromResult(val));
        var Error = Error<int>;

        var err1 = new Exception("err1");
        var err2 = new Exception("err2");

        (AsyncValue(1) == AsyncValue(1)).ItIs(true);
        (AsyncValue(1) == AsyncValue(2)).ItIs(false);

        (Error(err1) == Error(err1)).ItIs(true);
        (Error(err1) == Error(err2)).ItIs(false);

        (AsyncValue(1) == Error(err1)).ItIs(false);
    }

    [Test(Description = "Tests !=")]
    public void TestsInEqOperator()
    {
        AsyncResult<int> AsyncValue(int val) => Value(Task.FromResult(val));
        var Error = Error<int>;

        var err1 = new Exception("err1");
        var err2 = new Exception("err2");

        (AsyncValue(1) != AsyncValue(1)).ItIs(false);
        (AsyncValue(1) != AsyncValue(2)).ItIs(true);

        (Error(err1) != Error(err1)).ItIs(false);
        (Error(err1) != Error(err2)).ItIs(true);

        (AsyncValue(1) != Error(err1)).ItIs(true);
    }

    [Test(Description = "Tests Match")]
    public async Task TestsMatch()
    {
        AsyncResult<int> AsyncValue(int val) => Value(Task.FromResult(val));

        (
            await AsyncValue(1)
                .Match(
                    async value => $"value: {await value}",
                    error => Task.FromResult($"error: {error.Message}"))
        ).ItIs("value: 1");

        Error<int>(new TestException("err!"))
            .Match(value => $"value: {value}", error => $"error: {error.Message}").ItIs("error: err!");
    }
}