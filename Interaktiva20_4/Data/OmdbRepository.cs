using Interaktiva20_4.Models.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Interaktiva20_4.Data
{
    public class OmdbRepository : IOmdbRepository
    {
        private string baseUrl;
        public OmdbRepository(IConfiguration configuration)
        {
            baseUrl = configuration.GetValue<string>("OmdbApi:BaseUrl");
        }
        public async Task<OmdbDTO> SearchForMoviesOnOmdbApi()
        {
            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"{baseUrl}?apikey=547db346&s=fool";
                var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OmdbDTO>(data);
                return result;
            }
        }
    }
}
