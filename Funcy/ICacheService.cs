namespace Funcy
{
    public interface ICacheService
    {
        /// <summary>
        /// Gets the cached value of type <typeparamref name="T"/> for the given key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// Add in instance of type <typeparamref name="T"/> and store it in the cache against the given key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Add<T>(string key, T value);
    }
}