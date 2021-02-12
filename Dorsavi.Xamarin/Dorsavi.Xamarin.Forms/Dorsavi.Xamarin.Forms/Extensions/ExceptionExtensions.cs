﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Dorsavi.Xamarin.Forms.Extensions
{
    internal static class ExceptionExtensions
    {
        public static Exception HandleException(this Exception ex)
        {
            //Check the device type that the user is running 

            //If Mobile, the logs will be written into the database
            //If Windows (Desktop Or Surface) the logs will be written into the EventViewer

            //Output a local notification here

            if(Device.RuntimePlatform == Device.UWP)
            {

            }
            LogExtensions.LogException(ex);
            return ex;
        }

    }
}