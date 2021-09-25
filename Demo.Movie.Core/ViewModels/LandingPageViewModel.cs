
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Movie.Core.Interfaces;
using Demo.Movie.Core.Model;
using Demo.Movie.Core.ViewModels.MVVM;

namespace Demo.Movie.Core.ViewModels
{
    public class LandingPageViewModel : BaseViewModel
    {
        private readonly IMovieService _movieService;

        private List<Film> _popularFilms;
        private List<Genre> _genres;
        private List<Genre> _filmsByGenre;

        public List<Genre> FilmsByGenre
        {
            get => _filmsByGenre;
            set => RaiseAndUpdate(ref _filmsByGenre, value);
        }

        // Commands

        public Command GetFilmsCommand { get; set; }

        /// <summary>
        /// Landing Page Constructor
        /// </summary>
        /// <param name="movieService"></param>
        public LandingPageViewModel(IMovieService movieService)
        {
            _movieService = movieService;

            _genres = new List<Genre>();
            _popularFilms = new List<Film>();


            GetFilmsCommand = new Command(execute: async () => await GetFilms(),
                                          canExecute: () =>
                                          {
                                              return !_isClicked && !_isRefreshing;
                                          });
;        }

        public override async Task InitAsync()
        {
            GetFilmsCommand.Execute();

            await base.InitAsync();
        }

        private async Task GetFilms()
        {
            _isClicked = true;

            GenreResponse genreResponse = await _movieService.GetAvailableGenres();

            PopularFilmsResponse filmResponse = await _movieService.GetMostRecentPopularFilms();

            _genres = genreResponse.genres.ToList();
            _popularFilms = filmResponse.results.ToList();

            List<Genre> filmsByGenre = new List<Genre>
            {
                new Genre
                {
                    id = 0,
                    name = "Popular",
                    associated_films = _popularFilms.OrderBy(film => film.release_date)
                                                    .ToList()
                }
            };

            List<Genre> filmsByGenreQuery = _genres.Select(genre =>
            {
                List<Film> films = _popularFilms.Select(film => film)
                                                .Where(film => film.genre_ids.Contains(genre.id))
                                                .OrderBy(film => film.title)
                                                .ToList();
                genre.associated_films = films;

                return genre;

            })
            .Where(genre => genre.associated_films.Any())
            .ToList();

            filmsByGenre.AddRange(filmsByGenreQuery);

            FilmsByGenre = filmsByGenre;

            _isClicked = false;
        }
    }
}
