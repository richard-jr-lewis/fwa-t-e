using MoviesApi.DbModels;
using System.Collections.Generic;

namespace MoviesApi.DataServices
{
    public interface IDataService
    {
        IEnumerable<Movie> GetMovies(string title, int? yearOfRelease);

        IEnumerable<Genre> GetGenres();
    }
}