using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InnerCore.Api.DeConz.Extensions
{
    /// <summary>
    /// Enumerable Helpers
    /// </summary>
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// http://blogs.msdn.com/b/pfxteam/archive/2012/03/04/10277325.aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="dop"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static Task ForEachAsync<T>(this IEnumerable<T> source, int dop, Func<T, Task> body)
        {
            var semaphore = new SemaphoreSlim(initialCount: dop, maxCount: dop);

            return Task.WhenAll(from item in source select ProcessAsync(item, body, semaphore));
        }

        private static async Task ProcessAsync<T>(
            T item,
            Func<T, Task> body,
            SemaphoreSlim semaphore
        )
        {
            //Wait untill we are allowed to start
            await semaphore.WaitAsync().ConfigureAwait(false);

            try
            {
                //Do the processing
                await body(item).ConfigureAwait(false);
            }
            finally
            {
                //Release for others
                semaphore.Release();
            }
        }
    }
}
