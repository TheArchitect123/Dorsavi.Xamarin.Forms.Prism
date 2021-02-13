using System;
using System.Collections.Generic;
using System.Text;

namespace Dorsavi.Xamarin.Forms.RemoteServer.HttpConsumers.Exceptions
{
    public class FailedFetchZeroCountResultException : Exception
    {
        public FailedFetchZeroCountResultException(string message) : base(message)
        {
        }
    }
}
