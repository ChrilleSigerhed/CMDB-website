using Interaktiva20_4.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interaktiva20_4.Models.ViewModel
{
    public class SearchViewModel
    {
        public List<Movie> MovieList { get; set; }
        public string Title { get; set; }
        public string ImdbId { get; set; }
        public SearchViewModel(IEnumerable<MovieDTO> movies, IEnumerable<MovieInfoDTO> searchResult)
        {
            
            for (int i = 0; i < searchResult.Count(); i++)
            {
                MovieList.Add(new Movie
                {
                    imdbId = searchResult.ElementAt(i).imdbID,
                    title = searchResult.ElementAt(i).Title,
                    plot = searchResult.ElementAt(i).Plot,
                    img = searchResult.ElementAt(i).Poster
                });
            }
            for (int i = 0; i < MovieList.Count; i++)
            {
                for (int j = 0; j < movies.Count(); j++)
                {
                    if (MovieList[i].imdbId == movies.ElementAt(j).imdbId)
                    {
                        MovieList[i].numberOfLikes = movies.ElementAt(j).numberOfLikes;
                        MovieList[i].numberOfDislikes = movies.ElementAt(j).numberOfDislikes;
                    }
                }
            }
            MovieList.OrderBy(x => x.numberOfDislikes - x.numberOfLikes).ToList();

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

