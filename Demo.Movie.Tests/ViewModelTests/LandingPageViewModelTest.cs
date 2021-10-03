using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Movie.Core.Model;
using Demo.Movie.Core.ViewModels;
using Xunit;

namespace Demo.Movie.Tests.ViewModelTests
{
    public class LandingPageViewModelTest : BaseViewModelTest
    {
        private readonly LandingPageViewModel _landingPageVM;

        /// <summary>
        /// Here is a quick example of Mocked Unit Tests
        /// leveraging the View Models of the Demo.Movie app
        /// </summary>
        public LandingPageViewModelTest() : base()
        {
            _landingPageVM = new LandingPageViewModel(MovieService);
        }

        [Fact]
        public void ValidatePassingTest()
        {
            Assert.Equal(4, 2+2);
        }

        [Fact]
        public async Task GetFilmsAndGenreData_ReturnData()
        {
            bool actual = false;

            actual = await _landingPageVM.GetFilmAndGenreData();

            Assert.True(actual);
        }

        [Fact]
        public async Task GetFilms_PopularAndAdventureGenre_ReturnThreeListsOfFilmsByGenre()
        {
            List<Genre> actual = new List<Genre>();

            int expectedGenreCount = 3;
            int expectedPopulareGenreId = 0;
            int expectedAdventureGenreId = 12;
            int expectedFamilyGenreId = 10751;

            actual = await _landingPageVM.GetFilms();

            Assert.NotEmpty(actual);

            Assert.Equal(expectedGenreCount, actual.Count);

            // Genre: Popular
            Assert.Equal(expectedPopulareGenreId, actual[0].id);

            // Genre: Adventure
            Assert.Equal(expectedAdventureGenreId, actual[1].id);

            // Genre: Family
            Assert.Equal(expectedFamilyGenreId, actual[2].id);
        }

        [Fact]
        public async Task GetFilms_FreeDude_ReturnSpecificFilmInPopularAndAdventureGenresOnly()
        {
            List<Genre> actual = new List<Genre>();

            Film expectedFilm = new Film()
            {
                id = 123456,
                adult = false,
                genre_ids = new int[] { 35, 28, 12 },
                title = "Free Dude",
                poster_path = "/free_dude_test.jpg",
                release_date = "2021-08-11",
                overview = "This is a test overview for Free Dude.",
                popularity = 5418,
                vote_count = 1234,
                vote_average = 7
            };

            int expectedPopularGenreId = 0;
            int expectedAdventureGenreId = 12;
            int expectedFamilyGenreId = 10751;


            actual = await _landingPageVM.GetFilms();

            Film actualInPopularGenre = actual[0].associated_films.Find(film => film.id == expectedFilm.id && film.title == expectedFilm.title);
            Film actualInAdventureGenre = actual[1].associated_films.Find(film => film.id == expectedFilm.id && film.title == expectedFilm.title);
            Film actualInFamilyGenre = actual[2].associated_films.Find(film => film.id == expectedFilm.id && film.title == expectedFilm.title);


            Assert.NotEmpty(actual);

            // Genre: Popular
            Assert.NotEmpty(actual[0].associated_films);
            Assert.Equal(expectedPopularGenreId, actual[0].id);
            Assert.NotNull(actualInPopularGenre);

            // Genre: Adventure
            Assert.NotEmpty(actual[1].associated_films);
            Assert.Equal(expectedAdventureGenreId, actual[1].id);
            Assert.NotNull(actualInPopularGenre);

            // Genre: Family
            Assert.NotEmpty(actual[2].associated_films);
            Assert.Equal(expectedFamilyGenreId, actual[2].id);
            Assert.Null(actualInFamilyGenre);

        }

    }
}
