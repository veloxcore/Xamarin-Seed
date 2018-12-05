using Polly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using XamarinSeed.Common;
using XamarinSeed.Model;

namespace XamarinSeed.Service
{
    public class ServiceResponse
    {
        public Enums.ResponseStatus Status { get; set; }
        public string ErrorMessage { get; set; }

        #region Constructors
        public ServiceResponse(PolicyResult policyResponse)
        {
            if (policyResponse.Outcome == OutcomeType.Successful)
                this.Status = Enums.ResponseStatus.Success;
            else
            {
                CreateErrorServiceResponse(policyResponse.FinalException);
            }
        }

        public ServiceResponse(Enums.ResponseStatus status)
        {
            this.Status = status;
        }

        public ServiceResponse()
        {

        }
        #endregion

        #region Protected Methods
        protected void CreateErrorServiceResponse(Exception finalException)
        {
            if (finalException is Refit.ApiException)
            {
                HttpStatusCode statusCode = ((Refit.ApiException)finalException).StatusCode;
                if (statusCode == HttpStatusCode.BadRequest)
                    this.Status = Enums.ResponseStatus.BadRequest;

                if (statusCode == HttpStatusCode.Unauthorized)
                    this.Status = Enums.ResponseStatus.Unauthorized;

                if (statusCode == HttpStatusCode.NotFound)
                    this.Status = Enums.ResponseStatus.NotFound;

                if (statusCode == HttpStatusCode.InternalServerError)
                    this.Status = Enums.ResponseStatus.InternalServerError;
            }
            else if (finalException?.InnerException is Refit.ApiException)
            {
                HttpStatusCode statusCode = ((Refit.ApiException)finalException.InnerException).StatusCode;
                if (statusCode == HttpStatusCode.BadRequest)
                    this.Status = Enums.ResponseStatus.BadRequest;

                if (statusCode == HttpStatusCode.Unauthorized)
                    this.Status = Enums.ResponseStatus.Unauthorized;

                if (statusCode == HttpStatusCode.NotFound)
                    this.Status = Enums.ResponseStatus.NotFound;

                if (statusCode == HttpStatusCode.InternalServerError)
                    this.Status = Enums.ResponseStatus.InternalServerError;
            }
            else if (finalException is System.Net.WebException)
            {
                var status = ((System.Net.WebException)finalException).Status;
                if (status == WebExceptionStatus.ConnectFailure)
                {
                    if (finalException.InnerException.Message.Contains("Network is unreachable"))
                        this.Status = Enums.ResponseStatus.NoConnection;
                    if (finalException.InnerException.Message.Contains("No route to host"))
                        this.Status = Enums.ResponseStatus.HostNotFound;
                }

                if (finalException.Message.Contains("NameResolutionFailure"))
                    this.Status = Enums.ResponseStatus.NoConnection;
            }
            else if (finalException?.InnerException is System.Net.WebException)
            {
                var status = ((System.Net.WebException)finalException.InnerException).Status;
                if (status == WebExceptionStatus.ConnectFailure)
                {
                    if (finalException.InnerException.Message.Contains("Network is unreachable"))
                        this.Status = Enums.ResponseStatus.NoConnection;
                    if (finalException.InnerException.Message.Contains("No route to host"))
                        this.Status = Enums.ResponseStatus.HostNotFound;
                }

                if (finalException.Message.Contains("NameResolutionFailure"))
                    this.Status = Enums.ResponseStatus.NoConnection;
            }
            else if (finalException?.InnerException?.InnerException is System.Net.WebException)
            {
                var status = ((System.Net.WebException)finalException.InnerException.InnerException).Status;
                if (status == WebExceptionStatus.ConnectFailure)
                {
                    //No internet connection
                    if (finalException.InnerException.Message.Contains("Network is unreachable"))
                        this.Status = Enums.ResponseStatus.NoConnection;
                    //When service is unavailable.
                    if (finalException.InnerException.Message.Contains("No route to host"))
                        this.Status = Enums.ResponseStatus.HostNotFound;
                }
                //When making request to service in offline mode
                if (finalException.Message.Contains("NameResolutionFailure"))
                    this.Status = Enums.ResponseStatus.NoConnection;
            }
            else if (finalException is System.Threading.Tasks.TaskCanceledException)
            {
                //Background Task canceled due to request time out
                this.Status = Enums.ResponseStatus.RequestTimeout;
            }
            else if (finalException.InnerException != null &&
                finalException.InnerException.Message.Contains("ENETUNREACH (Network is unreachable)"))
            {
                //When try to sign in and no internet connection.
                this.Status = Enums.ResponseStatus.NoConnection;
            }
            else if (finalException.Message.Contains("No address associated with hostname"))
            {
                //When device is not connected to internet and trying to request azure service 
                this.Status = Enums.ResponseStatus.NoConnection;
            }
            else if (finalException.Message.Contains("EHOSTUNREACH (No route to host)"))
            {
                //When device is not connected to internet and trying to request local service
                this.Status = Enums.ResponseStatus.HostNotFound;
            }
            else if (finalException.Message.Contains("Socket closed"))
            {
                this.Status = Enums.ResponseStatus.NoConnection;
            }

            if (finalException is Refit.ApiException && !string.IsNullOrEmpty(((Refit.ApiException)finalException).Content))
                this.ErrorMessage = ((Refit.ApiException)finalException).Content;
            else if (this.Status == Enums.ResponseStatus.InternalServerError)
                this.ErrorMessage = finalException.Message;
        }
        #endregion
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public T Data { get; set; }

        public ServiceResponse(Enums.ResponseStatus status = Enums.ResponseStatus.Success)
            : base(status)
        {

        }

        public ServiceResponse(T responseData, Enums.ResponseStatus status = Enums.ResponseStatus.Success)
            : this(status)
        {
            this.Data = responseData;
        }

        public ServiceResponse(PolicyResult<T> policyResponse) : base()
        {
            if (policyResponse.Outcome == OutcomeType.Successful)
            {
                this.Status = Enums.ResponseStatus.Success;
                this.Data = policyResponse.Result;
            }
            else
            {
                CreateErrorServiceResponse(policyResponse.FinalException);
            }
        }
    }
}
