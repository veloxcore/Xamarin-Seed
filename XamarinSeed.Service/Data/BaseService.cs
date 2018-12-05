using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinSeed.Common;

namespace XamarinSeed.Service.Data
{
    public abstract class BaseService
    {
        #region Private Members
        IConnectionManager _connectionManager;
        ICacheHelper _cacheHelper;
        #endregion


        #region Protected Members
        protected ICacheHelper CacheHelper
        {
            get
            {
                return _cacheHelper;
            }
        }

        protected IConnectionManager ConnectionManager
        {
            get
            {
                return _connectionManager;
            }
        }
        #endregion

        public BaseService(ICacheHelper cacheHelper, IConnectionManager connectionManager)
        {
            _cacheHelper = cacheHelper;
            _connectionManager = connectionManager;
        }

        #region Protected Methods

        protected async Task GetAndFetch<T>(string key, Func<Task<ServiceResponse<T>>> methodToConnectToServer, Action<ServiceResponse<T>> callback = null, TimeSpan? cacheInterval = null)
        {
            T cacheData = await _cacheHelper.GetObject<T>(key);

            if (cacheData == null && !_connectionManager.IsConnected)
            {
                callback?.Invoke(new ServiceResponse<T>(Common.Enums.ResponseStatus.NoConnection));
                return;
            }

            ServiceResponse<T> response;
            if (cacheData != null)
            {
                callback?.Invoke(new ServiceResponse<T>(cacheData));
            }

            response = await methodToConnectToServer();
            if (response.Status == Common.Enums.ResponseStatus.Success)
                await _cacheHelper.InsertObject(key, response.Data, cacheInterval);

            callback?.Invoke(response);
        }

        protected async Task<ServiceResponse<T>> GetOrFetch<T>(string key, Func<Task<ServiceResponse<T>>> methodToConnectToServer, TimeSpan? cacheInterval = null)
        {
            T cacheData = await _cacheHelper.GetObject<T>(key);

            if (cacheData != null)
                return new ServiceResponse<T>(cacheData);

            if (cacheData == null && !_connectionManager.IsConnected)
                return new ServiceResponse<T>(Common.Enums.ResponseStatus.NoConnection);

            ServiceResponse<T> response = await methodToConnectToServer();

            if (response.Status == Common.Enums.ResponseStatus.Success && response.Data != null)
                await _cacheHelper.InsertObject(key, response.Data, cacheInterval);

            return response;
        }
        #endregion
    }
}
