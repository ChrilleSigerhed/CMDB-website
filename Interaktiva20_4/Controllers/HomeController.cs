using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Interaktiva20_4.Models;
using Interaktiva20_4.Data;
using Interaktiva20_4.Models.ViewModel;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Interaktiva20_4.Models.DTO;
using Microsoft.AspNetCore.Cors;
using System.Security;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Interaktiva20_4.Controllers
{
    public class HomeController : Controller
    {
        public HomeViewModel viewModel;
        private ICmdbRepository cmdbRepository;
        public HomeController(ICmdbRepository cmdbRepository)
        {
            this.cmdbRepository = cmdbRepository;
        }
        [Route("")]
        public async Task<IActionResult> Index()
        {
            List<Movie> newCmdbMovies = new List<Movie>();
            List<MovieDTO> cmdbList = cmdbRepository.GetMoviesCmdb().Result.ToList();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("MovieList")))
            {
                viewModel = await cmdbRepository.PresentIndex();
                HttpContext.Session.SetString("MovieList", JsonConvert.SerializeObject(viewModel.MovieList));
            }
            else if (JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList")).Count != cmdbList.Count())
            {
                
                List<Movie> sessionList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList"));
                List<MovieDTO> newList = new List<MovieDTO>();
                for (int i = 0; i < cmdbList.Count; i++)
                {
                    newList.Add(cmdbList[i]);
                }
                for (int i = 0; i < cmdbList.Count; i++)
                {
                    if (sessionList.Exists(x => x.imdbId == cmdbList[i].imdbId))
                    {
                        newList.Remove(cmdbList[i]);
                    }
                }
                
                for (int i = 0; i < newList.Count; i++)
                {
                    newCmdbMovies.Add(await cmdbRepository.GetMoviesByID(newList[i].imdbId));
                }
                viewModel = new HomeViewModel(JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList")));
            }
            else
            {
                viewModel = new HomeViewModel(JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList")));
            }
            viewModel.SavedList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList"));
            if (newCmdbMovies != null)
            {
                for (int i = 0; i < newCmdbMovies.Count; i++)
                {
                    viewModel.SavedList.Add(newCmdbMovies[i]);
                }
            }
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { SearchWord = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
