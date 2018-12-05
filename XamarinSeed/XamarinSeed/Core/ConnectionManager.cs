using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinSeed.Common;

namespace XamarinSeed.Core
{
    public class ConnectionManager : IConnectionManager
    {
        public bool IsConnected
        {
            get
            {
                return CrossConnectivity.Current.IsConnected;
            }
        }

        public event EventHandler ConnectivityChanged;

        public ConnectionManager()
        {
            CrossConnectivity.Current.ConnectivityChanged += (s, e) =>
            {
                ConnectivityChanged?.Invoke(s, e);
            };
        }
    }
}
