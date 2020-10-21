using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Interaktiva20_4.Models.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Interaktiva20_4.Data
{
    public class CmdbRepository : ICmdbRepository
    {
        private string baseUrl;
        public CmdbRepository(IConfiguration configuration)
        {
            baseUrl = configuration.GetValue<string>("CmdbApi:BaseUrl");
        }
        public async Task<IEnumerable<MovieDTO>> GetMovies()
        {
            //TODO: Fixa så att koden inte upprepas
            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"{baseUrl}countries";
                var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<MovieDTO>>(data);
                return result;
            }
        }
    }
}
