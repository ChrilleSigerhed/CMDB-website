using Interaktiva20_4.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interaktiva20_4.Models.ViewModel
{
    public class HomeViewModel
    {
        public List<Movie> movies { get; set; }
        public HomeViewModel(IEnumerable<MovieDTO> movies, IEnumerable<MovieInfoDTO> matching)
        {
            this.movies = movies
                .Select(x => new Movie
                {
                    imdbId = x.imdbId,
                    numberOfDislikes = x.numberOfDislikes,
                    numberOfLikes = x.numberOfLikes,
                    img = matching
                    .Select(x => x.Poster).ToString(),
                    Title = matching
                    .Select(x => x.Title).ToString(),
                    plot = matching
                    .Select(x => x.Poster).ToString()
                })
                .OrderBy(x => x.numberOfDislikes - x.numberOfLikes)
                .ToList();
                
        }
    }
}
