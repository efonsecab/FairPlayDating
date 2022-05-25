using FairPlayDating.Common.Global;
using FairPlayDating.Models.Facebook.GetMyAlbums;
using FairPlayDating.Models.Facebook.GetMyPhotos;
using System.Net.Http.Json;

namespace FairPlayDating.ClientServices
{
    public class FacebookClientService
    {
        private HttpClientService HttpClientService { get; }
        public FacebookClientService(HttpClientService httpClientService)
        {
            this.HttpClientService = httpClientService;
        }


        public async Task<GetMyPhotosResponse?> GetMyPhotosAsync(string? pageToken = null)
        {
            string requestUrl = $"{Constants.ApiRoutes.GetMyPhotos}?pageToken={pageToken}";
            var authorizedHttpClinet = this.HttpClientService.CreateAuthorizedClient();
            var result = await authorizedHttpClinet.GetFromJsonAsync<GetMyPhotosResponse>(requestUrl);
            return result;
        }

        public async Task<GetMyAlbumsResponse?> GetMyAlbumsAsync(string? pageToken = null)
        {
            string requestUrl = $"{Constants.ApiRoutes.GetMyAlbums}?pageToken={pageToken}";
            var authorizedHttpClinet = this.HttpClientService.CreateAuthorizedClient();
            var result = await authorizedHttpClinet.GetFromJsonAsync<GetMyAlbumsResponse>(requestUrl);
            return result;
        }
    }
}
