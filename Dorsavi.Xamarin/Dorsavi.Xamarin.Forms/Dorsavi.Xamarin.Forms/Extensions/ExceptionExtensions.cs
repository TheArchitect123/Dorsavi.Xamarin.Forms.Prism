﻿using Dorsavi.Xamarin.Forms.Helpers;
using Dorsavi.Xamarin.Forms.Shared.Extensions;
using System;

namespace Dorsavi.Xamarin.Forms.Extensions
{
    internal static class ExceptionExtensions
    {
        public static void HandleException(this Exception ex)
        {
            if (DeviceApiHelpers.isUWP())
                ex.LogException(); //Log the results into the EventViewer
            else
            {
                //Persist the log into the database

            }
        }
    }
}
