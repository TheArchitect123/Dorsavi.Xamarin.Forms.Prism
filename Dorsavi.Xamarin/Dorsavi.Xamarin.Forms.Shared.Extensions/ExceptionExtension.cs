﻿using System;

namespace Dorsavi.Xamarin.Forms.Shared.Extensions
{
    public static class ExceptionExtension
    {
        public static Exception HandleException(this Exception ex)
        {
            //Check the device type that the user is running 

            //If Mobile, the logs will be written into the database
            //If Windows (Desktop Or Surface) the logs will be written into the EventViewer

            //Output a local notification here
            LogExtensions.LogException(ex);
            return ex;
        }
    }
}
