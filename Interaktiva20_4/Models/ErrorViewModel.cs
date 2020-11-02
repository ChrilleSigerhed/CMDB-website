using System;
using System.Collections.Generic;

namespace Interaktiva20_4.Models
{
    public class ErrorViewModel
    {
        public string SearchWord { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(SearchWord);

        public List<Movie> SavedList { get; set; }
    }
}
