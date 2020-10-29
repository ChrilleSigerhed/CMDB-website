using Interaktiva20_4.Models.DTO;
using Interaktiva20_4.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interaktiva20_4.Controllers;
using Newtonsoft.Json;

namespace Interaktiva20_4.Models.ViewModel
{
    public class HomeViewModel
    {
        public List<Movie> SavedList { get; set; }
        public List<Movie> MovieList { get; set; }
        public string  Title { get; set; }
        public string ImdbId { get; set; }
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
                        MovieList[i].actor = matching.ElementAt(j).Actors;
                        MovieList[i].year = "(" + matching.ElementAt(j).Year + ")";
                        MovieList[i].ratings = matching.ElementAt(j).Ratings;
                    }
                }
            }
        }
        public HomeViewModel(List<Movie> savedList)
        {
            MovieList = savedList
                .OrderBy(x => x.numberOfDislikes - x.numberOfLikes)
                .ToList();
        }
        public HomeViewModel(SearchDTO searchResult, List<Movie> savedList)
        {
           
            MovieList = searchResult
                .Search.Select(x => new Movie
                {
                    imdbId = x.imdbID,
                    title = x.Title,
                    img = x.Poster,
                    actor = x.Actors,
                    plot = x.Plot,
                    year = x.Year,
                    ratings = x.Ratings
                }).ToList();
            for (int i = 0; i < MovieList.Count; i++)
            {
                for (int j = 0; j < savedList.Count; j++)
                {
                    if (MovieList[i].imdbId == savedList[j].imdbId)
                    {
                        MovieList[i].numberOfLikes = savedList[j].numberOfLikes;
                        MovieList[i].numberOfDislikes = savedList[j].numberOfDislikes;
                    }
                    else if(j == savedList.Count)
                    {
                        MovieList[i].numberOfLikes = 0;
                        MovieList[i].numberOfDislikes = 0;
                    }
                }
            }
            
        }

        public IEnumerable<SelectListItem> Movies
        {
            get
            {
                if (MovieList != null)
                {
                    return MovieList.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.title,
                        Value = x.imdbId
                    });
                }
                return null;
            }
        }
    }
}
