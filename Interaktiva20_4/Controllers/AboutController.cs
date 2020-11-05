using Interaktiva20_4.Data;
using Interaktiva20_4.Infrastructure;
using Interaktiva20_4.Models;
using Interaktiva20_4.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Interaktiva20_4.Controllers
{
    public class AboutController : Controller
    {
        private ICmdbRepository cmdbRepository;
        ListHandler listHandler;

        public AboutController(ICmdbRepository cmdbRepository)
        {
            this.cmdbRepository = cmdbRepository;
            this.listHandler = new ListHandler(cmdbRepository);
        }

        [Route("/About")]
        public async Task<IActionResult> Index(string ID)
        {
            List<MovieDTO> cmdbList = cmdbRepository.GetMoviesCmdb().Result.ToList();
            var savedList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList"));
            var viewModel = await cmdbRepository.PresentIndexID(ID, savedList);
            viewModel = listHandler.UpdateChangesAbout(cmdbList, viewModel);
            return View(viewModel);
        }
    }
}
