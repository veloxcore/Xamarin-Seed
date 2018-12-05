using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinSeed.Common;

namespace XamarinSeed.Service.Data
{
    public interface ICommonService
    {
        Task SetApiUrlAsync(string apiURL);
        Task<string> GetApiUrlAsync();
    }

    public class CommonService : BaseService, ICommonService
    {
        public const string API_URL = "APIUrl";

        public CommonService(ICacheHelper cacheHelper, IConnectionManager connectionManager) 
            : base(cacheHelper, connectionManager)
        {
        }

        public Task SetApiUrlAsync(string apiURL)
        {
            return CacheHelper.InsertObject<string>(API_URL, apiURL);
        }

        public Task<string> GetApiUrlAsync()
        {
            return CacheHelper.GetObject<string>(API_URL);
        }
    }
}
