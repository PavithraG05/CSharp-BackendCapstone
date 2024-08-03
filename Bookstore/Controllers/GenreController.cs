using AutoMapper;
using Bookstore.Models;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/genres")]
    public class GenreController : ControllerBase
    {
        private readonly IBookstoreRepository _bookstore;
        private readonly IMapper _mapper;


        public GenreController(IBookstoreRepository bookstore, IMapper mapper)
        {
            _bookstore = bookstore ?? throw new ArgumentNullException(nameof(bookstore));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenresDTO>>> GetGenres()
        {
            var genresEntities = await _bookstore.GetGenresAsync();
            return Ok(_mapper.Map<IEnumerable<GenresDTO>>(genresEntities));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<GenresDTO>>> GetGenre(int id)
        {
            var genreEntity = await _bookstore.GetGenreAsync(id);
            if (genreEntity == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GenresDTO>(genreEntity));

        }
    }
}
