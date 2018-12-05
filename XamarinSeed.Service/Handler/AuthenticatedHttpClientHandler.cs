using ModernHttpClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XamarinSeed.Service.Handler
{
    public class AuthenticatedHttpClientHandler : NativeMessageHandler
    {
        private readonly Func<string> _getToken;

        public AuthenticatedHttpClientHandler(Func<string> getToken)
        {
            if (getToken == null)
                throw new ArgumentNullException("getToken");

            this._getToken = getToken;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var auth = request.Headers.Authorization;
            if (auth != null)
            {
                string token = _getToken();
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(auth.Scheme, token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
