using Interaktiva20_4.Data;
using Interaktiva20_4.Models;
using Interaktiva20_4.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interaktiva20_4.Controllers
{
    public class AboutController : Controller
    {
        
        private ICmdbRepository cmdbRepository;

        public AboutController(ICmdbRepository cmdbRepository)
        {
            this.cmdbRepository = cmdbRepository;
        }
        [Route("/About")]
        public async Task<IActionResult> Index(string ID)
        {
            var viewModel = await cmdbRepository.PresentIndex(ID);
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("MovieList")))
            {
                HttpContext.Session.SetString("MovieList", JsonConvert.SerializeObject(viewModel.MovieList));
            }
            viewModel.SavedList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList"));
            return View(viewModel);
        }
    }
}
