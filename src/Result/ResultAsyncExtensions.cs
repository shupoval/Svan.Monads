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
            return await result.Bind(binder).ConfigureAwait(false);
        }

        public static async Task<Result<TError, TOut>> Bind<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, Result<TError, TOut>> binder)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Bind(binder);
        }

        public static async Task<Result<TOut, TSuccess>> BindError<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<Result<TOut, TSuccess>>> binder)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.BindError(binder).ConfigureAwait(false);
        }

        public static async Task<Result<TOut, TSuccess>> BindError<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Result<TOut, TSuccess>> binder)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.BindError(binder);
        }

        public static async Task<Result<TOut, TSuccess>> Recover<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Result<TOut, TSuccess>> recover)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Recover(recover);
        }

        public static async Task<Result<TOut, TSuccess>> Recover<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<Result<TOut, TSuccess>>> recover)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.Recover(recover).ConfigureAwait(false);
        }

        public static async Task<Result<TError, TOut>> Map<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, Task<TOut>> mapSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.Map(mapSuccess).ConfigureAwait(false);
        }

        public static async Task<Result<TError, TOut>> Map<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, TOut> mapSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Map(mapSuccess);
        }

        public static async Task<Result<TOut, TSuccess>> MapError<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<TOut>> mapError)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.MapError(mapError).ConfigureAwait(false);
        }

        public static async Task<Result<TOut, TSuccess>> MapError<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, TOut> mapError)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.MapError(mapError);
        }

        public static async Task<Result<TError, TSuccess>> Do<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TSuccess, Task> @do)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.Do(@do).ConfigureAwait(false);
        }

        public static async Task<Result<TError, TSuccess>> Do<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Action<TSuccess> @do)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Do(@do);
        }

        public static async Task<Result<TError, TSuccess>> DoIfError<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task> @do)
        {
            var result = await resultTask.ConfigureAwait(false);
            await result.DoIfError(@do).ConfigureAwait(false);
            return result;
        }

        public static async Task<Result<TError, TSuccess>> DoIfError<TError, TSuccess>(
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

        public static async Task<TSuccess> OrThrow<TError, TSuccess>(this Task<Result<TError, TSuccess>> resultTask)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.OrThrow();
        }

        public static async Task<TOut> Fold<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, TOut> caseError,
            Func<TSuccess, TOut> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Fold(caseError, caseSuccess);
        }

        public static async Task<TOut> Fold<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<TOut>> caseError,
            Func<TSuccess, TOut> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.Fold(caseError, caseSuccess).ConfigureAwait(false);
        }

        public static async Task<TOut> Fold<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, TOut> caseError,
            Func<TSuccess, Task<TOut>> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.Fold(caseError, caseSuccess).ConfigureAwait(false);
        }

        public static async Task<TOut> Fold<TError, TSuccess, TOut>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task<TOut>> caseError,
            Func<TSuccess, Task<TOut>> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result
                .Fold(caseError, caseSuccess)
                .ConfigureAwait(false);
        }

        public static async Task Fold<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask,
            Func<TError, Task> caseError,
            Func<TSuccess, Task> caseSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            await result.Fold(caseError, caseSuccess).ConfigureAwait(false);
        }

        public static async Task<Option<TSuccess>> ToOption<TError, TSuccess>(
            this Task<Result<TError, TSuccess>> resultTask)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.ToOption();
        }
    }
}