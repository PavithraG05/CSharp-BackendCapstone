using AutoMapper;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Bookstore.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IBookstoreRepository _bookstore;
        private readonly IMapper _mapper;
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
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }
        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            var user = ValidateCredentials(authenticationRequestBody.UserName, authenticationRequestBody.Password);

            if(user == null)
            {
                return Unauthorized();
            }
            

            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString()));
            //claimsForToken.Add(new Claim("given_name", user.Username.ToString());
            //claimsForToken.Add(new Claim("family_name", user.LastName));
            

            var jwtSecurityToken = new JwtSecurityToken(_configuration["Authentication:Issuer"], _configuration["Authentication:Audience"], claimsForToken, DateTime.UtcNow, DateTime.UtcNow.AddHours(1), signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(tokenToReturn);
        }

        public AuthenticationController(IConfiguration configuration, IBookstoreRepository bookstore, IMapper mapper)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _bookstore = bookstore ?? throw new ArgumentNullException(nameof(bookstore));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        private async Task<BookstoreUser> ValidateCredentials(string userName, string password)
        {
            var user = await _bookstore.ValidateUser(userName, password);

            if(user == null)
            {
                return null;
            }
            return new BookstoreUser(user.Id, user.Username, user.FirstName, user.LastName, user.Email, user.Password);
        }
    }
}
