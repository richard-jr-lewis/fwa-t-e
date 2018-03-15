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

        public async Task<IEnumerable<Movie>> GetMovies(string title, int? yearOfRelease)
        {
            return await Task.FromResult(_dataContext.Movies
                .WhereIf(!string.IsNullOrEmpty(title), x => x.Title.ToLower().Contains(title.ToLower()))
                .WhereIf(yearOfRelease.HasValue, x => x.YearOfRelease == yearOfRelease));
        }

        public async Task<IEnumerable<Genre>> GetGenres()
        {
            return await Task.FromResult(_dataContext.Genres);
        }


        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
        {
            return await Task.FromResult(_dataContext.Movies
                .Where(x => x.MovieGenres.Select(y=>y.Genre.Id).Contains(genreId)));
        }

        public async Task<IEnumerable<Rating>> GetTopFiveMovies()
        {
            return await Task.FromResult(_dataContext.Ratings
                .GroupBy(x => x.Movie, x => x.Value, (key, g) => new Rating { Movie = key, Value = Math.Round(g.Average() * 2, MidpointRounding.AwayFromZero) / 2 })
                .OrderByDescending(x => x.Value)
                .ThenBy(y => y.Movie.Title)
                .Take(5));
        }

        public async Task<IEnumerable<Rating>> GetTopFiveMovies(int userId)
        {
            return await Task.FromResult(_dataContext.Ratings
                .Where(x => x.User.Id == userId)
                .OrderByDescending(x => x.Value)
                .ThenBy(y => y.Movie.Title)
                .Select(x => new Rating { Movie = x.Movie, Value = x.Value })
                .Take(5));
        }

        public async Task UpdateRating(int userId, int movieId, int value)
        {
            var record = _dataContext.Ratings
                .Where(x => x.User.Id == userId)
                .Where(x => x.Movie.Id == movieId)
                .FirstOrDefault();

            if (record == null)
            {
                record = new Rating
                {
                    Id = _dataContext.Ratings.Count() + 1,
                    User = _dataContext.Users.Where(x => x.Id == userId).First(),
                    Movie = _dataContext.Movies.Where(x => x.Id == movieId).First(),
                    Value = value
                };

                _dataContext.Ratings.Add(record);
            }
            else
            {
                record.Value = value;

                _dataContext.Ratings.Update(record);
            }

            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> DoesMovieExist(int movieId)
        {
            return await Task.FromResult(_dataContext.Movies
                .Any(x => x.Id == movieId));
        }

        public async Task<bool> DoesUserExist(int userId)
        {
            return await Task.FromResult(_dataContext.Users
                .Any(x => x.Id == userId));
        }
    }
}
