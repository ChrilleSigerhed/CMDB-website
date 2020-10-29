using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interaktiva20_4.Data;
using Interaktiva20_4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Interaktiva20_4.Controllers
{
    public class SearchController : Controller
    {
        #region notAcceptedChars
        char[] notAcceptedChars = new char[] { ':', ';', '<', '>', '=', '?', '!',
            '@', '"', '#', '$', '%', '&', '(', ')', '*', '+', ',', '-', '.', '/',
            '[', ']', '^', '_', '`', '´', '{', '}', '~', ' '};
        #endregion
        private ICmdbRepository cmdbRepository;

        public SearchController(ICmdbRepository cmdbRepository)
        {
            this.cmdbRepository = cmdbRepository;
        }
        [Route("/Search")]
        public async Task<IActionResult> Index(string ID)
        {
            ID = FixSearchString(ID);
            var savedList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList"));
            var viewModel = await cmdbRepository.PresentIndex(ID, savedList);
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("MovieList")))
            {
                HttpContext.Session.SetString("MovieList", JsonConvert.SerializeObject(viewModel.MovieList));
            }
            viewModel.SavedList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList"));
            return View(viewModel);
        }
        public string FixSearchString(string ID)
        {

            //star wars episode iv a 22tecken 
            for (int i = 0; i < notAcceptedChars.Length; i++)
            {
                if (ID.Contains(notAcceptedChars[i]))
                {
                    ID = ID.Replace(notAcceptedChars[i], ' ');
                }
            }
            for (int i = 0; i < ID.Length; i++)
            {
                if (ID[i] == ' ' && ID[i + 1] == ' ')
                {
                    ID = ID.Remove(i, 1);
                }

            }
            if (ID.Length >= 22)
            {
                ID = ID.Remove(22).ToLower();
            }
            return ID;
        }
    }
}

