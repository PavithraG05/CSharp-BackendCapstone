using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace BookStoreAPITest
{
    public class BookTest
    {

        private readonly HttpClient _client;
        private const string AuthToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidW5pcXVlX25hbWUiOiJhZG1pbiIsImdpdmVuX25hbWUiOiJQYXZpdGhyYSIsImZhbWlseV9uYW1lIjoiRyIsImVtYWlsIjoiUGF2aWthbm5hbkBnbWFpbC5jb20iLCJuYmYiOjE3MjMxODA1MTYsImV4cCI6MTcyMzE4NzcxNiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyNiIsImF1ZCI6ImJvb2tzdG9yZWFwaSJ9.X9_w_EEdUO1WKc9fylsd3ytUJ3-j31ZP-v_hcC5BDxM";
        public BookTest()
        {
            _client = new HttpClient
            {
                BaseAddress = new System.Uri("https://localhost:7226/api/v1/")
            };

            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthToken);
        }


        [Fact]
        public async Task Test_GetAllBooks_Endpoint_Returns_Success_200()
        {
            // Arrange
            var requestUri = "books";

            // Act
            var response = await _client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();


            var json = JArray.Parse(content);
            Assert.NotEmpty(json);

            var firstBook = json.First as JObject;
            Assert.NotNull(firstBook);
            Assert.True(firstBook.ContainsKey("id"));
            Assert.True(firstBook.ContainsKey("title"));
            Assert.True(firstBook.ContainsKey("price"));
            Assert.True(firstBook.ContainsKey("author_Id"));
            Assert.True(firstBook.ContainsKey("genre_Id"));
            Assert.True(firstBook.ContainsKey("description"));
            Assert.True(firstBook.ContainsKey("publication_Date"));



            Assert.Equal(1, (int)firstBook["id"]);
            Assert.Equal("A Little Princess", (string)firstBook["title"]);
            Assert.Equal(649.0, (double)firstBook["price"]);
            Assert.Equal(1, (int)firstBook["author_Id"]);
            Assert.Equal(2, (int)firstBook["genre_Id"]);
        }


        [Fact]
        public async Task Test_GetBookbyID_Endpoint_Returns_Success_200()
        {
            // Arrange
            var bookId = 1;
            var requestUri = $"books/{bookId}";

            // Act
            var response = await _client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();


            var json = JObject.Parse(content);
            Assert.NotEmpty(json);


            Assert.True(json.ContainsKey("id"));
            Assert.True(json.ContainsKey("title"));
            Assert.True(json.ContainsKey("price"));
            Assert.True(json.ContainsKey("author_Id"));
            Assert.True(json.ContainsKey("genre_Id"));
            Assert.True(json.ContainsKey("description"));
            Assert.True(json.ContainsKey("publication_Date"));



            Assert.Equal(1, (int)json["id"]);
            Assert.Equal("A Little Princess", (string)json["title"]);
            Assert.Equal(649.0, (double)json["price"]);
            Assert.Equal(1, (int)json["author_Id"]);
            Assert.Equal(2, (int)json["genre_Id"]);
        }

        [Fact]
        public async Task Test_GetBookbyID_BookIDNotFound_Endpoint_404()
        {
            var bookId = 1999;
            var requestUri = $"books/{bookId}";

            var response = await _client.GetAsync(requestUri);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Test_Add_Book_Endpoint_Success_200()
        {
            // Arrange
            var newBook = new
            {
                title="The lone time",
                price=450,
                publication_date="2023-07-05",
                author_id=18,
                genre_id=14,
                description="xyz"
            };

            var requestUri = "books";
            var content = new StringContent(
                JsonConvert.SerializeObject(newBook),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await _client.PostAsync(requestUri, content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();


            var createdBook = JsonConvert.DeserializeObject<dynamic>(responseContent);

            Assert.NotNull(createdBook);
            Assert.Equal(newBook.title, (string)createdBook.title);
            Assert.Equal(newBook.price, (int)createdBook.price);
        }

        [Fact]
        public async Task Test_Add_Book_BadRequest_400()
        {
            // Arrange
            var newBook = new
            {
                price = 450,
                publication_date = "2023-07-05",
                author_id = 18,
                genre_id = 14,
                description = "xyz"
            };

            var requestUri = "books";

            var content = new StringContent(
                JsonConvert.SerializeObject(newBook),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync(requestUri, content);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Fact]
        public async Task Test_Add_Book_Conflict_409()
        {
            // Arrange
            var newBook = new
            {
                title = "Ratan Tata - A Complete Biography",
                price = 150,
                publication_date = "2021-11-18",
                author_id = 2,
                genre_id = 6,
                description = "xyz"
            };

            var requestUri = "authors";

            var content = new StringContent(
                JsonConvert.SerializeObject(newBook),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync(requestUri, content);

            Assert.Equal(System.Net.HttpStatusCode.Conflict, response.StatusCode);

        }

        [Fact]
        public async Task Test_Update_Book_Endpoint_Success_200()
        {
            // Arrange

            var newBook = new
            {
                title = "The Hidden Realm",
                price = 750,
                publication_date = "2023-05-15",
                author_id = 3,
                genre_id = 4,
                description = "xyz"
            };

            var BookId = 3;
            var requestUri = $"books/{BookId}";
            var content = new StringContent(
                JsonConvert.SerializeObject(newBook),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await _client.PutAsync(requestUri, content);

            // Assert
            response.EnsureSuccessStatusCode();

        }

        [Fact]
        public async Task Test_UpdateBookbyID_BookIDNotFound_Endpoint_404()
        {
            var bookId = 1999;
            var requestUri = $"books/{bookId}";
            var newBook = new
            {
                title = "The Hidden Realm",
                price = 750,
                publication_date = "2023-05-15",
                author_id = 3,
                genre_id = 4,
                description = "xyz"
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(newBook),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PutAsync(requestUri, content);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Test_Update_Book_MissingField_400()
        {
            // Arrange

            var newBook = new
            {
                price = 750,
                publication_date = "2023-05-15",
                author_id = 3,
                genre_id = 4,
                description = "xyz"
            };

            var bookId = 31;
            var requestUri = $"authors/{bookId}";
            var content = new StringContent(
                JsonConvert.SerializeObject(newBook),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await _client.PutAsync(requestUri, content);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Fact]
        public async Task Test_Update_Book_Conflict_409()
        {
            // Arrange
            var newBook = new
            {
                title = "Ratan Tata - A Complete Biography",
                price = 150,
                publication_date = "2021-11-18",
                author_id = 2,
                genre_id = 6,
                description = "xyz"
            };

            var bookId = 31;
            var requestUri = $"books/{bookId}";
            var content = new StringContent(
                JsonConvert.SerializeObject(newBook),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await _client.PutAsync(requestUri, content);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Conflict, response.StatusCode);

        }

        [Fact]
        public async Task Test_Delete_Book_200()
        {
            // Arrange
            var bookId = 39;
            var requestUri = $"books/{bookId}";

            // Act
            var response = await _client.DeleteAsync(requestUri);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);

        }

        [Fact]
        public async Task Test_Delete_Book_IDNotFound_404()
        {
            // Arrange
            var Id = 399;
            var requestUri = $"books/{Id}";

            // Act
            var response = await _client.DeleteAsync(requestUri);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}

