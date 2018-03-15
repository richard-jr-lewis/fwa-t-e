using MoviesApi.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApi.DataServices
{
    public interface IDataService
    {
        Task<IEnumerable<Movie>> GetMovies(string title, int? yearOfRelease);

        Task<IEnumerable<Genre>> GetGenres();

        Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId);

        Task<IEnumerable<Rating>> GetTopFiveMovies();

        Task<IEnumerable<Rating>> GetTopFiveMovies(int userId);

        Task UpdateRating(int userId, int movieId, int value);

        Task<bool> DoesMovieExist(int movieId);

        Task<bool> DoesUserExist(int userId);
    }
}