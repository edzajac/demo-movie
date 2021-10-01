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

        protected override string CurrentPage => "Film Modal";

        // Actions

        public Action PopFilmModalAction { get; set; }

        // Commands

        public Command PopFilmModalCommand { get; set; }
        public Command ViewTrailerCommand { get; set; }

        public FilmModalViewModel(IMovieService movieService)
        {
            _movieService = movieService;

            PopFilmModalCommand = new Command(execute: PopFilmModal,
                                              canExecute: () =>
                                              {
                                                  return !_isClicked;
                                              });

            ViewTrailerCommand = new Command(execute: async id => await ViewTrailerByFilmId((int)id),
                                             canExecute: id =>
                                             {
                                                 return !_isClicked;
                                             });
        }

        /// <summary>
        /// Triggers the PopFilmModal action to pop the current modal
        /// </summary>
        private void PopFilmModal()
        {
            _isClicked = true;

            PopFilmModalAction?.Invoke();

            _isClicked = false;
        }

        /// <summary>
        /// TODO:
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
