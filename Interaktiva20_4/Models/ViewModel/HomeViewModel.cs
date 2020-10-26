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
        public List<Movie> MovieList { get; set; }
        public HomeViewModel(IEnumerable<MovieDTO> movies, IEnumerable<MovieInfoDTO> matching)
        {

            MovieList = movies
                .Select(x => new Movie
                {
                    imdbId = x.imdbId,
                    numberOfDislikes = x.numberOfDislikes,
                    numberOfLikes = x.numberOfLikes
                })
                .OrderBy(x => x.numberOfDislikes - x.numberOfLikes)
                .ToList();

            for (int i = 0; i < MovieList.Count; i++)
            {
                for (int j = 0; j < MovieList.Count; j++)
                { 
                    if (MovieList[i].imdbId == matching.ElementAt(j).imdbID)
                    {
                        MovieList[i].title = matching.ElementAt(j).Title;
                        MovieList[i].plot = matching.ElementAt(j).Plot;
                        MovieList[i].img = matching.ElementAt(j).Poster;
                    }
                }
            }

        }
    }
}
