using Microsoft.AspNetCore.Mvc;
using MoviesApi.DataServices;
using System.Linq;

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

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dataService.GetMovies(null, null));
        }

        [HttpGet("title/{title}")]
        public IActionResult GetMoviesByTitle(string title)
        {
            var results = _dataService.GetMovies(title, null);

            if (results.Count() == 0)
            {
                return NotFound();
            }

            return Ok(results);
        }

        [HttpGet("title")]
        public IActionResult GetMoviesByTitleNoSearchCriteria()
        {
            return BadRequest();
        }

        [HttpGet("year/{year}")]
        public IActionResult GetMoviesByYearOfRelease(int year)
        {
            var results = _dataService.GetMovies(null, year);

            if (results.Count() == 0)
            {
                return NotFound();
            }

            return Ok(results);
        }

        [HttpGet("year")]
        public IActionResult GetMoviesByYearOfReleaseNoSearchCriteria()
        {
            return BadRequest();
        }

        [HttpGet("genres")]
        public IActionResult GetGenres()
        {
            return Ok(_dataService.GetGenres());
        }
    }
}
