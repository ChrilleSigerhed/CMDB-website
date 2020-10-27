using Interaktiva20_4.Data;
using Microsoft.AspNetCore.Mvc;
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
        [Route("/about")]
        public async Task<IActionResult> Index()
        {
            //var omdbbModel = await omdbRepository.SearchForMoviesOnOmdbApi();
            var viewModel = await cmdbRepository.PresentIndex();
            return View(viewModel);
        }
    }
}
