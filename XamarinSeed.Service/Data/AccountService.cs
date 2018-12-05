using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinSeed.Common;

namespace XamarinSeed.Service.Data
{
    public interface IAccountService
    {
        Task SetAuthTokenAsync(string token, TimeSpan? expiration = null);
        Task<string> GetAuthTokenAsync();
    }

    public class AccountService : BaseService, IAccountService
    {
        public const string AUTH_TOKEN = "Auth_Token";

        public AccountService(ICacheHelper cacheHelper, IConnectionManager connectionManager) 
            : base(cacheHelper, connectionManager)
        {
        }

        public Task<string> GetAuthTokenAsync()
        {
            return CacheHelper.GetObject<string>(AUTH_TOKEN);
        }

        public Task SetAuthTokenAsync(string token, TimeSpan? expiration = null)
        {
            return CacheHelper.InsertObject<string>(AUTH_TOKEN, token, expiration);
        }
    }
}
