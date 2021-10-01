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
        public async Task GetFilms_()
        {
            List<Genre> actual = new List<Genre>();

            int expectedGenreCount = 1;

            actual = await _landingPageVM.GetFilms();

            Assert.NotEmpty(actual);
            Assert.Equal(expectedGenreCount, actual.Count);
        }

    }
}
