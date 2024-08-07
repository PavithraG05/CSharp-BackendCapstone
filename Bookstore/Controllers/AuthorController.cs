using AutoMapper;
using Bookstore.Entities;
using Bookstore.Models;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Reflection;
using Asp.Versioning;

namespace Bookstore.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/authors")]
    [ApiVersion(1)]
    public class AuthorController : ControllerBase
    {
        private readonly IBookstoreRepository _bookstore;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IBookstoreRepository bookstore, IMapper mapper, ILogger<AuthorController> logger)
        {
            _bookstore = bookstore ?? throw new ArgumentNullException(nameof(bookstore));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorsDTO>>> GetAuthors(string? name)
        {
            try
            {
                _logger.LogInformation($"Triggering Api call to retrive Authors list");
                if (string.IsNullOrEmpty(name))
                {
                    var authorsEntities = await _bookstore.GetAuthorsAsync();
                    _logger.LogInformation($"Retrieved Author data successfully");
                    return Ok(_mapper.Map<IEnumerable<AuthorsDTO>>(authorsEntities));
                }

                var authorsEntity = await _bookstore.GetAuthorsbyAuthornameAsync(name);
                if (authorsEntity == null)
                {
                    _logger.LogError($"Authors not found");
                    return NotFound();
                }
                _logger.LogInformation($"Retrieved Author data successfully");
                return Ok(_mapper.Map<IEnumerable<AuthorsDTO>>(authorsEntity));
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the author.");
                return StatusCode(500, "An error occurred while retrieving the author.");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AuthorsDTO>>> GetAuthor(int id)
        {
            try
            {

                _logger.LogInformation($"Triggering Api call to retrive Author by ID ");
                var authorEntity = await _bookstore.GetAuthorAsync(id);

                if (authorEntity == null)
                {
                    _logger.LogError($"Author with {id} doesnt exist");
                    return NotFound();
                }
                _logger.LogInformation($"Retrieved Author data successfully");
                return Ok(_mapper.Map<AuthorsDTO>(authorEntity));
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                // Logger.LogError(ex, "An error occurred while updating the author.");
                _logger.LogError(ex,$"An error occured while retrieving the author with {id}");
                return StatusCode(500, "An error occurred while retrieving the author.");
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorsDTO author)
        {
            _logger.LogInformation($"Triggering Api call to add Author ");

            if (author == null)
            {
                _logger.LogError($"Author data is required.");
                return BadRequest("Author data is required.");
            }

            if (string.IsNullOrWhiteSpace(author.Author_Name)|| string.IsNullOrWhiteSpace(author.Biography))
            {
                _logger.LogError($"Missing one or more author field values.");
                if (string.IsNullOrWhiteSpace(author.Author_Name) && string.IsNullOrWhiteSpace(author.Biography)) return BadRequest("Missing one or more author field values");
                else if (string.IsNullOrWhiteSpace(author.Author_Name)) return BadRequest("Author Name is empty");
                else return BadRequest("Author Biography is empty");
            }
            //getting model data from user
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Author data is required.");
                return BadRequest("Author data is required.");
            }
            else if (ModelState.IsValid)
            {
               
                //converting model to entity <return type>
                var authorEntity = _mapper.Map<Authors>(author);
                authorEntity.Created_At = DateTime.UtcNow;
                authorEntity.Updated_At = DateTime.UtcNow;
                authorEntity.Created_By = "admin";
                authorEntity.Updated_By = "admin";
                try
                {
                    await _bookstore.AddAuthorAsync(authorEntity);

                    if (await _bookstore.SaveChangesAsync())
                    {
                        //converting model to entity
                        var authorToReturn = _mapper.Map<AuthorsDTO>(authorEntity);
                        _logger.LogInformation($"Added Author data successfully");

                        return CreatedAtAction(nameof(GetAuthor), new { id = authorToReturn.Author_Id }, authorToReturn);
                    }
                }
                catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
                {
                    // Handle unique constraint violation
                    _logger.LogError(ex,$"An author with this name already exists.");
                    return Conflict("An author with this name already exists.");
                }
                catch (Exception ex)
                {
                    // Log the exception and return a generic error response
                    _logger.LogError(ex, "An error occurred while adding the author.");
                    return StatusCode(500, "An error occurred while adding the author.");
                }
            }
            _logger.LogError("An error occurred while adding the author.");
            return BadRequest("Unable to add the author.");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorsUpdateDTO author)
        {
            _logger.LogInformation($"Triggering Api call to update Author ");

            if (author == null)
            {
                _logger.LogError("Author data is required.");
                return BadRequest("Author data is required.");
            }

            if (string.IsNullOrWhiteSpace(author.Author_Name) || string.IsNullOrWhiteSpace(author.Biography))
            {
                _logger.LogError("Missing one or more author field values");
                if (string.IsNullOrWhiteSpace(author.Author_Name) && string.IsNullOrWhiteSpace(author.Biography)) return BadRequest("Missing one or more author field values");
                else if (string.IsNullOrWhiteSpace(author.Author_Name)) return BadRequest("Author Name is empty");
                else return BadRequest("Author Biography is empty");
            }

            var existingAuthorEntity = await _bookstore.GetAuthorAsync(id);
            if (existingAuthorEntity == null)
            {
                _logger.LogError("Author data not found.");
                return NotFound();
            }
            
            if (existingAuthorEntity != null)
            {
                //existingAuthorEntity.Author_Id= id;
                existingAuthorEntity.Author_Name = author.Author_Name;
                existingAuthorEntity.Biography = author.Biography;
                existingAuthorEntity.Updated_At = DateTime.UtcNow;
            }
            try
            {
                await _bookstore.UpdateAuthorAsync(id, existingAuthorEntity);

                if (await _bookstore.SaveChangesAsync())
                {
                    //converting model to entity
                    var authorToReturn = _mapper.Map<AuthorsDTO>(existingAuthorEntity);
                    _logger.LogInformation("Updated Author data successfully");
                    return NoContent() ;
                }
            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                _logger.LogError(ex, "An author with this name already exists.");
                return Conflict("An author with this name already exists.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log the exception and return a generic error response
                _logger.LogError(ex, "An error occurred while updating the author.");
                return StatusCode(500, "An error occurred while updating the author.");
            }
            _logger.LogError("An error occurred while updating the author.");
            return StatusCode(500, "An error occurred while updating the author.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {

            try
            {
                var existingAuthorEntity = await _bookstore.GetAuthorAsync(id);
                if (existingAuthorEntity == null)
                {
                    _logger.LogError($"Author ID:{id} not found");
                    return NotFound();
                }

                var authorBooksEntity = await _bookstore.GetAuthorBookAsync(id);
                if (authorBooksEntity != null)
                {
                    _logger.LogError($"Delete of author forbidden as it has books");
                    return Forbid();
                }

                await _bookstore.DeleteAuthorAsync(id, existingAuthorEntity);

                if (await _bookstore.SaveChangesAsync())
                {
                    //converting model to entity
                    // var bookToReturn = _mapper.Map<BooksDTO>(existingBookEntity);
                    _logger.LogInformation($"Deleted Author data successfully");
                    return NoContent();
                }

            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                _logger.LogError(ex, "An error occurred while deleting the author.");
                return StatusCode(500, "An error occurred while deleting the author.");
            }
            _logger.LogError("An error occurred while deleting the author.");
            return StatusCode(500, "An error occurred while deleting the author.");
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
