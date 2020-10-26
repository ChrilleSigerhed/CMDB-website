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

namespace Interaktiva20_4.Controllers
{
    public class HomeController : Controller
    {
        private ICmdbRepository cmdbRepository;

        public HomeController(ICmdbRepository cmdbRepository)
        {
            this.cmdbRepository = cmdbRepository;
        }
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var viewModel = await cmdbRepository.PresentIndex();
            ViewData["movies"] = viewModel;
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
