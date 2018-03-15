using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DbModels
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
