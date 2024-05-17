using System;

namespace PureMonads.Tests;

public class TestException : Exception
{
    public TestException(string message) : base(message) {}

    public override bool Equals(object? obj) => obj is TestException other && other.Message == Message;

    public override int GetHashCode() => Message?.GetHashCode() ?? 0;
}