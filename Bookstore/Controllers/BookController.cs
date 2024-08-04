using Bookstore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Bookstore.Models;
using Bookstore.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookstoreRepository _bookstore;
        private readonly IMapper _mapper;


        public BookController(IBookstoreRepository bookstore, IMapper mapper)
        {
            _bookstore = bookstore ?? throw new ArgumentNullException(nameof(bookstore));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BooksDTO>>> GetBooks()
        {
            var booksEntities = await _bookstore.GetBooksAsync();
            return Ok(_mapper.Map<IEnumerable<BooksDTO>>(booksEntities));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BooksDTO>>> GetBook(int id)
        {
            var bookEntity = await _bookstore.GetBookAsync(id);
            if (bookEntity == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BooksDTO>(bookEntity));

        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BooksDTO book)
        {
            if (book == null)
            {
                return BadRequest("Book data is required.");
            }

            if (string.IsNullOrWhiteSpace(book.Title) || (book.Publication_Date == null) || book.Price == null || (string.IsNullOrWhiteSpace(book.Description)))
            {
                return BadRequest("One or more fields are missing values in books field"); 
            }

            if (book.Price < 0) {
                return BadRequest("Price cannot have negative values.");
            }
            //getting model data from user
            if (!ModelState.IsValid)
            {
                return BadRequest("Book data is required.");
            }
            else if (ModelState.IsValid)
            {

                //converting model to entity <return type>
                var bookEntity = _mapper.Map<Books>(book);
                bookEntity.Created_At = DateTime.UtcNow;
                bookEntity.Updated_At = DateTime.UtcNow;
                bookEntity.Created_By = "admin";
                bookEntity.Updated_By = "admin";
                try
                {
                    await _bookstore.AddBookAsync(bookEntity);

                    if (await _bookstore.SaveChangesAsync())
                    {
                        //converting model to entity
                        var bookToReturn = _mapper.Map<BooksDTO>(bookEntity);
                        return CreatedAtAction(nameof(GetBook), new { id = bookToReturn.Id }, bookToReturn);
                    }
                }
                catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
                {
                    // Handle unique constraint violation
                    return Conflict("A book with this name already exists.");
                }
                catch (Exception ex)
                {
                    // Log the exception and return a generic error response
                    // Logger.LogError(ex, "An error occurred while updating the author.");
                    return StatusCode(500, "An error occurred while adding the book.");
                }
            }
            return BadRequest("Unable to add the book.");

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
