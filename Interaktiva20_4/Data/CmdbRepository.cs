using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web.Providers.Entities;
using Interaktiva20_4.Infrastructure;
using Interaktiva20_4.Models;
using Interaktiva20_4.Models.DTO;
using Interaktiva20_4.Models.ViewModel;
using Microsoft.Extensions.Configuration;

namespace Interaktiva20_4.Data
{
    public class CmdbRepository : ICmdbRepository
    {
        private string baseUrlCmdb;
        private string baseUrlOmdb;
        private string ApiKey = "4c824471";
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
            var tasks = new List<Task<MovieInfoDTO>>();
            List<MovieInfoDTO> movieInfoList = new List<MovieInfoDTO>();
            for (int i = 0; i < resultCmdb.Count(); i++)
            {
                var APIString = $"/?apikey={ApiKey}&i=" + resultCmdb.ElementAt(i).imdbId + "&plot=full";
                var task = apiClient.GetAsync<MovieInfoDTO>(baseUrlOmdb + APIString);
                tasks.Add(task);
            }
            
            await Task.WhenAll(tasks);
            for (int i = 0; i < tasks.Count; i++)
            {
                movieInfoList.Add(tasks[i].Result);
            }
            return movieInfoList;
        }

        public async Task<HomeViewModel> PresentIndex()
        {
            var tasks = new List<Task>();
            
            var resultCmdb = GetMoviesCmdb();
            var resultOmdb = GetMatchingMovies(resultCmdb.Result);

            tasks.Add(resultOmdb);
            tasks.Add(resultCmdb);
            await Task.WhenAll(tasks);

            return new HomeViewModel(resultCmdb.Result, resultOmdb.Result);
        }

        public async Task<HomeViewModel> PresentIndex(string search, List<Movie> savedList)
        {
            var tasks = new List<Task>();
            //var resultCmdb =  GetMoviesCmdb();
            var resultOmdb =  GetMoviesBySearch(search);

            tasks.Add(resultOmdb);
            //tasks.Add(resultCmdb);
            await Task.WhenAll(tasks);

            return new HomeViewModel(resultOmdb.Result, savedList);
        }

        public async Task<SearchDTO> GetMoviesBySearch(string search)
        {
            var APIString = $"/?apikey={ApiKey}&s=" + search;
            return await apiClient.GetAsync<SearchDTO>(baseUrlOmdb + APIString); 
        }
    }
}
