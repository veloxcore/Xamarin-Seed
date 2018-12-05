using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinSeed.Common;
using XamarinSeed.Model.Entity;
using XamarinSeed.Service.Api;

namespace XamarinSeed.Service.Data
{
    public interface ISampleService
    {
        Task GetAndFetchSampleDataAsync(Action<ServiceResponse<List<Item>>> subscriber = null);
    }

    public class SampleService : BaseService, ISampleService
    {
        #region Keys
        public const string SAMPLE_DATA_KEY = "SampleData";
        #endregion

        ISampleApiService _sampleApiService;

        public SampleService(ISampleApiService sampleApiService, ICacheHelper cacheHelper, IConnectionManager connectionManager)
            : base(cacheHelper, connectionManager)
        {
            _sampleApiService = sampleApiService;
        }

        public async Task GetAndFetchSampleDataAsync(Action<ServiceResponse<List<Item>>> subscriber = null)
        {
            await GetAndFetch<List<Item>>(SAMPLE_DATA_KEY,() => _sampleApiService.GetItemsAsync(), subscriber);
        }
    }
}
