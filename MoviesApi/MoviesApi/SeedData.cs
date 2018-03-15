using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesApi.DbModels;
using MoviesApi.Entities;
using System;

namespace MoviesApi
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                AddTestData(context);
            }
        }

        private static void AddTestData(DataContext context)
        {
            var actionGenre = new Genre { Id = 1, Name = "Action" };
            var romanceGenre = new Genre { Id = 2, Name = "Romance" };
            var comedyGenre = new Genre { Id = 3, Name = "Comedy" };
            var dramaGenre = new Genre { Id = 4, Name = "Drama" };

            context.Genres.AddRange(actionGenre,
                romanceGenre,
                comedyGenre,
                dramaGenre);

            var movie1 = new Movie { Id = 1, RunningTime = 90, Title = "Test Movie 1", YearOfRelease = 1999 };
            var movie2 = new Movie { Id = 2, RunningTime = 120, Title = "Test Movie 2", YearOfRelease = 2000 };
            var movie3 = new Movie { Id = 3, RunningTime = 98, Title = "Serve", YearOfRelease = 2001 };
            var movie4 = new Movie { Id = 4, RunningTime = 124, Title = "Punch", YearOfRelease = 2002 };
            var movie5 = new Movie { Id = 5, RunningTime = 115, Title = "Bats", YearOfRelease = 2003 };
            var movie6 = new Movie { Id = 6, RunningTime = 176, Title = "Go Banana - The Story Of Ralph Wiggum", YearOfRelease = 2003 };
            var movie7 = new Movie { Id = 7, RunningTime = 101, Title = "The Arena", YearOfRelease = 2005 };
            var movie8 = new Movie { Id = 8, RunningTime = 112, Title = "Drama For Life", YearOfRelease = 2007 };
            var movie9 = new Movie { Id = 9, RunningTime = 112, Title = "Bulls In The Bronx", YearOfRelease = 2009 };
            var movie10 = new Movie { Id = 10, RunningTime = 126, Title = "All I Want", YearOfRelease = 2011 };

            context.Movies.AddRange(
                movie1,
                movie2,
                movie3,
                movie4,
                movie5,
                movie6,
                movie7,
                movie8,
                movie9,
                movie10);

            context.MovieGenres.AddRange(
                new MovieGenre { Movie = movie1, Genre = romanceGenre },
                new MovieGenre { Movie = movie2, Genre = comedyGenre },
                new MovieGenre { Movie = movie2, Genre = dramaGenre },
                new MovieGenre { Movie = movie3, Genre = actionGenre },
                new MovieGenre { Movie = movie4, Genre = actionGenre },
                new MovieGenre { Movie = movie5, Genre = comedyGenre },
                new MovieGenre { Movie = movie5, Genre = dramaGenre },
                new MovieGenre { Movie = movie6, Genre = comedyGenre },
                new MovieGenre { Movie = movie6, Genre = actionGenre },
                new MovieGenre { Movie = movie7, Genre = comedyGenre },
                new MovieGenre { Movie = movie8, Genre = dramaGenre },
                new MovieGenre { Movie = movie9, Genre = actionGenre },
                new MovieGenre { Movie = movie9, Genre = romanceGenre },
                new MovieGenre { Movie = movie10, Genre = actionGenre });

            var user1 = new User { Id = 1 };
            var user2 = new User { Id = 2 };
            var user3 = new User { Id = 3 };
            var user4 = new User { Id = 4 };
            var user5 = new User { Id = 5 };
            var user6 = new User { Id = 6 };
            var user7 = new User { Id = 7 };
            var user8 = new User { Id = 8 };

            context.Users.AddRange(
                user1,
                user2,
                user3,
                user4,
                user5,
                user6,
                user7,
                user8);

            context.Ratings.AddRange(
                new Rating { Id = 1, User = user1, Movie = movie1, Value = 4 },
                new Rating { Id = 2, User = user1, Movie = movie5, Value = 1 },
                new Rating { Id = 3, User = user1, Movie = movie8, Value = 3 },
                new Rating { Id = 4, User = user2, Movie = movie2, Value = 5 },
                new Rating { Id = 5, User = user2, Movie = movie4, Value = 2 },
                new Rating { Id = 6, User = user2, Movie = movie6, Value = 4 },
                new Rating { Id = 7, User = user2, Movie = movie7, Value = 3 },
                new Rating { Id = 8, User = user3, Movie = movie8, Value = 3 },
                new Rating { Id = 9, User = user3, Movie = movie1, Value = 3},
                new Rating { Id = 10, User = user3, Movie = movie2, Value = 2 },
                new Rating { Id = 11, User = user3, Movie = movie7, Value = 5 },
                new Rating { Id = 12, User = user3, Movie = movie8, Value = 2 },
                new Rating { Id = 13, User = user3, Movie = movie9, Value = 4 },
                new Rating { Id = 14, User = user4, Movie = movie10, Value = 1 },
                new Rating { Id = 15, User = user6, Movie = movie3, Value = 1 },
                new Rating { Id = 16, User = user6, Movie = movie5, Value = 2 },
                new Rating { Id = 17, User = user6, Movie = movie8, Value = 3 },
                new Rating { Id = 18, User = user7, Movie = movie9, Value = 4 },
                new Rating { Id = 19, User = user7, Movie = movie10, Value = 5 },
                new Rating { Id = 20, User = user8, Movie = movie3, Value = 4 },
                new Rating { Id = 21, User = user8, Movie = movie4, Value = 2 },
                new Rating { Id = 22, User = user8, Movie = movie5, Value = 3 },
                new Rating { Id = 23, User = user8, Movie = movie6, Value = 4 },
                new Rating { Id = 24, User = user8, Movie = movie9, Value = 5 });

            context.SaveChanges();
        }
    }
}
