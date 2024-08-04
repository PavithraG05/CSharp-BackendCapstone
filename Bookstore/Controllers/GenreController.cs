using AutoMapper;
using Bookstore.Models;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bookstore.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Controllers
{
    [ApiController]
    [Authorize]
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

        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody] GenresDTO genre)
        {
            if (genre == null)
            {
                return BadRequest("Genre data is required.");
            }

            if (string.IsNullOrWhiteSpace(genre.Genre_Name))
            {
                return BadRequest("Genre Name is missing");
            }
            //getting model data from user
            if (!ModelState.IsValid)
            {
                return BadRequest("Genre data is required.");
            }
            else if (ModelState.IsValid)
            {

                //converting model to entity <return type>
                var genreEntity = _mapper.Map<Genres>(genre);
                genreEntity.Created_At = DateTime.UtcNow;
                genreEntity.Updated_At = DateTime.UtcNow;
                genreEntity.Created_By = "admin";
                genreEntity.Updated_By = "admin";
                try
                {
                    await _bookstore.AddGenreAsync(genreEntity);

                    if (await _bookstore.SaveChangesAsync())
                    {
                        //converting model to entity
                        var genreToReturn = _mapper.Map<GenresDTO>(genreEntity);
                        return CreatedAtAction(nameof(GetGenre), new { id = genreToReturn.Genre_Id }, genreToReturn);
                    }
                }
                catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
                {
                    // Handle unique constraint violation
                    return Conflict("A genre with this name already exists.");
                }
                catch (Exception ex)
                {
                    // Log the exception and return a generic error response
                    // Logger.LogError(ex, "An error occurred while updating the author.");
                    return StatusCode(500, "An error occurred while adding the genre.");
                }
            }
            return BadRequest("Unable to add the genre.");

        }

        private bool IsUniqueConstraintViolation(DbUpdateException ex)
        {
            if (ex.InnerException is SqlException sqlException)
            {
                // SQL Server error number for unique constraint violation
                return sqlException.Number == 2627 || sqlException.Number == 2601;
            }

            return false;
        }
    }
}
