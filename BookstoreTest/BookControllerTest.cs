using Moq;
using Xunit;
using Bookstore.Controllers;
using Bookstore.Services; // Adjust based on your actual namespace
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Bookstore.Models; // Adjust based on your actual namespace
using Bookstore.Entities;

public class BookControllerTests
{
    private readonly BookController _controller;
    private readonly Mock<IBookstoreRepository> _mockBookstore;
    private readonly Mock<ILogger<BookController>> _mockLogger;
    private readonly Mock<IMapper> _mockMapper;
    public BookControllerTests()
    {
        _mockBookstore = new Mock<IBookstoreRepository>();
        _mockLogger = new Mock<ILogger<BookController>>();
        _mockMapper = new Mock<IMapper>();
        _controller = new BookController(_mockBookstore.Object,_mockMapper.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetBooks_ReturnsOkResult_WithBooksList_WhenNameIsNull()
    {
        // Arrange
        //var books = new List<BooksViewDTO>
        //{
        //    new BooksViewDTO { Id = 1, Title = "Book1", Price = 10, Publication_Date = DateTime.Now, Description = "Desc1", Author_Id = 1, Genre_Id = 1, Updated_At = DateTime.Now, Created_At = DateTime.Now, Updated_By = "admin", Created_By = "admin", Author = new AuthorsViewDTO { Author_Id = 1, Author_Name = "Author1", Biography = "Bio1" }, Genre = new GenresViewDTO { Genre_Id = 1, Genre_Name = "Genre1" } }
        //};

        _mockBookstore.Setup(x => x.GetBooksAsync()).ReturnsAsync((IEnumerable<Books>)null);

        // Act
        var result = await _controller.GetBooks(null);
        var returnValue = result?.Value as List<BooksViewDTO>;

        // Assert
        Assert.NotNull(result);
        //Assert.Equal(200, result.StatusCode);
        Assert.NotNull(returnValue);
        Assert.Single(returnValue);
        Assert.Equal("A Little Princess", returnValue.First().Title);
    }

    //[Fact]
    //public async Task GetBooks_ReturnsNotFound_WhenBooksNotFoundByName()
    //{
    //    // Arrange
    //    string bookName = "NonExistingBook";
    //    _mockBookstore.Setup(x => x.GetBooksbyBooknameAsync(bookName)).ReturnsAsync((IEnumerable<Book>)null);

    //    // Act
    //    var result = await _controller.GetBooks(bookName) as NotFoundResult;

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.Equal(404, result.StatusCode);
    //}

    //[Fact]
    //public async Task GetBooks_ReturnsOkResult_WithBooksList_WhenNameIsProvided()
    //{
    //    // Arrange
    //    string bookName = "Book1";
    //    var books = new List<Book>
    //    {
    //        new Book { Id = 1, Title = "Book1", Price = 10, Publication_Date = DateTime.Now, Description = "Desc1", Author_Id = 1, Genre_Id = 1, Updated_At = DateTime.Now, Created_At = DateTime.Now, Updated_By = "User1", Created_By = "User1", author = new Author { Author_Id = 1, Author_Name = "Author1", Biography = "Bio1" }, genre = new Genre { Genre_Id = 1, Genre_Name = "Genre1" } }
    //    };

    //    _mockBookstore.Setup(x => x.GetBooksbyBooknameAsync(bookName)).ReturnsAsync(books);

    //    // Act
    //    var result = await _controller.GetBooks(bookName) as OkObjectResult;
    //    var returnValue = result?.Value as List<BooksViewDTO>;

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.Equal(200, result.StatusCode);
    //    Assert.NotNull(returnValue);
    //    Assert.Single(returnValue);
    //    Assert.Equal("Book1", returnValue.First().Title);
    //}

    //[Fact]
    //public async Task GetBooks_ReturnsInternalServerError_WhenExceptionIsThrown()
    //{
    //    // Arrange
    //    _mockBookstore.Setup(x => x.GetBooksAsync()).ThrowsAsync(new Exception("Test exception"));

    //    // Act
    //    var result = await _controller.GetBooks(null) as ObjectResult;

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.Equal(500, result.StatusCode);
    //}
}
