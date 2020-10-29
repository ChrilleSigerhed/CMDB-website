using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interaktiva20_4.Models.DTO
{
    public class SearchDTO
    {
        public IEnumerable<MovieInfoDTO> Search { get; set; }
    }
}
