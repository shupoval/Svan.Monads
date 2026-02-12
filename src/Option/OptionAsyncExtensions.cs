using System;
using System.Threading.Tasks;

namespace Svan.Monads
{
    public static class OptionAsyncExtensions
    {
        public static async Task<Option<TOut>> Bind<T, TOut>(
            this Task<Option<T>> optionTask,
            Func<T, Task<Option<TOut>>> binder)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option
                .Bind(binder)
                .ConfigureAwait(false);
        }

        public static async Task<Option<TOut>> Bind<T, TOut>(
            this Task<Option<T>> optionTask,
            Func<T, Option<TOut>> binder)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Bind(binder);
        }

        public static async Task<Option<TOut>> Map<T, TOut>(
            this Task<Option<T>> optionTask,
            Func<T, Task<TOut>> mapSuccess)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.Map(mapSuccess).ConfigureAwait(false);
        }

        public static async Task<Option<TOut>> Map<T, TOut>(
            this Task<Option<T>> optionTask,
            Func<T, TOut> mapSuccess)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Map(mapSuccess);
        }

        /// <summary>
        /// Filter the value using a filter function. The filter function will not be executed if the current state of the option is <c>None</c>.
        /// </summary>
        /// <param name="optionTask"></param>
        /// <param name="filter"></param>
        /// <returns><c>Some</c> when filter returns true. <c>None</c> when filter returns false or current state of option is <c>None</c></returns>
        public static async Task<Option<T>> Filter<T>(
            this Task<Option<T>> optionTask,
            Func<T, bool> filter)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Filter(filter);
        }

        /// <summary>
        /// Filter the value using a filter function. The filter function will not be executed if the current state of the option is <c>None</c>.
        /// </summary>
        /// <param name="optionTask"></param>
        /// <param name="filter"></param>
        /// <returns><c>Some</c> when filter returns true. <c>None</c> when filter returns false or current state of option is <c>None</c></returns>
        public static async Task<Option<T>> Filter<T>(
            this Task<Option<T>> optionTask,
            Func<T, Task<bool>> filter)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.Filter(filter).ConfigureAwait(false);
        }

        public static async Task<Option<T>> Do<T>(
            this Task<Option<T>> optionTask,
            Func<T, Task> @do)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.Do(@do).ConfigureAwait(false);
        }

        public static async Task<Option<T>> Do<T>(
            this Task<Option<T>> optionTask,
            Action<T> @do)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Do(@do);
        }

        public static async Task<Option<T>> DoIfNone<T>(
            this Task<Option<T>> optionTask,
            Func<Task> @do)
        {
            var option = await optionTask.ConfigureAwait(false);
            await option.DoIfNone(@do).ConfigureAwait(false);
            return option;
        }

        public static async Task<Option<T>> DoIfNone<T>(
            this Task<Option<T>> optionTask,
            Action @do)
        {
            var option = await optionTask.ConfigureAwait(false);
            option.DoIfNone(@do);
            return option;
        }

        public static async Task<TOut> Fold<T, TOut>(
            this Task<Option<T>> optionTask,
            Func<TOut> caseNone,
            Func<T, TOut> caseSome)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Fold(caseNone, caseSome);
        }

        public static async Task<TOut> Fold<T, TOut>(
            this Task<Option<T>> optionTask,
            Func<Task<TOut>> caseNone,
            Func<T, TOut> caseSome)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.Fold(caseNone, caseSome).ConfigureAwait(false);
        }

        public static async Task<TOut> Fold<T, TOut>(
            this Task<Option<T>> optionTask,
            Func<TOut> caseNone,
            Func<T, Task<TOut>> caseSome)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.Fold(caseNone, caseSome).ConfigureAwait(false);
        }

        public static async Task<TOut> Fold<T, TOut>(
            this Task<Option<T>> optionTask,
            Func<Task<TOut>> caseNone,
            Func<T, Task<TOut>> caseSome)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option
                .Fold(caseNone, caseSome)
                .ConfigureAwait(false);
        }

        public static async Task Fold<T>(
            this Task<Option<T>> optionTask,
            Func<Task> caseNone,
            Func<T, Task> caseSome)
        {
            var option = await optionTask.ConfigureAwait(false);
            await option.Fold(caseNone, caseSome).ConfigureAwait(false);
        }

        public static async Task<T> DefaultWith<T>(
            this Task<Option<T>> optionTask,
            Func<T> fallback)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.DefaultWith(fallback);
        }

        public static async Task<T> DefaultWith<T>(
            this Task<Option<T>> optionTask,
            Func<Task<T>> fallback)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.DefaultWith(fallback).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the value of <c>Some</c> or throw a <see cref="NullReferenceException"/>.
        /// </summary>
        public static async Task<T> OrThrow<T>(
            this Task<Option<T>> optionTask)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.OrThrow();
        }

        /// <summary>
        /// Combine several options into a new option or <c>None</c> if any of the provided options are <c>None</c>
        /// </summary>
        public static async Task<Option<TOut>> Zip<T, TOut, TOther>(
            this Task<Option<T>> optionTask,
            Option<TOther> other,
            Func<T, TOther, TOut> combine)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Zip(other, combine);
        }

        /// <summary>
        /// Combine several options into a new option or <c>None</c> if any of the provided options are <c>None</c>
        /// </summary>
        public static async Task<Option<TOut>> Zip<T, TOut, TFirstOther, TSecondOther>(
            this Task<Option<T>> optionTask,
            Option<TFirstOther> firstOther,
            Option<TSecondOther> secondOther,
            Func<T, TFirstOther, TSecondOther, TOut> combine)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Zip(firstOther, secondOther, combine);
        }

        /// <summary>
        /// Combine several options into a new option or <c>None</c> if any of the provided options are <c>None</c>
        /// </summary>
        public static async Task<Option<TOut>> Zip<T, TOut, TFirstOther, TSecondOther, TThirdOther>(
            this Task<Option<T>> optionTask,
            Option<TFirstOther> firstOther,
            Option<TSecondOther> secondOther,
            Option<TThirdOther> thirdOther,
            Func<T, TFirstOther, TSecondOther, TThirdOther, TOut> combine)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Zip(firstOther, secondOther, thirdOther, combine);
        }

        /// <summary>
        /// Combine several options into a new option or <c>None</c> if any of the provided options are <c>None</c>
        /// </summary>
        public static async Task<Option<TOut>> Zip<T, TOut, TFirstOther, TSecondOther, TThirdOther, TFourthOther>(
            this Task<Option<T>> optionTask,
            Option<TFirstOther> firstOther,
            Option<TSecondOther> secondOther,
            Option<TThirdOther> thirdOther,
            Option<TFourthOther> fourthOther,
            Func<T, TFirstOther, TSecondOther, TThirdOther, TFourthOther, TOut> combine)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Zip(firstOther, secondOther, thirdOther, fourthOther, combine);
        }

        /// <summary>
        /// Merge two or more options together. Merge will only be performed if all involved options resolve to Some.
        /// </summary>
        /// <returns>The values merged into an option of a tuple</returns>
        public static async Task<Option<Tuple<TFirst, TSecond>>> Merge<TFirst, TSecond>(
            this Task<Option<TFirst>> firstTask,
            Option<TSecond> second)
        {
            var first = await firstTask.ConfigureAwait(false);
            return first.Merge(second);
        }

        /// <summary>
        /// Merge two or more options together. Merge will only be performed if all involved options resolve to Some.
        /// </summary>
        /// <returns>The values merged into an option of a tuple</returns>
        public static async Task<Option<Tuple<TFirst, TSecond, TThird>>> Merge<TFirst, TSecond, TThird>(
            this Task<Option<Tuple<TFirst, TSecond>>> groupTask,
            Option<TThird> other)
        {
            var group = await groupTask.ConfigureAwait(false);
            return group.Merge(other);
        }

        /// <summary>
        /// Merge two or more options together. Merge will only be performed if all involved options resolve to Some.
        /// </summary>
        /// <returns>The values merged into an option of a tuple</returns>
        public static async Task<Option<Tuple<TFirst, TSecond, TThird, TFourth>>> Merge<TFirst, TSecond, TThird, TFourth>(
            this Task<Option<Tuple<TFirst, TSecond, TThird>>> groupTask,
            Option<TFourth> other)
        {
            var group = await groupTask.ConfigureAwait(false);
            return group.Merge(other);
        }

        /// <summary>
        /// Merge two or more options together. Merge will only be performed if all involved options resolve to Some.
        /// </summary>
        /// <returns>The values merged into an option of a tuple</returns>
        public static async Task<Option<Tuple<TFirst, TSecond, TThird, TFourth, TFifth>>> Merge<TFirst, TSecond, TThird, TFourth, TFifth>(
            this Task<Option<Tuple<TFirst, TSecond, TThird, TFourth>>> groupTask,
            Option<TFifth> other)
        {
            var group = await groupTask.ConfigureAwait(false);
            return group.Merge(other);
        }

        public static async Task<Option<T>> ToOption<T>(this Task<T> valueTask)
        {
            var value = await valueTask.ConfigureAwait(false);
            return value.ToOption();
        }

        public static async Task<Result<TError, T>> ToResult<TError, T>(
            this Task<Option<T>> optionTask,
            Func<TError> defaultError)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.ToResult(defaultError);
        }
    }
}
