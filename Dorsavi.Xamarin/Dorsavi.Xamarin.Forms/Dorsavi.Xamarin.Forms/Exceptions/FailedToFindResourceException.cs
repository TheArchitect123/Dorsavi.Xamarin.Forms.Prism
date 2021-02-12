using System;

namespace Dorsavi.Xamarin.Forms.Exceptions
{

    public class FailedToFindResourceException : Exception
    {
        public FailedToFindResourceException(string message) : base(message)
        {
        }
    }
}
