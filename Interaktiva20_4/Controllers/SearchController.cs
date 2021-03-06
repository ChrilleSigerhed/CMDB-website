﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Interaktiva20_4.Data;
using Interaktiva20_4.Infrastructure;
using Interaktiva20_4.Models;
using Interaktiva20_4.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
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
        ListHandler listHandler;

        public SearchController(ICmdbRepository cmdbRepository)
        {
            this.cmdbRepository = cmdbRepository;
            this.listHandler = new ListHandler(cmdbRepository);
        }
        [Route("/Search")]
        public async Task<IActionResult> Index(string ID)
        {
            ID = FixSearchString(ID);
           
            var savedList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList"));
            try
            {
                List<MovieDTO> cmdbList = cmdbRepository.GetMoviesCmdb().Result.ToList();
                var viewModel = await cmdbRepository.PresentIndex(ID, savedList);
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("MovieList")))
                {
                    HttpContext.Session.SetString("MovieList", JsonConvert.SerializeObject(viewModel.MovieList));
                }

                viewModel.SavedList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList"));
                viewModel = listHandler.UpdateChangesSearch(cmdbList, viewModel);
                return View(viewModel);
            }
            catch(Exception ex)
            {
                ErrorViewModel viewModel = new ErrorViewModel();
                if (ex.HResult == -2146233088)
                {
                    viewModel.ErrorMessage = ex.InnerException.Message;
                }
                else
                {
                    viewModel.SavedList = JsonConvert.DeserializeObject<List<Movie>>(HttpContext.Session.GetString("MovieList"));
                    viewModel.SearchWord = ID;
                }

                return View("Error", viewModel);
            }
        }
        public string FixSearchString(string ID)
        {
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

