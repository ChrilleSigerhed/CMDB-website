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
        #region notAcceptedChars
        char[] notAcceptedChars = new char[] { ':', ';', '<', '>', '=', '?', '!', 
            '@', '"', '#', '$', '%', '&', '(', ')', '*', '+', ',', '-', '.', '/', 
            '[', ']', '^', '_', '`', '´', '{', '}', '~', ' '};
        #endregion
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
            ID = FixSearchString(ID);
            List<MovieDTO> cmdbList = cmdbRepository.GetMoviesCmdb().Result.ToList();
            var savedList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList"));
            var viewModel = await cmdbRepository.PresentIndexID(ID, savedList);
            viewModel = listHandler.UpdateChangesAbout(cmdbList, viewModel);
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
                if (ID[i] == ' ' && ID[i+1] == ' ')
                {
                    ID = ID.Remove(i, 1);
                }
                
            }
            if (ID.Length >=22)
            {
                ID = ID.Remove(22).ToLower();
            }
            return ID;
        }
    }
}
