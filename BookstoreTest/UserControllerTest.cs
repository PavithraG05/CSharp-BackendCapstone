using AutoMapper;
using Bookstore.Controllers;
using Bookstore.Entities;
using Bookstore.Models;
using Bookstore.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreTest
{
    public class UserControllerTest
    {
        private readonly UserController _controller;
        private readonly IBookstoreRepository _mockBookstore;
        private readonly ILogger<UserController> _mockLogger;
        private readonly IMapper _mockMapper;
        //private readonly IMapper _mockMapper;

        public UserControllerTest()
        {
            _mockBookstore = A.Fake<IBookstoreRepository>();
            _mockLogger = A.Fake<ILogger<UserController>>();
            _mockMapper = A.Fake<IMapper>();

            //var _mockMapperCfg = new MapperConfiguration(cfg => cfg.CreateMap<Authors, AuthorsDTO>());
            //_mockMapper = _mockMapperCfg.CreateMapper();
            _controller = new UserController(_mockBookstore, _mockMapper, _mockLogger);
        }


        [Fact]
        public async Task GetUser_ReturnsOkResult()
        {
            var userEntity = new Users {Id = 1, FirstName = "kart" ,LastName = "Neon",Email="gmail",Username="avin",Password="pavi",Created_At = DateTime.UtcNow,Updated_At = DateTime.UtcNow,Updated_By="admin",Created_By="admin"}
            ;

            var usersDTO = new UsersDTO {Id = 1, FirstName = "kart" ,LastName = "Neon",Email="gmail",Username="avin",Password="pavi",Created_At = DateTime.UtcNow,Updated_At = DateTime.UtcNow,Updated_By="admin",Created_By="admin"}
            ;

            string username = "admin";
            A.CallTo(() => _mockBookstore.GetUserAsync(username)).Returns(Task.FromResult<Users?>(userEntity));
            A.CallTo(() => _mockMapper.Map<UsersDTO>(userEntity)).Returns(usersDTO);
            var result = await _controller.GetUser(username);

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(usersDTO);
        }
    }
}
