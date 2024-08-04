using AutoMapper;
using Bookstore.Entities;
using Bookstore.Models;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace Bookstore.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookstoreRepository _bookstore;
        private readonly IMapper _mapper;


        public AuthorController(IBookstoreRepository bookstore, IMapper mapper)
        {
            _bookstore = bookstore ?? throw new ArgumentNullException(nameof(bookstore));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorsDTO>>> GetAuthors()
        {
            var authorsEntities = await _bookstore.GetAuthorsAsync();
            return Ok(_mapper.Map<IEnumerable<AuthorsDTO>>(authorsEntities));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AuthorsDTO>>> GetAuthor(int id)
        {
            var authorEntity = await _bookstore.GetAuthorAsync(id);
            if (authorEntity == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AuthorsDTO>(authorEntity));

        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorsDTO author)
        {
            if (author == null)
            {
                return BadRequest("Author data is required.");
            }

            if(string.IsNullOrWhiteSpace(author.Author_Name)|| string.IsNullOrWhiteSpace(author.Biography))
            {
                if (string.IsNullOrWhiteSpace(author.Author_Name) && string.IsNullOrWhiteSpace(author.Biography)) return BadRequest("Missing one or more author field values");
                else if (string.IsNullOrWhiteSpace(author.Author_Name)) return BadRequest("Author Name is missing");
                else return BadRequest("Author Biography is missing");
            }
            //getting model data from user
            if (!ModelState.IsValid)
            {
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
                        return CreatedAtAction(nameof(GetAuthor), new { id = authorToReturn.Author_Id }, authorToReturn);
                    }
                }
                catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
                {
                    // Handle unique constraint violation
                    return Conflict("An author with this name already exists.");
                }
                catch (Exception ex)
                {
                    // Log the exception and return a generic error response
                    // Logger.LogError(ex, "An error occurred while updating the author.");
                    return StatusCode(500, "An error occurred while adding the author.");
                }
            }
            return BadRequest("Unable to add the author.");

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
