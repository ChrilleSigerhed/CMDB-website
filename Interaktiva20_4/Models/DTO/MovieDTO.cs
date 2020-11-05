using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interaktiva20_4.Models.DTO
{
    public class MovieDTO
    {
        public string imdbId { get; set; }
        public int numberOfLikes { get; set; }
        public int numberOfDislikes { get; set; }
    }
}
