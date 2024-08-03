using Bookstore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Bookstore.Models;

namespace Bookstore.Controllers
{
    [ApiController]
    //[Authorize]
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
    }
}
