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
        public List<Movie> filmer { get; set; }
        public HomeViewModel(IEnumerable<MovieDTO> movies, IEnumerable<MovieInfoDTO> matching)
        {

            filmer = movies
                .Select(x => new Movie
                {
                    imdbId = x.imdbId,
                    numberOfDislikes = x.numberOfDislikes,
                    numberOfLikes = x.numberOfLikes
                })
                .OrderBy(x => x.numberOfDislikes - x.numberOfLikes)
                .ToList();

            for (int i = 0; i < filmer.Count; i++)
            {
                for (int j = 0; j < filmer.Count; j++)
                { 
                    if (filmer[i].imdbId == matching.ElementAt(j).imdbID)
                    {
                        filmer[i].title = matching.ElementAt(j).Title;
                        filmer[i].plot = matching.ElementAt(j).Plot;
                        filmer[i].img = matching.ElementAt(j).Poster;
                    }
                }
            }

        }
    }
}
