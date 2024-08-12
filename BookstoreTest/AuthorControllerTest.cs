using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Bookstore.Models; 
using Bookstore.Entities;
//using Moq;
using FakeItEasy;
using FluentAssertions;
using Xunit;
using Bookstore.Controllers;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Profiles;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BookstoreTest
{
    public class AuthorControllerTest
    {
        private readonly AuthorController _controller;
        private readonly IBookstoreRepository _mockBookstore;
        private readonly ILogger<AuthorController> _mockLogger;
        private readonly IMapper _mockMapper;
        //private readonly IMapper _mockMapper;

        public AuthorControllerTest()
        {
            _mockBookstore = A.Fake<IBookstoreRepository>();
            _mockLogger = A.Fake<ILogger<AuthorController>>();
            _mockMapper = A.Fake<IMapper>();

            //var _mockMapperCfg = new MapperConfiguration(cfg => cfg.CreateMap<Authors, AuthorsDTO>());
            //_mockMapper = _mockMapperCfg.CreateMapper();
            _controller = new AuthorController(_mockBookstore, _mockMapper, _mockLogger);
        }

        //[Fact]
        //public async Task GetAuthorById_ReturnsOkResult()
        //{
        //    int authorId = 1;
        //    Authors authorEntry = authorsEntity[0];
        //    //AuthorsDTO authorDTOEntry = authorsDTO[0];
        //    //var authorsList = authorsEntity;

        //    _mockBookstore.Setup(x => x.GetAuthorAsync(authorId)).ReturnsAsync(authorEntry);
        //    //var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<AuthorProfile>());
        //    //var mapper = new Mapper(mapperConfiguration);
        //    //_controller = new AuthorController(_mockBookstore.Object, mapper, _mockLogger.Object);

        //    //_mockMapper.Setup(m => m.Map<IEnumerable<AuthorsDTO>>(authorEntry)).Returns(authorsDTO);
        //    // Act
        //    var result = await _controller.GetAuthor(authorId);
        //    //var res = Assert.IsType<ActionResult<IEnumerable<AuthorsDTO>>>(result);
        //    var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        //    //Assert.NotNull(result);
        //    Assert.Equal(200, actionResult.StatusCode);

        //    var returnValue = result.Value as AuthorsDTO;

        //    // Assert
        //    //Assert.NotNull(returnValue);
        //    //Assert.Single(returnValue);
        //    Assert.Equal(authorId, returnValue.Author_Id);
        //}

       

        [Fact]
        public async Task GetAuthors_ReturnsOkResult()
        {
            var authorsEntity = new List<Authors>
            {
                new Authors {Author_Id = 1, Author_Name = "TestAuthor1", Biography = "TestBio1", Created_At = DateTime.UtcNow,Updated_At = DateTime.UtcNow,Updated_By="admin",Created_By="admin",Books = []}
            };

            var authorsDTO = new List<AuthorsDTO>
            {
                new AuthorsDTO {Author_Id = 1, Author_Name = "TestAuthor1", Biography = "TestBio1"}
            };

            A.CallTo(() => _mockBookstore.GetAuthorsAsync()).Returns(Task.FromResult<IEnumerable<Authors>>(authorsEntity));
            A.CallTo(() => _mockMapper.Map<IEnumerable<AuthorsDTO>>(authorsEntity)).Returns(authorsDTO);

            //var authorsList = authorsEntity;
            //_mockBookstore.Setup(x => x.GetAuthorsAsync()).ReturnsAsync(authorsEntity);
            //_mockMapper.Setup(m => m.Map<IEnumerable<AuthorsDTO>>(authorsEntity)).Returns(authorsDTO);
            //var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<AuthorProfile>());
            //var mapper = new Mapper(mapperConfiguration);
            // Act
            var result = await _controller.GetAuthors(null);

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(authorsDTO);

            //var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            //Assert.NotNull(result);
            //Assert.Equal(200, actionResult.StatusCode);

            //var returnValue = result.Value as IEnumerable<AuthorsDTO>;

            //// Assert
            //Assert.NotNull(returnValue);
            //Assert.Single(returnValue);
            //Assert.Equal("TestAuthor1", returnValue.First().Author_Name);
        }

        [Fact]
        public async Task GetAuthorsbyName_ReturnsOkResult()
        {
            var authorsEntity = new List<Authors>
            {
                new Authors {Author_Id = 1, Author_Name = "TestAuthor1", Biography = "TestBio1", Created_At = DateTime.UtcNow,Updated_At = DateTime.UtcNow,Updated_By="admin",Created_By="admin",Books = []}
            };

            var authorsDTO = new List<AuthorsDTO>
            {
                new AuthorsDTO {Author_Id = 1, Author_Name = "TestAuthor1", Biography = "TestBio1"}
            };

            string search = "The";

            A.CallTo(() => _mockBookstore.GetAuthorsbyAuthornameAsync(search)).Returns(Task.FromResult<IEnumerable<Authors>>(authorsEntity));
            A.CallTo(() => _mockMapper.Map<IEnumerable<AuthorsDTO>>(authorsEntity)).Returns(authorsDTO);

            var result = await _controller.GetAuthors(search);

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(authorsDTO);
        }

        [Fact]
        public async Task GetAuthorbyID_ReturnsOkResult()
        {
            var authorsEntity = new Authors {Author_Id = 1, Author_Name = "TestAuthor1", Biography = "TestBio1", Created_At = DateTime.UtcNow,Updated_At = DateTime.UtcNow,Updated_By="admin",Created_By="admin",Books = []};

            var authorsDTO = new AuthorsDTO {Author_Id = 1, Author_Name = "TestAuthor1", Biography = "TestBio1"};

            int author_id = 1;

            A.CallTo(() => _mockBookstore.GetAuthorAsync(author_id)).Returns(Task.FromResult<Authors?>(authorsEntity));
            A.CallTo(() => _mockMapper.Map<AuthorsDTO>(authorsEntity)).Returns(authorsDTO);

            var result = await _controller.GetAuthor(author_id);

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedDto = okResult.Value.Should().BeOfType<AuthorsDTO>().Subject;
            returnedDto.Author_Name.Should().Be(authorsEntity.Author_Name);
        }

        [Fact]
        public async Task GetAuthorbyID_NOTFound_ReturnsOkResult()
        {
            var authorsEntity = new Authors { Author_Id = 1, Author_Name = "TestAuthor1", Biography = "TestBio1", Created_At = DateTime.UtcNow, Updated_At = DateTime.UtcNow, Updated_By = "admin", Created_By = "admin", Books = [] };

            var authorsDTO = new AuthorsDTO { Author_Id = 1, Author_Name = "TestAuthor1", Biography = "TestBio1" };

            int author_id = 1000;

            A.CallTo(() => _mockBookstore.GetAuthorAsync(author_id)).Returns(Task.FromResult<Authors?>(null));
            A.CallTo(() => _mockMapper.Map<AuthorsDTO>(authorsEntity)).Returns(authorsDTO);

            var result = await _controller.GetAuthor(author_id);

            result.Result.Should().BeOfType<NotFoundResult>();

        }
    }
}
