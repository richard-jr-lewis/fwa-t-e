using Microsoft.EntityFrameworkCore;
using MoviesApi.DataServices;
using MoviesApi.DbModels;
using MoviesApi.Entities;
using System;
using System.Linq;
using Xunit;

namespace MoviesApi.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public DataContext Datacontext { get; }
        private DbContextOptions<DataContext> Options;

        public DatabaseFixture()
        {
            Options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("MovieApi")
                .Options;

            Datacontext = new DataContext(Options);


            var genre1 = new Genre { Id = 1, Name = "Test1" };
            var genre2 = new Genre { Id = 2, Name = "Test2" };
            var genre3 = new Genre { Id = 3, Name = "Test3" };

            Datacontext.Genres.AddRange(genre1,
                genre2,
                genre3);

            var movie1 = new Movie { Id = 1, Title = "Hello World", YearOfRelease = 2006 };
            var movie2 = new Movie { Id = 2, Title = "Hello World 2: Hello Worlder", YearOfRelease = 2018 };
            var movie3 = new Movie { Id = 3, Title = "Freewheeling - A Guide To Digital Advertising Success", YearOfRelease = 2018 };

            Datacontext.Movies.AddRange(
                movie1,
                movie2,
                movie3);

            Datacontext.MovieGenres.AddRange(
                new MovieGenre { Movie = movie1, Genre = genre1 },
                new MovieGenre { Movie = movie1, Genre = genre2 },
                new MovieGenre { Movie = movie2, Genre = genre3 },
                new MovieGenre { Movie = movie3, Genre = genre1 },
                new MovieGenre { Movie = movie3, Genre = genre3 });

            Datacontext.SaveChanges();
        }

        public void Dispose()
        {
            Datacontext.Dispose();
        }
    }

    public class MoviesApiTests : IClassFixture<DatabaseFixture>
    {
        private DatabaseFixture _databaseFixture;

        public MoviesApiTests(DatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;
        }

        [Theory]
        [InlineData("test1")]
        [InlineData("test2")]
        public async void QueryMovieTitleNoResultsFound(string title)
        {
            var dataService = new DataService(_databaseFixture.Datacontext);
            var result = await dataService.GetMovies(title, null);

            Assert.Empty(result);
        }

        [Theory]
        [InlineData("Hello World")]
        [InlineData("Hello World 2: Hello Worlder")]
        public async void QueryMovieTitleResultsFound(string title)
        {
            var dataService = new DataService(_databaseFixture.Datacontext);
            var result = await dataService.GetMovies(title, null);

            Assert.NotEmpty(result);
            Assert.All(result, x => Assert.Contains(title, x.Title));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void QueryMovieYearOfReleaseNoResultsFound(int? yearOfRelease)
        {
            var dataService = new DataService(_databaseFixture.Datacontext);
            var result = await dataService.GetMovies(null, yearOfRelease);

            Assert.Empty(result);
        }

        [Theory]
        [InlineData(2006)]
        [InlineData(2018)]
        public async void QueryMovieYearOfReleaseResultsFound(int? yearOfRelease)
        {
            var dataService = new DataService(_databaseFixture.Datacontext);
            var result = await dataService.GetMovies(null, yearOfRelease);

            Assert.NotEmpty(result);
            Assert.All(result, x => Assert.Equal(yearOfRelease, x.YearOfRelease));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void QueryGenreResultsFound(int genreId)
        {
            var dataService = new DataService(_databaseFixture.Datacontext);
            var result = await dataService.GetMoviesByGenre(genreId);

            Assert.All(result, x => x.MovieGenres.Select(y => y.Genre.Id).Contains(genreId));
        }
    }
}
