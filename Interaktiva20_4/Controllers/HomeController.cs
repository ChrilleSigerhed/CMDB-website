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
using Interaktiva20_4.Infrastructure;

namespace Interaktiva20_4.Controllers
{
    public class HomeController : Controller
    {
        public HomeViewModel viewModel;
        private ICmdbRepository cmdbRepository;
        ListHandler listHandler;
        List<Movie> sessionList = new List<Movie>();
        public HomeController(ICmdbRepository cmdbRepository)
        {
            this.cmdbRepository = cmdbRepository;
            this.listHandler = new ListHandler(cmdbRepository);
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
            else if (!string.IsNullOrEmpty(HttpContext.Session.GetString("MovieList")))
            {
                sessionList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList"));
                if (listHandler.CheckForNewMovies(cmdbList, sessionList))
                {
                    newCmdbMovies = await listHandler.AddNewMovies(cmdbList, sessionList);
                }
            }
            viewModel = new HomeViewModel(JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList")));
            viewModel.SavedList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList"));
            viewModel = listHandler.UpdateChangesHome(newCmdbMovies, cmdbList, viewModel);
            HttpContext.Session.SetString("MovieList", JsonConvert.SerializeObject(viewModel.SavedList));
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { SearchWord = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
