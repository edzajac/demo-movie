
using System.Threading.Tasks;
using Demo.Movie.Core.Interfaces;
using Demo.Movie.Core.Model;

namespace Demo.Movie.Core.Services
{
    public class MovieService : BaseHttpClient, IMovieService
    {
        protected override string ServiceName => nameof(MovieService);

        public Task<ImageConfigurationResponse> GetImageConfiguration()
        {
            return GetAsync<ImageConfigurationResponse>($"configuration?api_key={ApiKey}");
        }

        public Task<GenreResponse> GetAvailableGenres()
        {
            return GetAsync<GenreResponse>($"genre/movie/list?api_key={ApiKey}&language=en-US");
        }

        public Task<PopularFilmsResponse> GetMostRecentPopularFilms()
        {
            return GetAsync<PopularFilmsResponse>($"discover/movie?api_key={ApiKey}&sort_by=popularity.desc");
        }

        public Task<FilmTrailerResponse> GetTrailerInformationByFilmId(int id)
        {
            return GetAsync<FilmTrailerResponse>($"movie/{id}/videos?api_key={ApiKey}&language=en-US");
        }

    }
}
