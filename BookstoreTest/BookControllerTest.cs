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
    public class BookControllerTest
    {
        private readonly BookController _controller;
        private readonly IBookstoreRepository _mockBookstore;
        private readonly ILogger<BookController> _mockLogger;
        private readonly IMapper _mockMapper;
        //private readonly IMapper _mockMapper;

        public BookControllerTest()
        {
            _mockBookstore = A.Fake<IBookstoreRepository>();
            _mockLogger = A.Fake<ILogger<BookController>>();
            _mockMapper = A.Fake<IMapper>();

            //var _mockMapperCfg = new MapperConfiguration(cfg => cfg.CreateMap<Books, BooksDTO>());
            //_mockMapper = _mockMapperCfg.CreateMapper();
            _controller = new BookController(_mockBookstore, _mockMapper, _mockLogger);
        }

        //[Fact]
        //public async Task GetBookById_ReturnsOkResult()
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
        public async Task GetBooks_ReturnsOkResult()
        {
            var BooksEntity = new List<Books>
            {
                new Books {Id = 1, Title = "Book1", Price = 100.00, Publication_Date=new DateTime(2022-02-20),Description="xyz",Author_Id = 1,Genre_Id=1,Created_At = new DateTime(2022-02-20),Updated_At = new DateTime(2022-02-20),Updated_By="admin",Created_By="admin", author = new Authors{ Author_Id = 1, Author_Name = "zas", Biography = "bio" }, genre= new Genres{Genre_Id = 1, Genre_Name = "art" } }

            };

            var booksDTO = new List<BooksViewDTO>
            {
                new BooksViewDTO {Id = 1, Title = "Book1", Price = 100.00, Publication_Date=new DateTime(2022-02-20),Description="xyz",Author_Id = 1,Genre_Id=1, Created_At = new DateTime(2022-02-20),Updated_At =new DateTime(2022-02-20),Updated_By="admin",Created_By="admin", Author = new AuthorsViewDTO{ Author_Id = 1, Author_Name = "zas", Biography = "bio" }, Genre = new GenresViewDTO{Genre_Id = 1, Genre_Name = "art" }}
            };

            A.CallTo(() => _mockBookstore.GetBooksAsync()).Returns(Task.FromResult<IEnumerable<Books>>(BooksEntity));
            A.CallTo(() => _mockMapper.Map<IEnumerable<BooksViewDTO>>(BooksEntity)).Returns(booksDTO);

            //var authorsList = authorsEntity;
            //_mockBookstore.Setup(x => x.GetAuthorsAsync()).ReturnsAsync(authorsEntity);
            //_mockMapper.Setup(m => m.Map<IEnumerable<AuthorsDTO>>(authorsEntity)).Returns(authorsDTO);
            //var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<AuthorProfile>());
            //var mapper = new Mapper(mapperConfiguration);
            // Act
            var result = await _controller.GetBooks(null);

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(booksDTO);

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
        public async Task GetBooksbyName_ReturnsOkResult()
        {
            var BooksEntity = new List<Books>
            {
                new Books {Id = 1, Title = "Book1", Price = 100.00, Publication_Date=new DateTime(2022-02-20),Description="xyz",Author_Id = 1,Genre_Id=1,Created_At = new DateTime(2022-02-20),Updated_At = new DateTime(2022-02-20),Updated_By="admin",Created_By="admin", author = new Authors{ Author_Id = 1, Author_Name = "zas", Biography = "bio" }, genre= new Genres{Genre_Id = 1, Genre_Name = "art" } }
            };

            var booksDTO = new List<BooksViewDTO>
            {
                new BooksViewDTO {Id = 1, Title = "Book1", Price = 100.00, Publication_Date=new DateTime(2022-02-20),Description="xyz",Author_Id = 1,Genre_Id=1, Created_At = new DateTime(2022-02-20),Updated_At =new DateTime(2022-02-20),Updated_By="admin",Created_By="admin", Author = new AuthorsViewDTO{ Author_Id = 1, Author_Name = "zas", Biography = "bio" }, Genre = new GenresViewDTO{Genre_Id = 1, Genre_Name = "art" }}
            };

            string search = "Bo";

            A.CallTo(() => _mockBookstore.GetBooksbyBooknameAsync(search)).Returns(Task.FromResult<IEnumerable<Books>>(BooksEntity));
            A.CallTo(() => _mockMapper.Map<IEnumerable<BooksViewDTO>>(BooksEntity)).Returns(booksDTO);

            var result = await _controller.GetBooks(search);

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(booksDTO);
            //var returnedDto = okResult.Value.Should().BeOfType<BooksViewDTO>().Subject;
            //returnedDto.Title.Should().Be(BooksEntity[0].Title);

        }

        [Fact]
        public async Task GetBookbyID_ReturnsOkResult()
        {
            var booksEntity = new Books { Id = 1, Title = "Book1", Price = 100.00, Publication_Date = new DateTime(2022 - 02 - 20), Description = "xyz", Author_Id = 1, Genre_Id = 1, Created_At = new DateTime(2022 - 02 - 20), Updated_At = new DateTime(2022 - 02 - 20), Updated_By = "admin", Created_By = "admin", author = new Authors { Author_Id = 1, Author_Name = "zas", Biography = "bio" }, genre = new Genres { Genre_Id = 1, Genre_Name = "art" } };

            var booksDTO = new BooksViewDTO { Id = 1, Title = "Book1", Price = 100.00, Publication_Date = new DateTime(2022 - 02 - 20), Description = "xyz", Author_Id = 1, Genre_Id = 1, Created_At = new DateTime(2022 - 02 - 20), Updated_At = new DateTime(2022 - 02 - 20), Updated_By = "admin", Created_By = "admin", Author = new AuthorsViewDTO { Author_Id = 1, Author_Name = "zas", Biography = "bio" }, Genre = new GenresViewDTO { Genre_Id = 1, Genre_Name = "art" } };

            int book_id = 1;

            A.CallTo(() => _mockBookstore.GetBookAsync(book_id)).Returns(Task.FromResult<Books?>(booksEntity));
            A.CallTo(() => _mockMapper.Map<BooksViewDTO>(booksEntity)).Returns(booksDTO);

            var result = await _controller.GetBook(book_id);

            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(booksDTO);

            //var returnedDto = okResult.Value.Should().BeOfType<BooksViewDTO>().Subject;
            //returnedDto.Title.Should().Be(booksEntity.Title);
        }

        [Fact]
        public async Task GetBookbyID_NOTFound()
        {
            var booksEntity = new Books { Id = 1, Title = "Book1", Price = 100.00, Publication_Date = new DateTime(2022 - 02 - 20), Description = "xyz", Author_Id = 1, Genre_Id = 1, Created_At = new DateTime(2022 - 02 - 20), Updated_At = new DateTime(2022 - 02 - 20), Updated_By = "admin", Created_By = "admin", author = new Authors { Author_Id = 1, Author_Name = "zas", Biography = "bio" }, genre = new Genres { Genre_Id = 1, Genre_Name = "art" } };

            var booksDTO = new BooksViewDTO { Id = 1, Title = "Book1", Price = 100.00, Publication_Date = new DateTime(2022 - 02 - 20), Description = "xyz", Author_Id = 1, Genre_Id = 1, Created_At = new DateTime(2022 - 02 - 20), Updated_At = new DateTime(2022 - 02 - 20), Updated_By = "admin", Created_By = "admin", Author = new AuthorsViewDTO { Author_Id = 1, Author_Name = "zas", Biography = "bio" }, Genre = new GenresViewDTO { Genre_Id = 1, Genre_Name = "art" } };

            int book_id = 1000;

            A.CallTo(() => _mockBookstore.GetBookAsync(book_id)).Returns(Task.FromResult<Books?>(null));
            A.CallTo(() => _mockMapper.Map<BooksViewDTO>(booksEntity)).Returns(booksDTO);

            var result = await _controller.GetBook(book_id);

            result.Result.Should().BeOfType<NotFoundResult>();

        }
    }
}
