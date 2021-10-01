
using Demo.Movie.Core.Interfaces;
using Demo.Movie.Tests.MockServices;

namespace Demo.Movie.Tests.ViewModelTests
{
    public class BaseViewModelTest
    {
        protected readonly IMovieService MovieService;

        public BaseViewModelTest()
        {
            MovieService = new MockMovieService();
        }
    }
}
