using System.Collections.Generic;

namespace CommerceExchanger.Utils.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> ExceptSingle<TSource>(this IEnumerable<TSource> source, TSource except)
        {
            return source.ExceptSingle(except, EqualityComparer<TSource>.Default);
        }

        public static IEnumerable<TSource> ExceptSingle<TSource>(this IEnumerable<TSource> source, TSource except,
            IEqualityComparer<TSource> equalityComparer)
        {
            foreach (var el in source)
            {
                if (!equalityComparer.Equals(el, except))
                {
                    yield return el;
                }
            }
        }
    }
}