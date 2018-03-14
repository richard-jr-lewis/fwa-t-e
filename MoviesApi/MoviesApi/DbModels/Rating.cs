using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DbModels
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        public decimal Value { get; set; }
    }
}
