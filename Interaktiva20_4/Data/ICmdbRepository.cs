using Interaktiva20_4.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interaktiva20_4.Data
{
    public interface ICmdbRepository
    {
        Task <IEnumerable<MovieDTO>> GetMovies();
       
    }
}
