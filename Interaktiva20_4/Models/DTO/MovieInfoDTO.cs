using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interaktiva20_4.Models.DTO
{
    public class MovieInfoDTO
    {
        public string Title { get; set; }
        public string Poster { get; set; }
        public string imdbID { get; set; }
        public string Plot { get; set; }
        public string Actors { get; set; }
        public string Year { get; set; }
    }
}
