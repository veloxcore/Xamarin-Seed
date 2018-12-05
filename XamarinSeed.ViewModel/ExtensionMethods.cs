using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinSeed.Common;
using XamarinSeed.Service;

namespace XamarinSeed.ViewModel
{
    public static class ExtensionMethods
    {
        public static IUserDialogs UserDialog { get; set; } = UserDialogs.Instance;

        public static void HandleFailure(this ServiceResponse serviceResponse)
        {
            string failureMessage = GetFailureMessage(serviceResponse.Status);
            UserDialog.Toast(failureMessage, TimeSpan.FromSeconds(5));
        }

        public static string GetFailureMessage(this Enums.ResponseStatus status)
        {
            string failureMessage = string.Empty;
            switch (status)
            {
                case Enums.ResponseStatus.Unauthorized:
                    failureMessage = "Unauthorized Request.";
                    break;
                case Enums.ResponseStatus.NotFound:
                case Enums.ResponseStatus.Forbidden:
                    failureMessage = "Unable to connect server.";
                    break;
                case Enums.ResponseStatus.InternalServerError:
                    failureMessage = "Error occured while processing request. Please Contact admin.";
                    break;
                case Enums.ResponseStatus.RequestTimeout:
                    failureMessage = "Connection Timeout. Please try again with proper network connection.";
                    break;
                case Enums.ResponseStatus.NoConnection:
                    failureMessage = "No Internet Connection.";
                    break;
                case Enums.ResponseStatus.HostNotFound:
                    failureMessage = "Server not found.";
                    break;
                default:
                    failureMessage = "We encountered error. Please Contact admin.";
                    break;
            }
            return failureMessage;
        }
    }
}
