using System;
using System.Threading.Tasks;

namespace Svan.Monads
{
    public static class TryAsyncExtensions
    {
        public static async Task<Result<Exception, TSuccess>> ToResult<TSuccess>(
            this Task<Try<TSuccess>> tryTask)
            => await tryTask.ConfigureAwait(false);

        public static async Task<Try<TOut>> MapCatching<TSuccess, TOut>(
            this Task<Try<TSuccess>> tryTask,
            Func<TSuccess, TOut> mapper)
        {
            var tryResult = await tryTask.ConfigureAwait(false);
            return tryResult.MapCatching(mapper);
        }

        public static async Task<Try<TOut>> MapCatching<TSuccess, TOut>(
            this Task<Try<TSuccess>> tryTask,
            Func<TSuccess, Task<TOut>> mapper)
        {
            var tryResult = await tryTask.ConfigureAwait(false);
            return await tryResult.MapCatching(mapper).ConfigureAwait(false);
        }

        public static async Task<Try<TOut>> Bind<TSuccess, TOut>(
            this Task<Try<TSuccess>> tryTask,
            Func<TSuccess, Try<TOut>> binder)
        {
            var tryResult = await tryTask.ConfigureAwait(false);
            return tryResult.Bind(binder);
        }

        public static async Task<Try<TOut>> Bind<TSuccess, TOut>(
            this Task<Try<TSuccess>> tryTask,
            Func<TSuccess, Task<Try<TOut>>> binder)
        {
            var tryResult = await tryTask.ConfigureAwait(false);
            return await tryResult.Bind(binder).ConfigureAwait(false);
        }

        public static async Task<Try<TOut>> Map<TSuccess, TOut>(
            this Task<Try<TSuccess>> tryTask,
            Func<TSuccess, Task<TOut>> mapSuccess)
        {
            var result = await tryTask.ConfigureAwait(false);
            return await result.Map(mapSuccess).ConfigureAwait(false);
        }

        public static async Task<Try<TOut>> Map<TSuccess, TOut>(
            this Task<Try<TSuccess>> tryTask,
            Func<TSuccess, TOut> mapSuccess)
        {
            var result = await tryTask.ConfigureAwait(false);
            return result.Map(mapSuccess);
        }

        public static async Task<Result<TOut, TSuccess>> MapError<TSuccess, TOut>(
            this Task<Try<TSuccess>> resultTask,
            Func<Exception, TOut> mapError)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.MapError(mapError);
        }

        public static async Task<Try<TSuccess>> Do<TSuccess>(
            this Task<Try<TSuccess>> tryTask,
            Action<TSuccess> @do)
        {
            var tryResult = await tryTask.ConfigureAwait(false);
            tryResult.Do(@do);
            return tryResult;
        }

        public static async Task<Try<TSuccess>> Do<TSuccess>(
            this Task<Try<TSuccess>> tryTask,
            Func<TSuccess, Task> @do)
        {
            var tryResult = await tryTask.ConfigureAwait(false);
            await tryResult.Do(@do).ConfigureAwait(false);
            return tryResult;
        }

        public static async Task<Try<TSuccess>> DoIfError<TSuccess>(
            this Task<Try<TSuccess>> tryTask,
            Action<Exception> @do)
        {
            var tryResult = await tryTask.ConfigureAwait(false);
            tryResult.DoIfError(@do);
            return tryResult;
        }

        public static async Task<Try<TSuccess>> DoIfError<TSuccess>(
            this Task<Try<TSuccess>> tryTask,
            Func<Exception, Task> @do)
        {
            var tryResult = await tryTask.ConfigureAwait(false);
            await tryResult.DoIfError(@do).ConfigureAwait(false);
            return tryResult;
        }

        public static async Task<Try<TSuccessOut>> Zip<TError, TSuccess, TSuccessOut, TSuccessOther>(
            this Task<Try<TSuccess>> tryTask,
            Try<TSuccessOther> other,
            Func<TSuccess, TSuccessOther, TSuccessOut> combine)
        {
            var result = await tryTask.ConfigureAwait(false);
            return result.Zip(other, combine);
        }

        public static async Task<Try<TSuccessOut>> Zip<TError, TSuccess, TSuccessOut, TSuccessFirstOther, TSuccessSecondOther>(
            this Task<Try<TSuccess>> tryTask,
            Try<TSuccessFirstOther> firstOther,
            Try<TSuccessSecondOther> secondOther,
            Func<TSuccess, TSuccessFirstOther, TSuccessSecondOther, TSuccessOut> combine)
        {
            var result = await tryTask.ConfigureAwait(false);
            return result.Zip(firstOther, secondOther, combine);
        }

        public static async Task<Try<TSuccessOut>> Zip<TError, TSuccess, TSuccessOut, TSuccessFirstOther, TSuccessSecondOther, TSuccessThirdOther>(
            this Task<Try<TSuccess>> tryTask,
            Try<TSuccessFirstOther> firstOther,
            Try<TSuccessSecondOther> secondOther,
            Try<TSuccessThirdOther> thirdOther,
            Func<TSuccess, TSuccessFirstOther, TSuccessSecondOther, TSuccessThirdOther, TSuccessOut> combine)
        {
            var result = await tryTask.ConfigureAwait(false);
            return result.Zip(firstOther, secondOther, thirdOther, combine);
        }

        public static async Task<Try<TSuccessOut>> Zip<
            TError,
            TSuccess,
            TSuccessOut,
            TSuccessFirstOther,
            TSuccessSecondOther,
            TSuccessThirdOther,
            TSuccessFourthOther>(
            this Task<Try<TSuccess>> tryTask,
            Try<TSuccessFirstOther> firstOther,
            Try<TSuccessSecondOther> secondOther,
            Try<TSuccessThirdOther> thirdOther,
            Try<TSuccessFourthOther> fourthOther,
            Func<
                TSuccess,
                TSuccessFirstOther,
                TSuccessSecondOther,
                TSuccessThirdOther,
                TSuccessFourthOther,
                TSuccessOut> combine)
        {
            var result = await tryTask.ConfigureAwait(false);
            return result.Zip(firstOther, secondOther, thirdOther, fourthOther, combine);
        }
    }
}
