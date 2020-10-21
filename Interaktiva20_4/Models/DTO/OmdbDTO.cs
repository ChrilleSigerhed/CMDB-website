using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interaktiva20_4.Models.DTO
{
    public class OmdbDTO
    {
        public List<OmdbDTO> Search { get; set; }
        public string Title { get; set; }
    }
}
