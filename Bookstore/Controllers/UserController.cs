using AutoMapper;
using Bookstore.Models;
using Bookstore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    public class UserController:ControllerBase
    {
        private readonly IBookstoreRepository _bookstore;
        private readonly IMapper _mapper;


        public UserController(IBookstoreRepository bookstore, IMapper mapper)
        {
            _bookstore = bookstore ?? throw new ArgumentNullException(nameof(bookstore));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        
        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<UsersDTO>>> GetUser(string username)
        {
            var userEntity = await _bookstore.GetUserAsync(username);
            if (userEntity == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UsersDTO>(userEntity));

        }
    }
}
