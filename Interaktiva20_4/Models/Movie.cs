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
        public string img { get; set; }
        public string title { get; set; }
        public string plot { get; set; }
        public string year { get; set; }
        public string actor { get; set; }
    }
}
