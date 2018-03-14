using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesApi.DbModels;
using MoviesApi.Entities;
using System;
using System.Collections.Generic;

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

            context.Movies.AddRange(
                new Movie
                {
                    Genres = new List<Genre> { romanceGenre },
                    Id = 1,
                    RunningTime = 90,
                    Title = "Test Movie 1",
                    YearOfRelease = 1999
                },
                new Movie
                {
                    Genres = new List<Genre> { romanceGenre, comedyGenre },
                    Id = 2,
                    RunningTime = 120,
                    Title = "Test Movie 2",
                    YearOfRelease = 2001
                },
                new Movie
                {
                    Genres = new List<Genre> { dramaGenre },
                    Id = 3,
                    RunningTime = 98,
                    Title = "Serve",
                    YearOfRelease = 2003
                },
                new Movie
                {
                    Genres = new List<Genre> { actionGenre },
                    Id = 4,
                    RunningTime = 115,
                    Title = "Punch",
                    YearOfRelease = 2003
                });

            context.SaveChanges();
        }
    }
}
