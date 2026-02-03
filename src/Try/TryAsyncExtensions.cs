using System;
using System.Threading.Tasks;

namespace Svan.Monads
{
    public static class TryAsyncExtensions
    {
        public static async Task<Result<Exception, TSuccess>> ToResultAsync<TSuccess>(
            this Task<Try<TSuccess>> tryTask)
            => await tryTask;

        public static async Task<Try<TOut>> MapCatching<TSuccess, TOut>(
            Task<Try<TSuccess>> tryTask,
            Func<TSuccess, TOut> mapper)
        {
            var tryResult = await tryTask.ConfigureAwait(false);
            return tryResult.MapCatching(mapper);
        }

        public static async Task<Try<TOut>> MapCatching<TSuccess, TOut>(
            Task<Try<TSuccess>> tryTask,
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

        public async static Task<Try<TOut>> Map<TSuccess, TOut>(
            this Task<Try<TSuccess>> tryTask,
            Func<TSuccess, Task<TOut>> mapSuccess)
        {
            var result = await tryTask.ConfigureAwait(false);
            return await result.Map(mapSuccess).ConfigureAwait(false);
        }

        public async static Task<Try<TOut>> Map<TSuccess, TOut>(
            this Task<Try<TSuccess>> tryTask,
            Func<TSuccess, TOut> mapSuccess)
        {
            var result = await tryTask.ConfigureAwait(false);
            return result.Map(mapSuccess);
        }
    }
}
