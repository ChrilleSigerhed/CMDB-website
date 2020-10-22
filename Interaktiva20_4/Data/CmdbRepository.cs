using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Interaktiva20_4.Models.DTO;
using Interaktiva20_4.Models.ViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Interaktiva20_4.Data
{
    public class CmdbRepository : ICmdbRepository
    {
        private string baseUrlCmdb;
        private string baseUrlOmdb;

        public CmdbRepository(IConfiguration configuration)
        {
            baseUrlCmdb = configuration.GetValue<string>("CmdbApi:BaseUrl");
            baseUrlOmdb = configuration.GetValue<string>("OmdbApi:BaseUrl");

        }
        public async Task<IEnumerable<MovieDTO>> GetMoviesCmdb()
        {
            //TODO: Fixa så att koden inte upprepas
            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"{baseUrlCmdb}/movie";
                var response = await client.GetAsync($"{baseUrlCmdb}/movie", HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var resultCmdb = JsonConvert.DeserializeObject<IEnumerable<MovieDTO>>(data);
                return resultCmdb;
            }
        }
        public async Task<IEnumerable<MovieInfoDTO>> GetMatchingMovies(IEnumerable<MovieDTO> resultCmdb)
        {
            List<MovieInfoDTO> movieInfoList = new List<MovieInfoDTO>();
            //TODO: Fixa så att koden inte upprepas
            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < resultCmdb.Count(); i++)
                {
                    var APIString = "?apikey=547db346&i=" + resultCmdb.ElementAt(i).imdbId;
                    var endpoint = $"{baseUrlOmdb}/{APIString}";
                    var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();
                    var data = await response.Content.ReadAsStringAsync();
                    var resultOmdb = JsonConvert.DeserializeObject<MovieInfoDTO>(data);
                    movieInfoList.Add(resultOmdb);
                }
                return movieInfoList;
            }
        }
        public async Task<HomeViewModel> PresentIndex()
        {
            var resultCmdb = await GetMoviesCmdb();
            var resultOmdb = await GetMatchingMovies(resultCmdb);
            return new HomeViewModel(resultCmdb, resultOmdb);
        }
    }
}
