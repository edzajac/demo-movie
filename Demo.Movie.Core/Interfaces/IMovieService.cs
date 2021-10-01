
using System.Threading.Tasks;
using Demo.Movie.Core.Model;

namespace Demo.Movie.Core.Interfaces
{
    public interface IMovieService
    {
        Task<ImageConfigurationResponse> GetImageConfiguration();
        Task<GenreResponse> GetAvailableGenres();
        Task<PopularFilmsResponse> GetMostRecentPopularFilms();
        Task<FilmTrailerResponse> GetTrailerInformationByFilmId(int id);
    }
}
