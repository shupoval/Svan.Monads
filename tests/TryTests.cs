using Xunit;

namespace Svan.Monads.UnitTests
{
    public class TryTests
    {
        [Fact]
        public void Try_can_wrap_caught_exceptions()
        {
            Try.Catching<string>((Func<string>)(() =>
                {
                    throw new Exception("Error that should be caught");
                }))
                .DoIfError((actual) => Assert.IsType<Exception>(actual))
                .Do((_) => Assert.False(true));
        }

        [Fact]
        public Task Try_can_wrap_caught_exceptions_async() =>
            Try.Catching(() => Task.FromException<string>(new Exception("Error that should be caught")))
                .DoIfError((actual) => Assert.IsType<Exception>(actual))
                .Do((_) => Assert.False(true));

        [Fact]
        public void Try_can_be_casted_to_result() =>
            Try.Catching(() => "a string")
                .DoIfError((_) => Assert.True(false))
                .Do((actual) => Assert.Equal("a string", actual))
                .MapCatching((Func<string, string>)(value => throw new ArgumentException("This failed")))
                .DoIfError((exception) => Assert.IsType<ArgumentException>(exception))
                .MapError((exception) =>
                {
                    Assert.Equal("This failed", exception.Message);
                    return new ErrorThatIsNotAnExceptionType();
                })
                .Do((value) => Assert.True(false));

        [Fact]
        public Task Try_can_be_casted_to_result_async1() =>
            Try.Catching(() => Task.FromResult("a string"))
                .DoIfError((_) => Assert.True(false))
                .Do(actual => Assert.Equal("a string", actual))
                .MapCatching(value => Task.FromException<string>(new ArgumentException("This failed")))
                .DoIfError((exception) => Assert.IsType<ArgumentException>(exception))
                .MapError((exception) =>
                {
                    Assert.Equal("This failed", exception.Message);
                    return new ErrorThatIsNotAnExceptionType();
                })
                .Do((value) => Assert.True(false));

        [Fact]
        public Task Try_can_be_casted_to_result_async2() =>
            Try.Catching(() => Task.FromResult("a string"))
                .DoIfError((_) => Assert.True(false))
                .Do(actual => Assert.Equal("a string", actual))
                .MapCatching(value => Task.FromException<string>(new ArgumentException("This failed")))
                .DoIfError((exception) => Assert.IsType<ArgumentException>(exception))
                .MapError((exception) =>
                {
                    Assert.Equal("This failed", exception.Message);
                    return new ErrorThatIsNotAnExceptionType();
                })
                .Do((value) => Assert.True(false));

        [Fact]
        public Task Try_can_be_casted_to_result_async3() =>
            Try.Catching(() => "a string")
                .DoIfError((_) => Assert.True(false))
                .Do((actual) => Assert.Equal("a string", actual))
                .MapCatching((Func<string, string>)(value => throw new ArgumentException("This failed")))
                .DoIfError((exception) => Assert.IsType<ArgumentException>(exception))
                .MapError((exception) =>
                {
                    Assert.Equal("This failed", exception.Message);
                    return Task.FromResult(new ErrorThatIsNotAnExceptionType());
                })
                .Do((value) => Assert.True(false));

        class ErrorThatIsNotAnExceptionType { }
    }
}