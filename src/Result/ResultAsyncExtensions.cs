using System;
using System.Threading.Tasks;

namespace Svan.Monads
{
    public static class ResultAsyncExtensions
    {
        public static async Task<Result<TError, TOut>> Bind<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, Task<Result<TError, TOut>>> binder)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result
                .Bind(binder)
                .ConfigureAwait(false);
        }

        public static async Task<Result<TError, TOut>> Bind<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, Result<TError, TOut>> binder)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Bind(binder);
        }

        public async static Task<Result<TOut, TSuccess>> BindError<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<Result<TOut, TSuccess>>> binder)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result
                .BindError(binder)
                .ConfigureAwait(false);
        }

        public async static Task<Result<TOut, TSuccess>> BindError<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Result<TOut, TSuccess>> binder)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.BindError(binder);
        }

        public async static Task<Result<TOut, TSuccess>> Recover<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Result<TOut, TSuccess>> recover)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Match(
                error => recover(error.Value),
                success => success.Value);
        }

        public async static Task<Result<TOut, TSuccess>> Recover<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<Result<TOut, TSuccess>>> recover)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result
                .Match(
                    error => recover(error.Value),
                    success => Task.FromResult<Result<TOut, TSuccess>>(success.Value))
                .ConfigureAwait(false);
        }

        public async static Task<Result<TError, TOut>> Map<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, Task<TOut>> mapSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.Map(mapSuccess).ConfigureAwait(false);
        }

        public async static Task<Result<TError, TOut>> Map<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, TOut> mapSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Map(mapSuccess);
        }

        public async static Task<Result<TOut, TSuccess>> MapError<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<TOut>> mapError)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.MapError(mapError).ConfigureAwait(false);
        }

        public async static Task<Result<TOut, TSuccess>> MapError<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, TOut> mapError)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.MapError(mapError);
        }

        public async static Task<TOut> Fold<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, TOut> caseError,
            Func<TSuccess, TOut> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Fold(caseError, caseSuccess);
        }

        public async static Task<TOut> Fold<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<TOut>> caseError,
            Func<TSuccess, TOut> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.Fold(caseError, caseSuccess).ConfigureAwait(false);
        }

        public async static Task<TOut> Fold<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, TOut> caseError,
            Func<TSuccess, Task<TOut>> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.Fold(caseError, caseSuccess).ConfigureAwait(false);
        }

        public async static Task<TOut> Fold<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<TOut>> caseError,
            Func<TSuccess, Task<TOut>> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result
                .Fold(caseError, caseSuccess)
                .ConfigureAwait(false);
        }

        public async static Task Fold<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task> caseError,
            Func<TSuccess, Task> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            await result.Fold(caseError, caseSuccess).ConfigureAwait(false);
        }

        public async static Task<Result<TError, TSuccess>> Do<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, Task> @do)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.Do(@do).ConfigureAwait(false);
        }

        public async static Task<Result<TError, TSuccess>> Do<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Action<TSuccess> @do)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Do(@do);
        }

        public async static Task<Result<TError, TSuccess>> DoIfError<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task> @do)
        {
            var result = await resultTask.ConfigureAwait(false);
            await result.DoIfError(@do).ConfigureAwait(false);
            return result;
        }

        public async static Task<Result<TError, TSuccess>> DoIfError<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Action<TError> @do)
        {
            var result = await resultTask.ConfigureAwait(false);
            result.DoIfError(@do);
            return result;
        }

        public static async Task<TSuccess> DefaultWith<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, TSuccess> fallback)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.DefaultWith(fallback);
        }

        public static async Task<TSuccess> DefaultWith<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<TSuccess>> fallback)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.DefaultWith(fallback).ConfigureAwait(false);
        }
    }
}