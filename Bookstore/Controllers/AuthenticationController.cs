using Asp.Versioning;
using AutoMapper;
using Bookstore.Entities;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Bookstore.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/authentication")]
    [ApiVersion(1)]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IBookstoreRepository _bookstore;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationController> _logger;
        private class BookstoreUser
        {

            public int Id { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }

            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }


            public BookstoreUser(int Id, string UserName, string FirstName, string LastName, string Email, string Password)
            {
                this.Id = Id;
                this.UserName = UserName;
                this.FirstName = FirstName;
                this.LastName = LastName;
                this.Email = Email;
                this.Password = Password;

            }
        }

        public class AuthenticationRequestBody
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            var user = await ValidateCredentials(authenticationRequestBody.UserName, authenticationRequestBody.Password);

            if(user == null)
            {
                _logger.LogError("User unauthorized");
                return Unauthorized();
            }
            

            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName), // Adding username claim
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName), // Adding first name claim
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName), // Adding last name claim
                new Claim(JwtRegisteredClaimNames.Email, user.Email) // Adding email claim
            };

            var jwtSecurityToken = new JwtSecurityToken(_configuration["Authentication:Issuer"], _configuration["Authentication:Audience"], claimsForToken, DateTime.UtcNow, DateTime.UtcNow.AddHours(2), signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            _logger.LogInformation("Token retrieved successfully");
            return Ok(tokenToReturn);
        }

        public AuthenticationController(IConfiguration configuration, IBookstoreRepository bookstore, IMapper mapper, ILogger<AuthenticationController> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _bookstore = bookstore ?? throw new ArgumentNullException(nameof(bookstore));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        private async Task<BookstoreUser> ValidateCredentials(string userName, string password)
        {
            //var user = await _bookstore.ValidateUser(userName, password);
            var user = await _bookstore.GetUserAsync(userName);
            if (user == null)
            {
                return null;
            }
            return new BookstoreUser(user.Id, user.Username, user.FirstName, user.LastName, user.Email, user.Password);

            //if (userName == "admin" && password == "pass@123")
            //{
            //    return new BookstoreUser(1, "admin", "Pavithra", "g", "pavi@gmail", "pass@123");

            //}
            //return null;

        }
    }
}
