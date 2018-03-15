using Microsoft.AspNetCore.Mvc;
using MoviesApi.DataServices;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    public class MoviesApiController : Controller
    {
        private readonly IDataService _dataService;

        public MoviesApiController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dataService.GetMovies(null, null));
        }

        [HttpGet("search/{title}/{year}")]
        public async Task<IActionResult> GetMoviesByTitle(string title, int year)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest();
            }

            return await GetMovies(title, year);

        }

        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetMoviesByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest();
            }

            return await GetMovies(title, null);

        }

        [HttpGet("title")]
        public IActionResult GetMoviesByTitleNoSearchCriteria()
        {
            return BadRequest();
        }

        [HttpGet("year/{year}")]
        public async Task<IActionResult> GetMoviesByYearOfRelease(int year)
        {
            return await GetMovies(null, year);
        }

        [HttpGet("year")]
        public IActionResult GetMoviesByYearOfReleaseNoSearchCriteria()
        {
            return BadRequest();
        }

        private async Task<IActionResult> GetMovies(string title, int? year)
        {
            var results = await _dataService.GetMovies(title, year);

            if (results.Count() == 0)
            {
                return NotFound();
            }

            return Ok(results);
        }

        [HttpGet("genres")]
        public async Task<IActionResult> GetGenres()
        {
            return Ok(await _dataService.GetGenres());
        }

        [HttpGet("genre/{genreId}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId)
        {
            return Ok(await _dataService.GetMoviesByGenre(genreId));
        }

        [HttpGet("top5")]
        public async Task<IActionResult> GetTopFiveMovies()
        {
            return Ok(await _dataService.GetTopFiveMovies());
        }

        [HttpGet("top5/{userId}")]
        public async Task<IActionResult> GetTopFiveMovies(int userId)
        {
            return Ok(await _dataService.GetTopFiveMovies(userId));
        }

        [HttpPost("rating/{userId}/{movieId}/{value}")]
        public async Task<IActionResult> UpdateRating(int userId, int movieId, int value)
        {
            var doesUserExist = await _dataService.DoesUserExist(userId);
            var doesMovieExist = await _dataService.DoesUserExist(movieId);

            if (!doesUserExist || !doesMovieExist)
            {
                return NotFound();
            }

            if (value < 1 || value > 5)
            {
                return BadRequest();
            }

            await _dataService.UpdateRating(userId, movieId, value);

            return Ok();
        }
    }
}
