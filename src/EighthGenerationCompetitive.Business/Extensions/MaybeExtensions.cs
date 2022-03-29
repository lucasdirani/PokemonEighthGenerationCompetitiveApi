using EighthGenerationCompetitive.Business.Monads;
using System;
using System.Diagnostics.CodeAnalysis;

namespace EighthGenerationCompetitive.Business.Extensions
{
    public static class MaybeExtensions
    {
        public static T Unwrap<T>(this Maybe<T> maybe, [AllowNull] T defaultValue = null)
            where T : class
        {
            return maybe.Unwrap(x => x, defaultValue);
        }

        public static K Unwrap<T, K>(this Maybe<T> maybe, Func<T, K> selector, K defaultValue = default)
            where T : class
        {
            return maybe.HasValue ? selector(maybe.Value) : defaultValue;
        }
    }
}