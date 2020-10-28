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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("MovieList")))
            {
                viewModel = await cmdbRepository.PresentIndex();
                HttpContext.Session.SetString("MovieList", JsonConvert.SerializeObject(viewModel.MovieList));
            }
            else
            {
                viewModel = new HomeViewModel(JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList")));
            }
            viewModel.SavedList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList"));
            return View(viewModel);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
