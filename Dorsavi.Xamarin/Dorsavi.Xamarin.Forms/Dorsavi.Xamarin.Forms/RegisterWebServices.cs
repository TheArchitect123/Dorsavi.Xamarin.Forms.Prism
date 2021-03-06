﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using System.Net.Sockets;

namespace Dorsavi.Xamarin.Forms
{
    using Dorsavi.Xamarin.Forms.Constants;
    using Dorsavi.Xamarin.Forms.Extensions;

    /// <summary>
    /// Use this class to register an IHTTPClientFactory, which will be integrated with Polly to manage 
    /// </summary>
    internal class RegisterWebServices
    {
        private static IServiceProvider _serviceProvider;
        public static void InitializeClientFactory()
        {
            var host = new HostBuilder()
                   .ConfigureServices(InitializeHttpServices)
                   .Build();

            _serviceProvider = host.Services;
        }

        public static T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }

        private static bool CatchSocketException(SocketException socketIssue)
        {
            socketIssue.HandleException(); //If a failure occurs to connect to the remote server make sure to log to the event viewer 
            return true;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetPollyRetryPolicies()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrInner<SocketException>((error) => CatchSocketException(error))
                .Or<SocketException>((error) => CatchSocketException(error))
                .WaitAndRetryAsync(3, (retryAttempt) => TimeSpan.FromSeconds(6));
        }

        public static void InitializeHttpServices(IServiceCollection services)
        {
            services.AddHttpClient(HttpClientNames.CommonHttpClientName, client =>
            {
                var config = GetService<IConfiguration>();
                client.Timeout = TimeSpan.FromMinutes(2);

            }).AddPolicyHandler(GetPollyRetryPolicies());
        }
    }
}


