using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Bookstore.Models; // Adjust based on your actual namespace
using Bookstore.Entities;
using Moq;
using Xunit;
using Bookstore.Controllers;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Profiles;

namespace BookstoreTest
{
    public class AuthorControllerTest
    {
        private readonly AuthorController _controller;
        private readonly Mock<IBookstoreRepository> _mockBookstore;
        private readonly Mock<ILogger<AuthorController>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        public AuthorControllerTest()
        {
            _mockBookstore = new Mock<IBookstoreRepository>();
            _mockLogger = new Mock<ILogger<AuthorController>>();
            _mockMapper = new Mock<IMapper>();
            _controller = new AuthorController(_mockBookstore.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAuthors_ReturnsOkResult()
        {
            // Arrange
            //var books = new List<BooksViewDTO>
            //{
            //    new BooksViewDTO { Id = 1, Title = "Book1", Price = 10, Publication_Date = DateTime.Now, Description = "Desc1", Author_Id = 1, Genre_Id = 1, Updated_At = DateTime.Now, Created_At = DateTime.Now, Updated_By = "admin", Created_By = "admin", Author = new AuthorsViewDTO { Author_Id = 1, Author_Name = "Author1", Biography = "Bio1" }, Genre = new GenresViewDTO { Genre_Id = 1, Genre_Name = "Genre1" } }
            //};

            _mockBookstore.Setup(x => x.GetAuthorsAsync()).ReturnsAsync((IEnumerable<Authors>)null);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<AuthorProfile>());
            var mapper = new Mapper(mapperConfiguration);
            // Act
            var result = await _controller.GetAuthors(null);
            Assert.IsType<OkObjectResult>(result);
            var returnValue = result?.Value as List<AuthorsDTO>;

            // Assert
            Assert.NotNull(result);
            //Assert.Equal(200, result.StatusCode);
            Assert.NotNull(returnValue);
            Assert.Single(returnValue);
            Assert.Equal("Nancy Bond", returnValue.First().Author_Name);
        }
    }
}
