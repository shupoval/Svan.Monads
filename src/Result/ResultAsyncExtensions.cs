using System;
using System.Threading.Tasks;

namespace Svan.Monads
{
    public static class ResultAsyncExtensions
    {
        public static Task<Result<TError, TOut>> BindAsync<TError, TSuccess, TOut>(
            this Result<TError, TSuccess> result,
            Func<TSuccess, Task<Result<TError, TOut>>> binder) =>
            result.Match(
                error => Task.FromResult<Result<TError, TOut>>(error),
                success => binder(success.Value));

        public static async Task<Result<TError, TOut>> BindAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, Task<Result<TError, TOut>>> binder)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result
                .Match(
                    error => Task.FromResult<Result<TError, TOut>>(error),
                    success => binder(success.Value))
                .ConfigureAwait(false);
        }

        public static async Task<Result<TError, TOut>> BindAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, Result<TError, TOut>> binder)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Match(
                error => error.Value,
                success => binder(success.Value));
        }

        public async static Task<Result<TOut, TSuccess>> BindErrorAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<Result<TOut, TSuccess>>> binder)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.Match(
                error => binder(error.Value),
                success => Task.FromResult<Result<TOut, TSuccess>>(success.Value));
        }

        public async static Task<Result<TOut, TSuccess>> BindErrorAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Result<TOut, TSuccess>> binder)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Match(
                error => binder(error.Value),
                success => success.Value);
        }

        public async static Task<Result<TError, TOut>> MapAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, Task<TOut>> mapSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result
                .Match(
                    error => Task.FromResult<Result<TError, TOut>>(error.Value),
                    async success => Result<TError, TOut>.Success(
                        await mapSuccess(success.Value).ConfigureAwait(false)))
                .ConfigureAwait(false);
        }

        public async static Task<Result<TError, TOut>> MapAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, TOut> mapSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Match(
                error => error.Value,
                success => Result<TError, TOut>.Success(mapSuccess(success.Value)));
        }

        public async static Task<Result<TOut, TSuccess>> MapErrorAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<TOut>> mapError)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result
                .Match(
                    async error => Result<TOut, TSuccess>.Error(
                        await mapError(error.Value).ConfigureAwait(false)),
                    success => Task.FromResult<Result<TOut, TSuccess>>(success.Value))
                .ConfigureAwait(false);
        }

        public async static Task<Result<TOut, TSuccess>> MapErrorAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, TOut> mapError)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Match(
                error => Result<TOut, TSuccess>.Error(mapError(error.Value)),
                success => success.Value);
        }

        public async static Task<TOut> FoldAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, TOut> caseError,
            Func<TSuccess, TOut> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Match(
                error => caseError(error.Value),
                success => caseSuccess(success.Value));
        }

        public async static Task<TOut> FoldAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<TOut>> caseError,
            Func<TSuccess, Task<TOut>> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result
                .Match(
                    error => caseError(error.Value),
                    success => caseSuccess(success.Value))
                .ConfigureAwait(false);
        }

        public async static Task<Result<TError, TSuccess>> DoAsync<TError, TSuccess>(
            this Result<TError, TSuccess> result,
            Func<TSuccess, Task> @do)
        {
            if (result.IsSuccess())
            {
                await @do(result.SuccessValue()).ConfigureAwait(false);
            }

            return result;
        }

        public async static Task<Result<TError, TSuccess>> DoAsync<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, Task> @do)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsSuccess())
            {
                await @do(result.SuccessValue()).ConfigureAwait(false);
            }

            return result;
        }

        public async static Task<Result<TError, TSuccess>> DoAsync<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Action<TSuccess> @do)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsSuccess())
            {
                @do(result.SuccessValue());
            }

            return result;
        }

        public async static Task<Result<TError, TSuccess>> DoIfErrorAsync<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task> @do)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsError())
            {
                await @do(result.ErrorValue()).ConfigureAwait(false);
            }

            return result;
        }

        public async static Task<Result<TError, TSuccess>> DoIfErrorAsync<TError, TSuccess>(
            this Result<TError, TSuccess> result,
            Func<TError, Task> @do)
        {
            if (result.IsError())
            {
                await @do(result.ErrorValue()).ConfigureAwait(false);
            }

            return result;
        }

        public async static Task<Result<TError, TSuccess>> DoIfErrorAsync<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Action<TError> @do)
        {
            var result = await resultTask.ConfigureAwait(false);

            if (result.IsError())
            { 
                @do(result.ErrorValue());
            }

            return result;
        }
    }
}