using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Demo.Movie.Core.AppSetup;

namespace Demo.Movie.Core.Helpers
{
    public static class AkavacheCache
    {
        public enum Key
        {
            ImageConfig,
            Genres,
            PopularFilms,
        }

        public static void InitCache()
        {
            Registrations.Start(AppConfig.AppName);
        }

        public static void EnsureCaches()
        {
            var caches = new[]
            {
                BlobCache.LocalMachine,
                BlobCache.Secure,
                BlobCache.UserAccount,
                BlobCache.InMemory
            };

            caches.Select(x => x.Flush())
                  .Merge()
                  .Select(_ => Unit.Default)
                  .Wait();
        }

        /// <summary>
        /// Gets the cached object based on the given key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<T> Get<T>(AkavacheCache.Key key)
        {
            T value;

            try
            {
                value = await BlobCache.LocalMachine.GetObject<T>(key.ToString());
            }
            catch // Key not found, return default
            {
                value = default(T);
            }

            return value;
        }

        /// <summary>
        /// Sets the given object based on the given key. This returns true or false based on the success of the insert operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        /// <returns></returns>
        public static bool Set<T>(AkavacheCache.Key key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            try
            {
                BlobCache.LocalMachine.InsertObject<T>(key.ToString(), value, absoluteExpiration);

                return true;
            }
            catch // If error occurs, return false for value being inserted
            {
                return false;
            }
        }


        /// <summary>
        /// Attempts to get the cached object based on the given key. If the item does not exist
        /// or returns an error, call the given Func to return the latest version of an object
        /// and then the result is set in cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fetchFunc"></param>
        /// <param name="absoluteExpiration"></param>
        /// <returns></returns>
        public static async Task<T> GetOrCreate<T>(AkavacheCache.Key key, Func<T> fetchFunc, DateTimeOffset? absoluteExpiration = null)
        {
            T value;

            try
            {
                value = await BlobCache.LocalMachine.GetOrCreateObject<T>(key.ToString(), fetchFunc, absoluteExpiration);
            }
            catch // If error occurs in the method, return default
            {
                value = default;
            }

            return value;
        }

    }
}
