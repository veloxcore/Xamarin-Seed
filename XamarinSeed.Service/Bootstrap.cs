using Autofac;
using ModernHttpClient;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XamarinSeed.Common;
using XamarinSeed.Service.Api;
using XamarinSeed.Service.Handler;

namespace XamarinSeed.Service
{
    public class Bootstrap
    {
        public static void RegisterDependency(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Bootstrap).GetTypeInfo().Assembly)
               .Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
            
            builder.Register(c => RegisterNativeService<ISampleApi>(c));
        }

        /// <summary>
        /// This method is for register the api service for authorization request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        private static T RegisterService<T>(IComponentContext c)
        {
            ICacheHelper _cacheHelper = c.Resolve<ICacheHelper>();
            
            // Usually we take the rest api url from persistant store, but for demo it is static
            //string apiUrl = Task.Run<string>(() => _cacheHelper.GetObject<string>(Data.CommonService.API_URL).Result).Result;
            string apiUrl = "https://jsonplaceholder.typicode.com/";
            Func<string> GetToken = () =>
            {
                return Task.Run<string>(() => _cacheHelper.GetObject<string>(Data.AccountService.AUTH_TOKEN).Result).Result;
            };

            var client = new HttpClient(new AuthenticatedHttpClientHandler(GetToken))
            {
                BaseAddress = new Uri(apiUrl)
            };

            return RestService.For<T>(client);
        }

        private static T RegisterNativeService<T>(IComponentContext c)
        {
            ICacheHelper _cacheHelper = c.Resolve<ICacheHelper>();
            // Usually we take the rest api url from persistant store, but for demo it is static
            //string apiUrl = Task.Run<string>(() => _cacheHelper.GetObject<string>(Data.CommonService.API_URL).Result).Result;
            string apiUrl = "https://jsonplaceholder.typicode.com/";
            var client = new HttpClient(new NativeMessageHandler())
            {
                BaseAddress = new Uri(apiUrl)
            };

            return RestService.For<T>(client);
        }
    }
}
