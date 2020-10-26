using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Interaktiva20_4.Infrastructure;
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
        IApiClient apiClient;
        public CmdbRepository(IConfiguration configuration, IApiClient apiClient)
        {
            baseUrlCmdb = configuration.GetValue<string>("CmdbApi:BaseUrl");
            baseUrlOmdb = configuration.GetValue<string>("OmdbApi:BaseUrl");
            this.apiClient = apiClient;
        }
        public async Task<IEnumerable<MovieDTO>> GetMoviesCmdb()
        {
            return await apiClient.GetAsync<IEnumerable<MovieDTO>>(baseUrlCmdb + "/movie");
        }
        public async Task<IEnumerable<MovieInfoDTO>> GetMatchingMovies(IEnumerable<MovieDTO> resultCmdb)
        {
            List<MovieInfoDTO> movieInfoList = new List<MovieInfoDTO>();
            for (int i = 0; i < resultCmdb.Count(); i++)
            {
                var APIString = "/?apikey=547db346&i=" + resultCmdb.ElementAt(i).imdbId;
                movieInfoList.Add(await apiClient.GetAsync<MovieInfoDTO>(baseUrlOmdb + APIString));
            }
            return movieInfoList;
        }
        public async Task<HomeViewModel> PresentIndex()
        {
            var tasks = new List<Task>();
            var resultCmdb =  await GetMoviesCmdb();
            var resultOmdb = GetMatchingMovies(resultCmdb);

            tasks.Add(resultOmdb);
            await Task.WhenAll(tasks);

            return new HomeViewModel(resultCmdb, resultOmdb.Result);
        }
    }
}
