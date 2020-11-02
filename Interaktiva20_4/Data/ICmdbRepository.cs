using Interaktiva20_4.Models;
using Interaktiva20_4.Models.DTO;
using Interaktiva20_4.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interaktiva20_4.Data
{
    public interface ICmdbRepository
    {
        Task<IEnumerable<MovieDTO>> GetMoviesCmdb();

        Task<IEnumerable<MovieInfoDTO>> GetMatchingMovies(IEnumerable<MovieDTO> c);

        Task<HomeViewModel> PresentIndex();
        
        Task<HomeViewModel> PresentIndex(string search, List<Movie> savedList);

        Task<SearchDTO> GetMoviesBySearch(string search);
        Task<HomeViewModel> PresentIndexID(string iD, List<Movie> savedList);
        Task<Movie> GetMoviesByID(string ID);
    }
}
