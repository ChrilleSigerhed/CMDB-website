using Interaktiva20_4.Data;
using Interaktiva20_4.Infrastructure;
using Interaktiva20_4.Models;
using Interaktiva20_4.Models.DTO;
using Interaktiva20_4.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Interaktiva20_4.Test
{
    public class CmdbMockRepository : ICmdbRepository
    {
        private string baseUrl;
        
        public CmdbMockRepository(IWebHostEnvironment webHostEnvironment)
        {
            baseUrl = $"{webHostEnvironment.ContentRootPath}\\Test\\Mockdata\\";
            
        }
        public async Task<IEnumerable<MovieInfoDTO>> GetMatchingMovies(IEnumerable<MovieDTO> c)
        {
            await Task.Delay(0);
            return FileHandler.GetTestData<IEnumerable<MovieInfoDTO>>($"{baseUrl}OmdbMock.json");
        }


        public async Task<IEnumerable<MovieDTO>> GetMoviesCmdb()
        {
            await Task.Delay(0);
            return FileHandler.GetTestData<IEnumerable<MovieDTO>>($"{baseUrl}Movie.json");
        }

        public async Task<HomeViewModel> PresentIndex()
        {

            var tasks = new List<Task>();
            var resultCmdb = await GetMoviesCmdb();
            var resultOmdb = GetMatchingMovies(resultCmdb);

            tasks.Add(resultOmdb);
            await Task.WhenAll(tasks);

            return new HomeViewModel(resultCmdb, resultOmdb.Result);
        }

        public Task<HomeViewModel> PresentIndex(string search, List<Movie> savedList)
        {
            throw new NotImplementedException();
        }
        public Task<SearchDTO> GetMoviesBySearch(string search)
        {
            throw new NotImplementedException();
        }

        public Task<HomeViewModel> PresentIndex(string iD)
        {
            throw new NotImplementedException();
        }

        public Task<HomeViewModel> PresentIndexID(string iD, List<Movie> savedList)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMoviesByID(string ID)
        {
            throw new NotImplementedException();
        }
    }
}
