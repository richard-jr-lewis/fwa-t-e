using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DbModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }
    }
}
