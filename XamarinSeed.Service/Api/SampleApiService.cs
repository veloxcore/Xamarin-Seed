using Polly;
using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XamarinSeed.Model.Entity;

namespace XamarinSeed.Service.Api
{
    [Headers("Accept: application/json")]
    public interface ISampleApi
    {
        [Get("/todos")]
        Task<List<Item>> GetItemsAsync();
    }

    public interface ISampleApiService
    {
        Task<ServiceResponse<List<Item>>> GetItemsAsync();
    }

    public class SampleApiService : ISampleApiService
    {
        ISampleApi _sampleApi;

        public SampleApiService(ISampleApi sampleApi)
        {
            _sampleApi = sampleApi;
        }

        public async Task<ServiceResponse<List<Item>>> GetItemsAsync()
        {
            PolicyResult <List<Item>> policyResponse = await Policy
               .Handle<WebException>().RetryAsync()
               .ExecuteAndCaptureAsync(async () => await _sampleApi.GetItemsAsync());

            return new ServiceResponse<List<Item>>(policyResponse);
        }
    }
}
