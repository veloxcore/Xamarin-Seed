using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSeed.Common
{
    public interface IConnectionManager
    {
        bool IsConnected { get; }
        event EventHandler ConnectivityChanged;
    }
}
