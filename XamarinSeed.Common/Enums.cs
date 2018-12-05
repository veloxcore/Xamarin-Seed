using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSeed.Common
{
    public class Enums
    {
        public enum LogDestination
        {
            LocalMachine,
            UserAccount
        }

        public enum MediatorMessageType
        {
            AddItem,
        }

        public enum ResponseStatus
        {
            None = 0,
            //When the request to sever is successful(200)
            Success = 1,
            //When the request contains wrong data(400)
            BadRequest = 2,
            //Authorization token is not added in the request(401)
            Unauthorized = 3,
            //webpage you were trying to reach could not be found on the server(404)
            NotFound = 4,
            //server understood the request but refuses to authorize it(403)
            Forbidden = 5,
            //Server Error(500)
            InternalServerError = 6,
            //Not receiving a response from the backend servers within the allowed time period
            RequestTimeout = 7,
            NoConnection = 8,
            HostNotFound = 9
        }

        public enum ViewType
        {
            MainPage,
            Menu,
            Items,
            ItemDetails,
            NewItem,
            About
        }
    }
}
