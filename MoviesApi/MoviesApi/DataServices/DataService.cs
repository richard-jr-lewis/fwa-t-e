using MoviesApi.DbModels;
using MoviesApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.DataServices
{
    public class DataService : IDataService
    {
        private DataContext _dataContext;

        public DataService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Movie> GetMovies(string title, int? yearOfRelease)
        {
            return _dataContext.Movies
                .WhereIf(!string.IsNullOrEmpty(title), x => x.Title.ToLower().Contains(title.ToLower()))
                .WhereIf(yearOfRelease.HasValue, x => x.YearOfRelease == yearOfRelease);
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _dataContext.Genres;
        }
    }
}
