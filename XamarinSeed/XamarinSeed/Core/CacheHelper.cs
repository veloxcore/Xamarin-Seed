using Akavache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using XamarinSeed.Common;

namespace XamarinSeed.Core
{
    public class CacheHelper : ICacheHelper
    {
        public CacheHelper()
        {
            BlobCache.ApplicationName = "Xamarin Seed";
        }

        /// <summary>
        /// Flushes this instance.
        /// </summary>
        /// <param name="logDestination">The log destination.</param>
        public async Task Flush(Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine)
        {
            if (logDestination == Enums.LogDestination.LocalMachine)
                await BlobCache.LocalMachine.Flush();
            else
                await BlobCache.UserAccount.Flush();
        }

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<T> GetObject<T>(string key, Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("GetObject<T>: " + key);
                if (logDestination == Enums.LogDestination.LocalMachine)
                    return await BlobCache.LocalMachine.GetObject<T>(key.ToLower());
                else
                    return await BlobCache.UserAccount.GetObject<T>(key.ToLower());
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Gets all objects of type T.
        /// </summary>
        /// <typeparam name="T">Type of objects to fetch.</typeparam>
        /// <returns>List of type T objects from cache</returns>
        public async Task<IList<T>> GetAllObjects<T>(Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine)
        {
            try
            {
                if (logDestination == Enums.LogDestination.LocalMachine)
                    return (await BlobCache.LocalMachine.GetAllObjects<T>()).ToList();
                else
                    return (await BlobCache.UserAccount.GetAllObjects<T>()).ToList();
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// Inserts the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public async Task InsertObject<T>(string key, T value, TimeSpan? expiration = null,
            Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("InsertObject<T>: " + key + ", Value: " + value);

                if (!expiration.HasValue)
                    expiration = TimeSpan.FromDays(500);

                if (logDestination == Enums.LogDestination.LocalMachine)
                    await BlobCache.LocalMachine.InsertObject<T>(key.ToLower(), value, expiration.Value);
                else
                    await BlobCache.UserAccount.InsertObject<T>(key.ToLower(), value, expiration.Value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Invalidates the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task Invalidate(string key, Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine)
        {
            if (logDestination == Enums.LogDestination.LocalMachine)
                await BlobCache.LocalMachine.Invalidate(key.ToLower());
            else
                await BlobCache.UserAccount.Invalidate(key.ToLower());
        }

        /// <summary>
        /// Invalidates all.
        /// </summary>
        /// <returns></returns>
        public async Task InvalidateAll(Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine)
        {
            if (logDestination == Enums.LogDestination.LocalMachine)
                await BlobCache.LocalMachine.InvalidateAll();
            else
                await BlobCache.UserAccount.InvalidateAll();
        }

        /// <summary>
        /// Vacuums this instance.
        /// </summary>
        /// <returns></returns>
        public async Task Vacuum(Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine)
        {
            if (logDestination == Enums.LogDestination.LocalMachine)
                await BlobCache.LocalMachine.Vacuum();
            else
                await BlobCache.UserAccount.Vacuum();
        }

        public async Task InvalidateAllObjects<T>(Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine)
        {
            if (logDestination == Enums.LogDestination.LocalMachine)
                await BlobCache.LocalMachine.InvalidateAllObjects<T>();
            else
                await BlobCache.UserAccount.InvalidateAllObjects<T>();
        }

        public async Task<List<string>> GetKeysBySuffix(string suffix, Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine)
        {
            var keys = logDestination == Enums.LogDestination.LocalMachine ?
                await BlobCache.LocalMachine.GetAllKeys() :
                await BlobCache.UserAccount.GetAllKeys();
            return keys.Where(o => o.EndsWith(suffix, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public async Task<List<string>> GetKeysByPrefix(string prefix, Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine)
        {
            var keys = logDestination == Enums.LogDestination.LocalMachine ?
                await BlobCache.LocalMachine.GetAllKeys() :
                await BlobCache.UserAccount.GetAllKeys();
            return keys.Where(o => o.StartsWith(prefix, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public async Task<List<string>> GetKeysByPrefixs(IEnumerable<string> prefixs, Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine)
        {
            var keys = logDestination == Enums.LogDestination.LocalMachine ?
                await BlobCache.LocalMachine.GetAllKeys() :
                await BlobCache.UserAccount.GetAllKeys();
            List<string> result = new List<string>();
            foreach (string prefix in prefixs)
            {
                result.AddRange(keys.Where(o => o.StartsWith(prefix, StringComparison.CurrentCultureIgnoreCase)));
            }

            return result;
        }

        public async Task<List<T>> GetObjects<T>(IEnumerable<string> keys, Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine)
        {
            try
            {
                IDictionary<string, T> result = logDestination == Enums.LogDestination.LocalMachine ?
                    await BlobCache.LocalMachine.GetObjects<T>(keys.Select(o => o.ToLower())) :
                    await BlobCache.UserAccount.GetObjects<T>(keys.Select(o => o.ToLower()));
                return result.Values.ToList();
            }
            catch (KeyNotFoundException)
            {
                return new List<T>();
            }
        }

        public async Task<List<string>> GetAllKeysAsync(Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine)
        {
            if (logDestination == Enums.LogDestination.LocalMachine)
                return (await BlobCache.LocalMachine.GetAllKeys()).ToList();
            else
                return (await BlobCache.UserAccount.GetAllKeys()).ToList();
        }
    }
}
