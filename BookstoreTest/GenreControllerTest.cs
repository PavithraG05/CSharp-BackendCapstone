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
    public class GenreControllerTest
    {
        private readonly GenreController _controller;
        private readonly IBookstoreRepository _mockBookstore;
        private readonly ILogger<GenreController> _mockLogger;
        private readonly IMapper _mockMapper;
        //private readonly IMapper _mockMapper;

        public GenreControllerTest()
        {
            _mockBookstore = A.Fake<IBookstoreRepository>();
            _mockLogger = A.Fake<ILogger<GenreController>>();
            _mockMapper = A.Fake<IMapper>();

            //var _mockMapperCfg = new MapperConfiguration(cfg => cfg.CreateMap<Authors, AuthorsDTO>());
            //_mockMapper = _mockMapperCfg.CreateMapper();
            _controller = new GenreController(_mockBookstore, _mockMapper, _mockLogger);
        }


        [Fact]
        public async Task GetGenres_ReturnsOkResult()
        {
            var genresEntity = new List<Genres>
            {
                new Genres {Genre_Id = 1, Genre_Name = "art" ,Created_At = DateTime.UtcNow,Updated_At = DateTime.UtcNow,Updated_By="admin",Created_By="admin",Books = []}
            };

            var genresDTO = new List<GenresDTO>
            {
                new GenresDTO {Genre_Id = 1, Genre_Name = "art"}
            };

            A.CallTo(() => _mockBookstore.GetGenresAsync()).Returns(Task.FromResult<IEnumerable<Genres>>(genresEntity));
            A.CallTo(() => _mockMapper.Map<IEnumerable<GenresDTO>>(genresEntity)).Returns(genresDTO);

            //var authorsList = authorsEntity;
            //_mockBookstore.Setup(x => x.GetAuthorsAsync()).ReturnsAsync(authorsEntity);
            //_mockMapper.Setup(m => m.Map<IEnumerable<AuthorsDTO>>(authorsEntity)).Returns(authorsDTO);
            //var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<AuthorProfile>());
            //var mapper = new Mapper(mapperConfiguration);
            // Act
            var result = await _controller.GetGenres(null);

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(genresDTO);

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
        public async Task GetGenresbyName_ReturnsOkResult()
        {
            var genresEntity = new List<Genres>
            {
                new Genres {Genre_Id = 1, Genre_Name = "art" ,Created_At = DateTime.UtcNow,Updated_At = DateTime.UtcNow,Updated_By="admin",Created_By="admin",Books = []}
            };

            var genresDTO = new List<GenresDTO>
            {
                new GenresDTO {Genre_Id = 1, Genre_Name = "art"}
            };

            string search = "a";

            A.CallTo(() => _mockBookstore.GetGenresbyGenrenameAsync(search)).Returns(Task.FromResult<IEnumerable<Genres>>(genresEntity));
            A.CallTo(() => _mockMapper.Map<IEnumerable<GenresDTO>>(genresEntity)).Returns(genresDTO);

            var result = await _controller.GetGenres(search);

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(genresDTO);
        }

        [Fact]
        public async Task GetGenrebyID_ReturnsOkResult()
        {
            var genresEntity = new Genres { Genre_Id = 1, Genre_Name = "art", Created_At = DateTime.UtcNow, Updated_At = DateTime.UtcNow, Updated_By = "admin", Created_By = "admin", Books = [] };

            var genresDTO = new GenresDTO { Genre_Id = 1, Genre_Name = "art" };

            int genre_id = 1;

            A.CallTo(() => _mockBookstore.GetGenreAsync(genre_id)).Returns(Task.FromResult<Genres?>(genresEntity));
            A.CallTo(() => _mockMapper.Map<GenresDTO>(genresEntity)).Returns(genresDTO);

            var result = await _controller.GetGenre(genre_id);

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedDto = okResult.Value.Should().BeOfType<GenresDTO>().Subject;
            returnedDto.Genre_Name.Should().Be(genresEntity.Genre_Name);
        }

        [Fact]
        public async Task GetGenrebyID_NOTFound_ReturnsOkResult()
        {
            var genresEntity = new Genres { Genre_Id = 1, Genre_Name = "art", Created_At = DateTime.UtcNow, Updated_At = DateTime.UtcNow, Updated_By = "admin", Created_By = "admin", Books = [] };

            var genresDTO = new GenresDTO { Genre_Id = 1, Genre_Name = "art" };

            int genre_id = 1;

            A.CallTo(() => _mockBookstore.GetGenreAsync(genre_id)).Returns(Task.FromResult<Genres?>(null));
            A.CallTo(() => _mockMapper.Map<GenresDTO>(genresEntity)).Returns(genresDTO);

            var result = await _controller.GetGenre(genre_id);

            result.Result.Should().BeOfType<NotFoundResult>();

        }
    }
}
