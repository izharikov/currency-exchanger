using System.Collections.Generic;
using System.Linq;

namespace CommerceExchanger.Utils.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> source, TSource except)
        {
            return source.Except(new[] {except});
        }
    }
}