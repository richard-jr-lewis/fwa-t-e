using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DbModels
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public double Value { get; set; }
    }
}
