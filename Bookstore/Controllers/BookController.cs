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
        private readonly ILogger<BookController> _logger;

        public BookController(IBookstoreRepository bookstore, IMapper mapper, ILogger<BookController> logger)
        {
            _bookstore = bookstore ?? throw new ArgumentNullException(nameof(bookstore));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BooksDTO>>> GetBooks(string? name)
        {
            
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    _logger.LogInformation($"Triggering API to retrieve Book data");
                    var booksEntities = await _bookstore.GetBooksAsync();
                    _logger.LogInformation($"Retrieved Book data successfully");
                    return Ok(_mapper.Map<IEnumerable<BooksDTO>>(booksEntities));
                }

                var booksEntity = await _bookstore.GetBooksbyBooknameAsync(name);
                if (booksEntity == null)
                {
                    _logger.LogError($"Books not found");
                    return NotFound();
                }
                _logger.LogInformation($"Retrieved Book data successfully");
                return Ok(_mapper.Map<IEnumerable<BooksDTO>>(booksEntity));
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                _logger.LogError(ex, "An error occurred while retrieving the book.");
                return StatusCode(500, "An error occurred while retrieving the book.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BooksDTO>>> GetBook(int id)
        {
            try
            {
                _logger.LogInformation($"Triggering API to retrieve Book data by ID");

                var bookEntity = await _bookstore.GetBookAsync(id);
                if (bookEntity == null)
                {
                    _logger.LogError($"Book ID:{id} not found");
                    return NotFound();
                }
                _logger.LogInformation($"Retrieved Book data successfully");
                return Ok(_mapper.Map<BooksDTO>(bookEntity));
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                _logger.LogError(ex, "An error occurred while retrieving the book.");
                return StatusCode(500, "An error occurred while retrieving the book.");
            }

        }

        

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BooksDTO book)
        {
            _logger.LogInformation($"Triggering API to add Book data");

            if (book == null)
            {
                _logger.LogError($"Book data is required.");
                return BadRequest("Book data is required.");
            }

            if (string.IsNullOrWhiteSpace(book.Title) || (book.Publication_Date == null) || book.Price == null || (string.IsNullOrWhiteSpace(book.Description)))
            {
                _logger.LogError($"One or more fields are missing values in books field");
                return BadRequest("One or more fields are missing values in books field"); 
            }

            if (book.Price < 0) {
                _logger.LogError($"Book Price cannot have negative values.");
                return BadRequest("Price cannot have negative values.");
            }
            //getting model data from user
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Book data is required.");
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
                        _logger.LogInformation($"Added Book data successfully");
                        return CreatedAtAction(nameof(GetBook), new { id = bookToReturn.Id }, bookToReturn);
                    }
                }
                catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
                {
                    // Handle unique constraint violation
                    _logger.LogError(ex, "A book with this name already exists.");
                    return Conflict("A book with this name already exists.");
                }
                catch (Exception ex)
                {
                    // Log the exception and return a generic error response
                    _logger.LogError(ex, "An error occurred while adding the book.");
                    return StatusCode(500, "An error occurred while adding the book.");
                }
            }
            _logger.LogError("An error occurred while adding the book.");
            return BadRequest("Unable to add the book.");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] BooksUpdateDTO book)
        {
            _logger.LogInformation($"Triggering API to update Book data");

            if (book == null)
            {
                _logger.LogError($"Book data is required.");
                return BadRequest("Book data is required.");
            }

            if (string.IsNullOrWhiteSpace(book.Title) || (book.Publication_Date == null) || book.Price == null || (string.IsNullOrWhiteSpace(book.Description)))
            {
                _logger.LogError($"One or more fields are missing values in books field");
                return BadRequest("One or more fields are missing values in books field");
            }

            if (book.Price < 0)
            {
                _logger.LogError($"Book Price cannot have negative values.");
                return BadRequest("Price cannot have negative values.");
            }

            var existingBookEntity = await _bookstore.GetBookAsync(id);
            if (existingBookEntity == null)
            {
                _logger.LogError($"Book ID:{id} not found");
                return NotFound();
            }

            if (existingBookEntity != null)
            {
                existingBookEntity.Title= book.Title;
                existingBookEntity.Price = book.Price;
                existingBookEntity.Publication_Date = book.Publication_Date;
                existingBookEntity.Author_Id = book.Author_Id;
                existingBookEntity.Genre_Id = book.Genre_Id;
                existingBookEntity.Updated_At = DateTime.UtcNow;
            }
            try
            {
                await _bookstore.UpdateBookAsync(id, existingBookEntity);

                if (await _bookstore.SaveChangesAsync())
                {
                    //converting model to entity
                    var bookToReturn = _mapper.Map<BooksDTO>(existingBookEntity);
                    _logger.LogInformation($"Updated Book data successfully");
                    return NoContent();
                }
            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                _logger.LogError(ex, "A book with this name already exists.");
                return Conflict("A book with this name already exists.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log the exception and return a generic error response
                _logger.LogError(ex, "An error occurred while updating the book.");
                return StatusCode(500, "An error occurred while updating the book.");
            }
            _logger.LogError("An error occurred while updating the book.");
            return StatusCode(500, "An error occurred while updating the book.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {

            try
            {
                var existingBookEntity = await _bookstore.GetBookAsync(id);
                if (existingBookEntity == null)
                {
                    _logger.LogError($"Book ID:{id} not found");
                    return NotFound();
                }
                await _bookstore.DeleteBookAsync(id, existingBookEntity);

                if (await _bookstore.SaveChangesAsync())
                {
                    //converting model to entity
                   // var bookToReturn = _mapper.Map<BooksDTO>(existingBookEntity);
                    _logger.LogInformation($"Deleted Book data successfully");
                    return NoContent();
                }

            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                _logger.LogError(ex, "An error occurred while deleting the book.");
                return StatusCode(500, "An error occurred while deleting the book.");
            }
            _logger.LogError("An error occurred while deleting the book.");
            return StatusCode(500, "An error occurred while deleting the book.");
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Books>>> GetBooksbyGenre([FromQuery] string? name)
        //{
        //    try
        //    {
        //        //if (string.IsNullOrEmpty(name))
        //        //{
        //        //    await GetBooks();
        //        //}
        //        var genreEntity = await _bookstore.GetGenrebyNameAsync(name);
        //        if (genreEntity == null)
        //        {
        //            _logger.LogError($"Genre not found");
        //            return NotFound();
        //        }
        //        var booksEntity = await _bookstore.GetBooksbyGenreAsync(genreEntity.Genre_Id);
        //        if (booksEntity == null)
        //        {
        //            _logger.LogError($"Books not found");
        //            return NotFound();
        //        }
        //        _logger.LogInformation($"Retrieved Book data successfully");
        //        return Ok(_mapper.Map<IEnumerable<BooksDTO>>(booksEntity));
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception and return a generic error response
        //        _logger.LogError(ex, "An error occurred while retrieving the book.");
        //        return StatusCode(500, "An error occurred while retrieving the book.");
        //    }
        //}
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
