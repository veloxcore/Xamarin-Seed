using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinSeed.Common
{
    public interface ICacheHelper
    {
        Task InsertObject<T>(string key, T value, TimeSpan? expiration = null,
            Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine);

        Task<T> GetObject<T>(string Key, Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine);

        Task<IList<T>> GetAllObjects<T>(Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine);

        Task Invalidate(string key, Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine);

        Task InvalidateAll(Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine);

        Task InvalidateAllObjects<T>(Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine);

        Task Flush(Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine);

        Task Vacuum(Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine);

        Task<List<string>> GetKeysBySuffix(string suffix, Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine);

        Task<List<string>> GetKeysByPrefix(string prefix, Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine);

        Task<List<string>> GetKeysByPrefixs(IEnumerable<string> prefixs, Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine);

        Task<List<string>> GetAllKeysAsync(Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine);

        Task<List<T>> GetObjects<T>(IEnumerable<string> keys, Enums.LogDestination logDestination = Enums.LogDestination.LocalMachine);
    }
}
