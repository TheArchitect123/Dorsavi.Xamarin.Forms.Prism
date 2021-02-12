using System;
using System.Collections.Generic;
using System.Text;

namespace Dorsavi.Xamarin.Forms.Exceptions
{
    public class FailedFetchZeroCountResultException : Exception
    {
        public FailedFetchZeroCountResultException(string message) : base(message)
        {
        }
    }
}
