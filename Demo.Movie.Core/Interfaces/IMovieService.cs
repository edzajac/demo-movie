
using System.Threading.Tasks;
using Demo.Movie.Core.Model;

namespace Demo.Movie.Core.Interfaces
{
    public interface IMovieService
    {
        Task<GenreResponse> GetAvailableGenres();
        Task<PopularFilmsResponse> GetMostRecentPopularFilms();
    }
}
