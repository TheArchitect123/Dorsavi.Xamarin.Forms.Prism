using System;

namespace Dorsavi.Xamarin.Forms.RemoteServer.HttpConsumers.Exceptions
{
    public class FailedFetchViaInternetConnectionException : Exception
    {
        public FailedFetchViaInternetConnectionException(string message) : base(message)
        {
        }
    }
}
