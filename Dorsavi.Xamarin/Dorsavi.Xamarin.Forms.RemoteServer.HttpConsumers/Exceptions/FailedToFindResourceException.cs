using System;

namespace Dorsavi.Xamarin.Forms.RemoteServer.HttpConsumers.Exceptions
{

    public class FailedToFindResourceException : Exception
    {
        public FailedToFindResourceException(string message) : base(message)
        {
        }
    }
}
