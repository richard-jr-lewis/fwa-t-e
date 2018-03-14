using Microsoft.EntityFrameworkCore;
using MoviesApi.Controllers;
using MoviesApi.DataServices;
using MoviesApi.DbModels;
using MoviesApi.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace MoviesApi.Tests
{
    public class DatabaseFixture
    {
        private DataContext _datacontext;
        private DbContextOptions<DataContext> Options;

        public DatabaseFixture()
        {
            Options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("MovieApi")
                .Options;

            using (_datacontext = new DataContext(Options))
            {
                _datacontext.Movies.Add(new Movie { Id = 1, Title = "Hello World", YearOfRelease=2006 });
                _datacontext.Movies.Add(new Movie { Id = 2, Title = "Hello World 2: Hello Worlder", YearOfRelease=2018 });
                _datacontext.Movies.Add(new Movie { Id = 3, Title = "Freewheeling - A Guide To Digital Advertising Success", YearOfRelease = 2018 });
                _datacontext.SaveChanges();
            }
        }
    }

    public class MoviesApiTests : IClassFixture<DatabaseFixture>
    {
        private DatabaseFixture _databaseFixture;

        public MoviesApiTests(DatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;
        }

        //[Fact]
        //public void CallQueryMovieNoCriteria()
        //{
        //    var options = new DbContextOptionsBuilder<DataContext>()
        //        .UseInMemoryDatabase("MovieApi")
        //        .Options;

        //    //PopulateTestData(options);

        //    using (var context = new DataContext(options))
        //    {
        //        //var controller = MoviesApiController(context);
        //    }

        //    var response = HttpStatusCode.OK;

        //    Assert.Equal(HttpStatusCode.BadRequest, response);
        //}

        [Theory]
        [InlineData("test1")]
        [InlineData("test2")]
        public void QueryMovieTitleNoResultsFound(string title)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("MovieApi")
                .Options;

            using (var context = new DataContext(options))
            {
                var dataService = new DataService(context);
                var result = dataService.GetMovies(title, null);

                Assert.Empty(result);
            }
        }

        [Theory]
        [InlineData("Hello World")]
        [InlineData("Hello World 2: Hello Worlder")]
        public void QueryMovieTitleResultsFound(string title)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("MovieApi")
                .Options;

            using (var context = new DataContext(options))
            {
                var dataService = new DataService(context);
                var result = dataService.GetMovies(title, null);

                Assert.NotEmpty(result);
                Assert.All(result, x => Assert.Contains(title, x.Title));
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void QueryMovieYearOfReleaseNoResultsFound(int? yearOfRelease)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("MovieApi")
                .Options;

            using (var context = new DataContext(options))
            {
                var dataService = new DataService(context);
                var result = dataService.GetMovies(null, yearOfRelease);

                Assert.Empty(result);
            }
        }

        [Theory]
        [InlineData(2006)]
        [InlineData(2018)]
        public void QueryMovieYearOfReleaseResultsFound(int? yearOfRelease)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("MovieApi")
                .Options;

            using (var context = new DataContext(options))
            {
                var dataService = new DataService(context);
                var result = dataService.GetMovies(null, yearOfRelease);

                Assert.NotEmpty(result);
                Assert.All(result, x => Assert.Equal(yearOfRelease, x.YearOfRelease));
            }
        }
    }
}
