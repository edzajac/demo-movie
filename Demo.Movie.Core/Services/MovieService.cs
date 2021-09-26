
using System.Threading.Tasks;
using Demo.Movie.Core.AppSetup;
using Demo.Movie.Core.Interfaces;
using Demo.Movie.Core.Model;

namespace Demo.Movie.Core.Services
{
    public class MovieService : BaseHttpClient, IMovieService
    {
        public Task<ImageConfigurationResponse> GetImageConfiguration()
        {
            return GetAsync<ImageConfigurationResponse>($"configuration?api_key={AppConfig.Authentication.ApiKey}");
        }

        public Task<GenreResponse> GetAvailableGenres()
        {
            return GetAsync<GenreResponse>($"genre/movie/list?api_key={AppConfig.Authentication.ApiKey}&language=en-US");
        }

        public Task<PopularFilmsResponse> GetMostRecentPopularFilms()
        {
            return GetAsync<PopularFilmsResponse>($"discover/movie?api_key={AppConfig.Authentication.ApiKey}&sort_by=popularity.desc");
        }

    }
}
