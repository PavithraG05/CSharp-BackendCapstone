using AutoMapper;
using Bookstore.Models;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [ApiController]
    //[Authorize]
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
    }
}
