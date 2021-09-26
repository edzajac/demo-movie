
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Movie.Core.Helpers;
using Demo.Movie.Core.Interfaces;
using Demo.Movie.Core.Model;
using Demo.Movie.Core.ViewModels.MVVM;
using Xamarin.Essentials;

namespace Demo.Movie.Core.ViewModels
{
    public class LandingPageViewModel : BaseViewModel
    {
        private readonly IMovieService _movieService;

        private ImageConfiguration _currentConfig;

        private List<Film> _popularFilms;
        private List<Genre> _genres;
        private List<Genre> _filmsByGenre;

        public List<Genre> FilmsByGenre
        {
            get => _filmsByGenre;
            set => RaiseAndUpdate(ref _filmsByGenre, value);
        }

        // Actions

        public Action<Film> NavigateToFilmModalAction { get; set; } 

        // Commands

        public Command GetFilmsCommand { get; set; }
        public Command NavigateToFilmModalCommand { get; set; }

        /// <summary>
        /// Landing Page Constructor
        /// </summary>
        /// <param name="movieService"></param>
        public LandingPageViewModel(IMovieService movieService)
        {
            _movieService = movieService;

            _currentConfig = new ImageConfiguration();
            _genres = new List<Genre>();
            _popularFilms = new List<Film>();


            GetFilmsCommand = new Command(execute: async () => await GetFilms(),
                                          canExecute: () =>
                                          {
                                              return !_isClicked && !_isLoading;
                                          });

            NavigateToFilmModalCommand = new Command(execute: film => NavigateToFilmModal((Film)film),
                                                     canExecute: film =>
                                                     {
                                                         return !_isClicked && !_isLoading;
                                                     });
        }

        public override async Task InitAsync()
        {
            GetFilmsCommand.Execute();

            await base.InitAsync();
        }

        #region Command Methods

        /// <summary>
        /// Triggers the NavigateToFilmModalAction with the film param
        /// </summary>
        /// <param name="film"></param>
        private void NavigateToFilmModal(Film film)
        {
            _isClicked = true;

            NavigateToFilmModalAction?.Invoke(film);

            _isClicked = false;
        }

        /// <summary>
        /// Displays the film and genre data received
        /// </summary>
        /// <returns></returns>
        private async Task GetFilms()
        {
            IsLoading = true;

            bool isDataAvailable = await GetFilmAndGenreData();

            IsDataAvailable = isDataAvailable;

            //TODO: Check with designer about offline state
            // with no data stored.

            if (isDataAvailable)
            {
                // TODO: Check with PO and designer about lists containing 
                // two or more films

                List<string> posterSizes = _currentConfig.poster_sizes.ToList();

                string posterSize = posterSizes.Any() && posterSizes.Count >= 2 ? posterSizes[1] : "w154";

                _popularFilms.ForEach(film =>
                {
                    film.poster_url = $"{_currentConfig.base_url}{posterSize}{film.poster_path}";
                });

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
                                                    .Distinct()
                                                    .OrderBy(film => film.title)
                                                    .ToList();
                    genre.associated_films = films;

                    return genre;

                })
                .Where(genre => genre.associated_films.Any() && genre.associated_films.Count >= 3)
                .ToList();

                filmsByGenre.AddRange(filmsByGenreQuery);

                FilmsByGenre = filmsByGenre;
            }

            IsLoading = false;
        }

        #endregion

        /// <summary>
        /// Gets the data regarding genres, the most recent popular films,
        /// and the image configuration to get image files. If no network connection
        /// is available and/or an issue occurs with the calls, the last saved data set
        /// will be used.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> GetFilmAndGenreData()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                ImageConfigurationResponse configResponse = await _movieService.GetImageConfiguration();

                GenreResponse genreResponse = await _movieService.GetAvailableGenres();

                PopularFilmsResponse filmResponse = await _movieService.GetMostRecentPopularFilms();

                if (configResponse != null)
                {
                    AkavacheCache.Set<ImageConfiguration>(AkavacheCache.Key.ImageConfig, configResponse.images);
                    _currentConfig = configResponse.images;
                }
                else
                {
                    var imageConfig = await AkavacheCache.Get<ImageConfiguration>(AkavacheCache.Key.ImageConfig);
                    _currentConfig = imageConfig;
                }

                if (genreResponse != null)
                {
                    AkavacheCache.Set<IEnumerable<Genre>>(AkavacheCache.Key.Genres, genreResponse.genres);
                    _genres = genreResponse.genres.ToList();

                }
                else
                {
                    var genres = await AkavacheCache.Get<IEnumerable<Genre>>(AkavacheCache.Key.Genres);
                    _genres = genres.ToList();
                }

                if (filmResponse != null)
                {
                    AkavacheCache.Set<IEnumerable<Film>>(AkavacheCache.Key.PopularFilms, filmResponse.results);
                    _popularFilms = filmResponse.results.ToList();
                }
                else
                {
                    var films = await AkavacheCache.Get<IEnumerable<Film>>(AkavacheCache.Key.PopularFilms);
                    _popularFilms = films.ToList();
                }

            }
            else
            {
                var imageConfig = await AkavacheCache.Get<ImageConfiguration>(AkavacheCache.Key.ImageConfig);
                var genres = await AkavacheCache.Get<IEnumerable<Genre>>(AkavacheCache.Key.Genres);
                var films = await AkavacheCache.Get<IEnumerable<Film>>(AkavacheCache.Key.PopularFilms);

                _currentConfig = imageConfig;
                _genres = genres.ToList();
                _popularFilms = films.ToList();
            }

            return _currentConfig != null && _genres != null
                && _genres.Any() && _popularFilms != null
                && _popularFilms.Any();

        }
    }
}

