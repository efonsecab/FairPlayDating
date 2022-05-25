using FairPlayDating.Models.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.ClientServices
{
    public class MatchClientService
    {
        private HttpClientService _httpClientService;
        public MatchClientService(HttpClientService httpClientService)
        {
            this._httpClientService = httpClientService;
        }
        public async Task<MatchModel[]?> GetMyMatchesAsync()
        {
            var requestUrl = "api/Match/GetMyMatches";
            var authorizedHttpClient = this._httpClientService.CreateAuthorizedClient();
            var result = await authorizedHttpClient.GetFromJsonAsync<MatchModel[]>(requestUrl);
            return result;
        }
    }
}
