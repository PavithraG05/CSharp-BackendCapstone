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
        private readonly ILogger<GenreController> _logger;

        public GenreController(IBookstoreRepository bookstore, IMapper mapper, ILogger<GenreController> logger)
        {
            _bookstore = bookstore ?? throw new ArgumentNullException(nameof(bookstore));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenresDTO>>> GetGenres(string name)
        {
           
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogInformation("Triggering api to get genre list");
                var genresEntities = await _bookstore.GetGenresAsync();
                _logger.LogInformation("Retrieved genre list successfully");
                return Ok(_mapper.Map<IEnumerable<GenresDTO>>(genresEntities));
            }

            var genresEntity = await _bookstore.GetGenresbyGenrenameAsync(name);
            if (genresEntity == null)
            {
                _logger.LogError($"Genres not found");
                return NotFound();
            }
            _logger.LogInformation($"Retrieved Genres data successfully");
            return Ok(_mapper.Map<IEnumerable<GenresDTO>>(genresEntity));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<GenresDTO>>> GetGenre(int id)
        {
            try
            {
                _logger.LogInformation("Triggering api to get genre by Id");
                var genreEntity = await _bookstore.GetGenreAsync(id);
                if (genreEntity == null)
                {
                _logger.LogError($"Genre with ID:{id} doesnt exist");
                    return NotFound();
                }
                _logger.LogInformation("Retrieved genre by ID successfully");
                return Ok(_mapper.Map<GenresDTO>(genreEntity));
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                _logger.LogError(ex, "An error occurred while retrieving the genre.");
                return StatusCode(500, "An error occurred while retrieving the genre.");
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody] GenresDTO genre)
        {
            _logger.LogInformation("Triggering API to add genre");
            if (genre == null)
            {
                _logger.LogError($"Genre data is required.");
                return BadRequest("Genre data is required.");
            }

            if (string.IsNullOrWhiteSpace(genre.Genre_Name))
            {
                _logger.LogError($"Genre Name is missing.");
                return BadRequest("Genre Name is missing");
            }
            //getting model data from user
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Genre data is required.");
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
                        _logger.LogInformation("Added genre successfully");
                        return CreatedAtAction(nameof(GetGenre), new { id = genreToReturn.Genre_Id }, genreToReturn);
                    }
                }
                catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
                {
                    // Handle unique constraint violation
                    _logger.LogError(ex, "A genre with this name already exists.");
                    return Conflict("A genre with this name already exists.");
                }
                catch (Exception ex)
                {
                    // Log the exception and return a generic error response
                    _logger.LogError(ex, "An error occurred while adding the genre.");
                    return StatusCode(500, "An error occurred while adding the genre.");
                }
            }
            _logger.LogError("An error occurred while adding the genre.");
            return BadRequest("Unable to add the genre.");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] GenresUpdateDTO genre)
        {
            _logger.LogInformation("Triggering API to update genre");

            if (genre == null)
            {
                _logger.LogError($"Genre data is required.");
                return BadRequest("Genre data is required.");
            }

            if (string.IsNullOrWhiteSpace(genre.Genre_Name))
            {
                _logger.LogError($"Genre name is empty.");
                return BadRequest("Genre name is empty");
            }

            var existingGenreEntity = await _bookstore.GetGenreAsync(id);
            if (existingGenreEntity == null)
            {
                _logger.LogError($"Genre {id} not found");
                return NotFound();
            }

            if (existingGenreEntity != null)
            {
                //existingAuthorEntity.Author_Id= id;
                existingGenreEntity.Genre_Name = genre.Genre_Name;
                existingGenreEntity.Updated_At = DateTime.UtcNow;
            }
            try
            {
                await _bookstore.UpdateGenreAsync(id, existingGenreEntity);

                if (await _bookstore.SaveChangesAsync())
                {
                    //converting model to entity
                    var genreToReturn = _mapper.Map<GenresDTO>(existingGenreEntity);
                    return NoContent();
                }
            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                _logger.LogError(ex, "A genre with this name already exists.");
                return Conflict("A genre with this name already exists.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log the exception and return a generic error response
                _logger.LogError(ex, "An error occurred while updating the genre.");
                return StatusCode(500, "An error occurred while updating the genre.");
            }

            _logger.LogError("An error occurred while updating the genre.");
            return StatusCode(500, "An error occurred while updating the genre.");
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
