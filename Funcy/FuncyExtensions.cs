using System;

namespace Funcy
{
    public static class FuncyExtensions
    {
        /// <summary>
        /// Encapsulates the 'fetching data from cache / wherever' pattern
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheService"></param>
        /// <param name="func"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Fetch<T>(this ICacheService cacheService, Func<T> func, string key)
            where T : class
        {
            var result = cacheService.Get<T>(key);
            if (result == null)
            {
                // Invoke the getter to actually get the data...
                result = func();
                cacheService.Add(key, result);
            }
            return result;
        }


        /// <summary>
        /// Bring in a function to create a cache key, and a function to get the data, and return
        /// a function that will, when invoked, create the key and call Fetch...
        /// </summary>
        /// <typeparam name="TKey">The type of the parameter e.g. int for customerId</typeparam>
        /// <typeparam name="TResult">The return type, e.g. Customer</typeparam>
        /// <param name="cacheService"></param>
        /// <param name="makeCacheKey">The Func to create the cache key</param>
        /// <param name="getData">The Func to get the data</param>
        /// <returns>A Func that will bring in a key, create a cache key and invoke Fetch to return a result</returns>
        public static Func<TKey, TResult> Encachify<TKey, TResult>(
            this ICacheService cacheService,
            Func<TKey, string> makeCacheKey,
            Func<TKey, TResult> getData) where TResult : class
        {
            return key =>
                {
                    var cacheKey = makeCacheKey(key);
                    return cacheService.Fetch(() => getData(key), cacheKey);
                };
        }
    }
}