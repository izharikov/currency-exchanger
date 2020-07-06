using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CommerceExchanger.Utils.Extensions
{
    public static class TaskExtensions
    {
        public static TaskAwaiter<TResult[]> GetAwaiter<TResult>(this IEnumerable<Task<TResult>> tasks)
        {
            return Task.WhenAll(tasks).GetAwaiter();
        }
    }
}