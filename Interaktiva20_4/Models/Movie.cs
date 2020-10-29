using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interaktiva20_4.Models
{
    public class Movie
    {
        public string imdbId { get; set; }
        public int numberOfLikes { get; set; }
        public int numberOfDislikes { get; set; }
        public string Poster { get; set; }
        public string Title { get; set; }
        public string Plot { get; set; }
        public string Year { get; set; }
        public string Actors { get; set; }
        public List<RatingsDTO> ratings { get; set; }
    }
}
