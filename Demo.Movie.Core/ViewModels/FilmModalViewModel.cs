using System;
using System.Linq;
using System.Threading.Tasks;
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

        // Commands

        public Command ViewTrailerCommand { get; set; }

        public FilmModalViewModel(IMovieService movieService)
        {
            _movieService = movieService;

            ViewTrailerCommand = new Command(execute: async id => await ViewTrailerByFilmId((int)id),
                                             canExecute: id =>
                                             {
                                                 return !_isClicked;
                                             });
        }

        private async Task ViewTrailerByFilmId(int id)
        {
            _isClicked = true;

            FilmTrailerResponse response = await _movieService.GetTrailerInformationByFilmId(id);

            if(response != null && response.results.Any())
            {
                FilmTrailer trailer = response.results.First();

                //TODO: Finish trailer logic
            }

            _isClicked = false;
        }
    }
}
