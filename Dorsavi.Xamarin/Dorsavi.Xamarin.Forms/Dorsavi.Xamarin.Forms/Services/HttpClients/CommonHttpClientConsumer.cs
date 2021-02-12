using Dorsavi.Xamarin.Forms.Exceptions;
using Dorsavi.Xamarin.Forms.RemoteServer.Models;
using Dorsavi.Xamarin.Forms.RemoteServer.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dorsavi.Xamarin.Forms.Services.HttpClients
{
    public static class CommonHttpClientConsumer
    {
        public static async Task<List<DorsaviItemsDto>> RetrieveMelbourneResultsFromAzureRemoteResource()
        {
            List<DorsaviItemsDto> responseResults = null;
            using (var request = new HttpRequestMessage(HttpMethod.Get, AzureDorsaviResources.AzureDorsaviTestApiDirectory))
            {
                try
                {
                    var response = await HttpClientHelper.GetHttpClient().SendAsync(request, HttpCompletionOption.ResponseContentRead);
                    if (response != null)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            if ((responseResults = JsonConvert.DeserializeObject<List<DorsaviItemsDto>>(result)).Count == 0)
                                throw new FailedFetchZeroCountResultException("Failed to retrieve any results from the server");
                        }
                        else
                            throw new FailedFetchZeroCountResultException("Failed to retrieve any results from the server");
                    }
                }
                catch (HttpRequestException httpFailed)
                {
                    throw new FailedFetchViaInternetConnectionException(httpFailed.Message);
                }
                catch (Exception ex)
                {
                    throw new FailedToFindResourceException("Could not find the specified resource");
                }

                return responseResults;
            }
        }
    }

    /// <summary>
    /// Fetch the HttpClient from HttpClientFactory
    /// </summary>
    /// <returns></returns>
    internal static class HttpClientHelper
    {
        public static HttpClient GetHttpClient() => RegisterWebServices.GetService<IHttpClientFactory>().CreateClient(nameof(CommonHttpClientConsumer));
    }
}
