using Interaktiva20_4.Data;
using Interaktiva20_4.Models;
using Interaktiva20_4.Models.DTO;
using Interaktiva20_4.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interaktiva20_4.Infrastructure
{
    public class ListHandler
    {
        private ICmdbRepository cmdbRepository;
        HomeViewModel viewModel;
        public List<MovieDTO> cmdbList { get; set; }
        public List<Movie> movieList { get; set; }
        public ListHandler(ICmdbRepository cmdbRepository)
        {
            this.cmdbRepository = cmdbRepository;
        }
        public bool CheckForNewMovies(List<MovieDTO> cmdbList, List<Movie> sessionList)
        {
            if (sessionList.Count != cmdbList.Count())
            {
                return true;
            }
            return false;
        }

        public HomeViewModel UpdateChangesHome(List<Movie> newCmdbMovies, List<MovieDTO> cmdbList, HomeViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.cmdbList = cmdbList;
            viewModel = UpdateLikesHome();
            viewModel = UpdateSavedList(newCmdbMovies);
            return viewModel;
        }
       
        public HomeViewModel UpdateChangesSearch(List<MovieDTO> cmdbList, HomeViewModel viewModel)
        {
            for (int i = 0; i < cmdbList.Count; i++)
            {
                for (int j = 0; j < viewModel.MovieList.Count; j++)
                {
                    if (cmdbList[i].imdbId == viewModel.MovieList[j].imdbId &&
                        (cmdbList[i].numberOfDislikes != viewModel.MovieList[j].numberOfDislikes ||
                        cmdbList[i].numberOfLikes != viewModel.MovieList[j].numberOfLikes))
                    {
                        viewModel.MovieList[j].numberOfDislikes = cmdbList[i].numberOfDislikes;
                        viewModel.MovieList[j].numberOfLikes = cmdbList[i].numberOfLikes;
                    }
                }
            }
            return viewModel;
        }
        public HomeViewModel UpdateChangesAbout(List<MovieDTO> cmdbList, HomeViewModel viewModel)
        {
            for (int i = 0; i < cmdbList.Count; i++)
            {
                if (cmdbList[i].imdbId == viewModel.SelectedMovie.imdbId &&
                    (cmdbList[i].numberOfDislikes != viewModel.SelectedMovie.numberOfDislikes ||
                    cmdbList[i].numberOfLikes != viewModel.SelectedMovie.numberOfLikes))
                {
                    viewModel.SelectedMovie.numberOfDislikes = cmdbList[i].numberOfDislikes;
                    viewModel.SelectedMovie.numberOfLikes = cmdbList[i].numberOfLikes;
                }
            }
            return viewModel;
        }
        public HomeViewModel UpdateLikesHome()
        {
            for (int i = 0; i < cmdbList.Count; i++)
            {
                for (int j = 0; j < viewModel.SavedList.Count; j++)
                {
                    if (cmdbList[i].imdbId == viewModel.SavedList[j].imdbId &&
                        (cmdbList[i].numberOfDislikes != viewModel.SavedList[j].numberOfDislikes ||
                        cmdbList[i].numberOfLikes != viewModel.SavedList[j].numberOfLikes))
                    {
                        viewModel.SavedList[j].numberOfDislikes = cmdbList[i].numberOfDislikes;
                        viewModel.SavedList[j].numberOfLikes = cmdbList[i].numberOfLikes;
                    }
                }
            }
            return viewModel;
        }
       
        public HomeViewModel UpdateSavedList(List<Movie> newCmdbMovies)
        {
            if (newCmdbMovies != null)
            {
                for (int i = 0; i < newCmdbMovies.Count; i++)
                {
                    viewModel.SavedList.Add(newCmdbMovies[i]);
                }
            }
            return viewModel;
        }
        
        public async Task<List<Movie>> AddNewMovies(List<MovieDTO> cmdbList, List<Movie> sessionList)
        {
            List<Movie> newCmdbMovies = new List<Movie>();
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
            return newCmdbMovies;

        }
    }
}
