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
        private readonly ILogger<UserController> _logger;

        public UserController(IBookstoreRepository bookstore, IMapper mapper, ILogger<UserController> logger)
        {
            _bookstore = bookstore ?? throw new ArgumentNullException(nameof(bookstore));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<UsersDTO>>> GetUser(string username)
        {
            try
            {
                _logger.LogInformation("Triggering API to get user.");
                var userEntity = await _bookstore.GetUserAsync(username);
                if (userEntity == null)
                {
                _logger.LogError("User not found");
                    return NotFound();
                }
                _logger.LogInformation("User retrieved successfully");
                return Ok(_mapper.Map<UsersDTO>(userEntity));
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                _logger.LogError(ex, "An error occurred while retrieving the user.");
                return StatusCode(500, "An error occurred while retrieving the user.");
            }
        }
    }
}
