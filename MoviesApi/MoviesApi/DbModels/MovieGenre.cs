using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApi.DbModels
{
    public class MovieGenre
    {
        [Key]
        [Column(Order = 0)]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [Key]
        [Column(Order = 1)]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
