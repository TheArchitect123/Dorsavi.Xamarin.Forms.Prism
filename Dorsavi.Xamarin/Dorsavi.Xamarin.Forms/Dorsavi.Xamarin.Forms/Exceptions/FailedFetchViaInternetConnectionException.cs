using System;

namespace Dorsavi.Xamarin.Forms.Exceptions
{
    public class FailedFetchViaInternetConnectionException : Exception
    {
        public FailedFetchViaInternetConnectionException(string message) : base(message)
        {
        }
    }
}
