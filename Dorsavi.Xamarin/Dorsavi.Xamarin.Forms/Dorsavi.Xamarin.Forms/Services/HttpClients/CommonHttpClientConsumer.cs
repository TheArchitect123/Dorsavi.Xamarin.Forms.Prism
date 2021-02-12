using Dorsavi.Xamarin.Forms.RemoteServer.Models;
using Dorsavi.Xamarin.Forms.RemoteServer.Resources;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dorsavi.Xamarin.Forms.Services.HttpClients
{
    public class CommonHttpClientConsumer
    {
        private readonly HttpClient httpClient = RegisterWebServices.GetService<IHttpClientFactory>().CreateClient(nameof(CommonHttpClientConsumer));

        public async Task<IEnumerable<DorsaviItemsDto>> RetrieveResultsFromAzureRemoteResource()
        {
            IEnumerable<DorsaviItemsDto> responseResults = null;
            using (var request = new HttpRequestMessage(HttpMethod.Get, AzureDorsaviResources.AzureDorsaviTestApiDirectory))
            {
                var response = this.httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).Result;
                if (response != null)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrWhiteSpace(result))
                        responseResults = JsonConvert.DeserializeObject<IEnumerable<DorsaviItemsDto>>(result);
                }
            }

            return responseResults;
        }
    }
}
