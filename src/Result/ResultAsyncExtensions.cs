using System;
using System.Threading.Tasks;

namespace Svan.Monads
{
    public static class ResultAsyncExtensions
    {
        public static async Task<Result<TError, TOut>> BindAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, Task<Result<TError, TOut>>> binder)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result
                .BindAsync(binder)
                .ConfigureAwait(false);
        }

        public static async Task<Result<TError, TOut>> BindAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, Result<TError, TOut>> binder)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Bind(binder);
        }

        public async static Task<Result<TOut, TSuccess>> BindErrorAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<Result<TOut, TSuccess>>> binder)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result
                .BindErrorAsync(binder)
                .ConfigureAwait(false);
        }

        public async static Task<Result<TOut, TSuccess>> BindErrorAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Result<TOut, TSuccess>> binder)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.BindError(binder);
        }

        public async static Task<Result<TError, TOut>> MapAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, Task<TOut>> mapSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.MapAsync(mapSuccess).ConfigureAwait(false);
        }

        public async static Task<Result<TError, TOut>> MapAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, TOut> mapSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Map(mapSuccess);
        }

        public async static Task<Result<TOut, TSuccess>> MapErrorAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<TOut>> mapError)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.MapErrorAsync(mapError).ConfigureAwait(false);
        }

        public async static Task<Result<TOut, TSuccess>> MapErrorAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, TOut> mapError)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.MapError(mapError);
        }

        public async static Task<TOut> FoldAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, TOut> caseError,
            Func<TSuccess, TOut> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Fold(caseError, caseSuccess);
        }

        public async static Task<TOut> FoldAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<TOut>> caseError,
            Func<TSuccess, TOut> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.FoldAsync(caseError, caseSuccess).ConfigureAwait(false);
        }

        public async static Task<TOut> FoldAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, TOut> caseError,
            Func<TSuccess, Task<TOut>> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.FoldAsync(caseError, caseSuccess).ConfigureAwait(false);
        }

        public async static Task<TOut> FoldAsync<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<TOut>> caseError,
            Func<TSuccess, Task<TOut>> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result
                .FoldAsync(caseError, caseSuccess)
                .ConfigureAwait(false);
        }

        public async static Task<Result<TError, TSuccess>> DoAsync<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, Task> @do)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.DoAsync(@do).ConfigureAwait(false);
        }

        public async static Task<Result<TError, TSuccess>> DoAsync<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Action<TSuccess> @do)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Do(@do);
        }

        public async static Task<Result<TError, TSuccess>> DoIfErrorAsync<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task> @do)
        {
            var result = await resultTask.ConfigureAwait(false);
            await result.DoIfErrorAsync(@do).ConfigureAwait(false);
            return result;
        }

        public async static Task<Result<TError, TSuccess>> DoIfErrorAsync<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Action<TError> @do)
        {
            var result = await resultTask.ConfigureAwait(false);
            result.DoIfError(@do);
            return result;
        }
    }
}