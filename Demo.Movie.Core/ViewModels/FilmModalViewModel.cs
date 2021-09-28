using System;
using Demo.Movie.Core.Interfaces;
using Demo.Movie.Core.Model;
using Demo.Movie.Core.ViewModels.MVVM;

namespace Demo.Movie.Core.ViewModels
{
    public class FilmModalViewModel : BaseViewModel
    {
        private readonly IMovieService _movieService;

        private Film _chosenFilm;

        public Film ChosenFilm
        {
            get => _chosenFilm;
            set => RaiseAndUpdate(ref _chosenFilm, value);
        }

        public FilmModalViewModel(IMovieService movieService)
        {
            _movieService = movieService;
        }
    }
}
