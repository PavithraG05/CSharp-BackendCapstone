//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Xunit;
//using Newtonsoft.Json.Linq;

//namespace BookStoreAPITest
//{
//    public class AuthorTest
//    {
        
//        private readonly HttpClient _client;
//        private const string AuthToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidW5pcXVlX25hbWUiOiJhZG1pbiIsImdpdmVuX25hbWUiOiJQYXZpdGhyYSIsImZhbWlseV9uYW1lIjoiRyIsImVtYWlsIjoiUGF2aWthbm5hbkBnbWFpbC5jb20iLCJuYmYiOjE3MjM0NTkzODIsImV4cCI6MTcyMzQ2NjU4MiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyNiIsImF1ZCI6ImJvb2tzdG9yZWFwaSJ9.4rCRIu7ajVgPceFFvZ4eTKsUgE7q2EPaT4fiR2HwziM";
//        public AuthorTest()
//        {
//            _client = new HttpClient
//            {
//                BaseAddress = new System.Uri("https://localhost:7226/api/v1/")
//            };

//            _client.DefaultRequestHeaders.Authorization =
//                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthToken);
//        }


//        [Fact]
//        public async Task Test_GetAllAuthors_Endpoint_Returns_Success_200()
//        {
//            // Arrange
//            var requestUri = "authors";

//            // Act
//            var response = await _client.GetAsync(requestUri);

//            response.EnsureSuccessStatusCode(); 

//            var content = await response.Content.ReadAsStringAsync();

            
//            var json = JArray.Parse(content);
//            Assert.NotEmpty(json); 

//            var firstAuthor = json.First as JObject;
//            Assert.NotNull(firstAuthor);
//            Assert.True(firstAuthor.ContainsKey("author_Id"));
//            Assert.True(firstAuthor.ContainsKey("author_Name"));
//            Assert.True(firstAuthor.ContainsKey("biography"));

//            Assert.Equal(1, (int)firstAuthor["author_Id"]);
//            Assert.Equal("Nancy Bond", (string)firstAuthor["author_Name"]);
//            Assert.Equal("Nancy Bond is a prolific fantasy writer known for her richly imagined worlds, complex characters, and epic narratives that captivate readers of all ages. Her ability to blend elements of magic and adventure has made her a favorite among fantasy enthusiasts worldwide.", (string)firstAuthor["biography"]);
//        }


//        [Fact]
//        public async Task Test_GetAuthorbyID_Endpoint_Returns_Success_200()
//        {
//            // Arrange
//            var authorId = 1;
//            var requestUri = $"authors/{authorId}";

//            // Act
//            var response = await _client.GetAsync(requestUri);

//            response.EnsureSuccessStatusCode();

//            var content = await response.Content.ReadAsStringAsync();


//            var json = JObject.Parse(content);
//            Assert.NotEmpty(json);

            
//            Assert.True(json.ContainsKey("author_Id"));
//            Assert.True(json.ContainsKey("author_Name"));
//            Assert.True(json.ContainsKey("biography"));

//            Assert.Equal(1, (int)json["author_Id"]);
//            Assert.Equal("Nancy Bond", (string)json["author_Name"]);
//            Assert.Equal("Nancy Bond is a prolific fantasy writer known for her richly imagined worlds, complex characters, and epic narratives that captivate readers of all ages. Her ability to blend elements of magic and adventure has made her a favorite among fantasy enthusiasts worldwide.", (string)json["biography"]);
//        }

//        [Fact]
//        public async Task Test_GetAuthorbyID_AuthorIDNotFound_Endpoint_404()
//        {
//            var authorId = 1999;
//            var requestUri = $"authors/{authorId}";

//            var response = await _client.GetAsync(requestUri);

//            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
//        }

//        [Fact]
//        public async Task Test_Add_Author_Endpoint_Success_200()
//        {
//            // Arrange
//            var newAuthor = new
//            {
//                author_name = "New Author4",
//                biography = "This is a biography of the new author4."
//            };

//            var requestUri = "authors";
//            var content = new StringContent(
//                JsonConvert.SerializeObject(newAuthor),
//                Encoding.UTF8,
//                "application/json");

//            // Act
//            var response = await _client.PostAsync(requestUri, content);

//            // Assert
//            response.EnsureSuccessStatusCode();
//            var responseContent = await response.Content.ReadAsStringAsync();


//            var createdAuthor = JsonConvert.DeserializeObject<dynamic>(responseContent);

//            Assert.NotNull(createdAuthor);
//            Assert.Equal(newAuthor.author_name, (string)createdAuthor.author_Name);
//            Assert.Equal(newAuthor.biography, (string)createdAuthor.biography);
//        }

//        [Fact]
//        public async Task Test_Add_Author_BadRequest_400()
//        {
//            // Arrange
//            var newAuthor = new
//            {
//                biography = "This is a biography of the new author."
//            };

//            var requestUri = "authors";

//            var content = new StringContent(
//                JsonConvert.SerializeObject(newAuthor),
//                Encoding.UTF8,
//                "application/json");

//            var response = await _client.PostAsync(requestUri, content);

//            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
           
//        }

//        [Fact]
//        public async Task Test_Add_Author_Conflict_409()
//        {
//            // Arrange
//            var newAuthor = new
//            {
//                author_name = "Sophia Clark",
//                biography = "Sophia Clark explores the mysteries of quantum physics and the implications of scientific discovery on humanity's understanding of reality.Her speculative fiction challenges readers to ponder the philosophical and ethical implications of technological advancements."
//            };

//            var requestUri = "authors";

//            var content = new StringContent(
//                JsonConvert.SerializeObject(newAuthor),
//                Encoding.UTF8,
//                "application/json");

//            var response = await _client.PostAsync(requestUri, content);

//            Assert.Equal(System.Net.HttpStatusCode.Conflict, response.StatusCode);

//        }

//        [Fact]
//        public async Task Test_Update_Author_Endpoint_Success_200()
//        {
//            // Arrange

//            var newAuthor = new
//            {
//                author_name = "Olivia Green",
//                biography = "Author Olivia Green writes gripping mysteries set in exotic locations, blending suspenseful plots with rich cultural details and compelling characters. Her ability to create atmospheric settings and intricate puzzles has earned her a reputation as a master of the mystery genre."
//            };

//            var authorId = 14;
//            var requestUri = $"authors/{authorId}";
//            var content = new StringContent(
//                JsonConvert.SerializeObject(newAuthor),
//                Encoding.UTF8,
//                "application/json");

//            // Act
//            var response = await _client.PutAsync(requestUri, content);

//            // Assert
//            response.EnsureSuccessStatusCode();
            
//        }

//        [Fact]
//        public async Task Test_UpdateAuthorbyID_AuthorIDNotFound_Endpoint_404()
//        {
//            var authorId = 1999;
//            var requestUri = $"authors/{authorId}";
//            var newAuthor = new
//            {
//                author_name = "Olivia Green",
//                biography = "Author Olivia Green writes gripping mysteries set in exotic locations, blending suspenseful plots with rich cultural details and compelling characters. Her ability to create atmospheric settings and intricate puzzles has earned her a reputation as a master of the mystery genre."
//            };

//            var content = new StringContent(
//                JsonConvert.SerializeObject(newAuthor),
//                Encoding.UTF8,
//                "application/json");

//            var response = await _client.PutAsync(requestUri,content);

//            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
//        }

//        [Fact]
//        public async Task Test_Update_Author_MissingField_400()
//        {
//            // Arrange

//            var newAuthor = new
//            {
//                biography = "Author Olivia Green writes gripping mysteries set in exotic locations, blending suspenseful plots with rich cultural details and compelling characters. Her ability to create atmospheric settings and intricate puzzles has earned her a reputation as a master of the mystery genre."
//            };

//            var authorId = 14;
//            var requestUri = $"authors/{authorId}";
//            var content = new StringContent(
//                JsonConvert.SerializeObject(newAuthor),
//                Encoding.UTF8,
//                "application/json");

//            // Act
//            var response = await _client.PutAsync(requestUri, content);

//            // Assert
//            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);

//        }

//        [Fact]
//        public async Task Test_Update_Author_Conflict_409()
//        {
//            // Arrange

//            var newAuthor = new
//            {
//                author_name = "Matthew Lee",
//                biography = "Matthew Lee's horror novels keep readers on the edge of their seats with chilling suspense, supernatural phenomena, and spine-tingling plot twists. His atmospheric settings and vivid descriptions create an immersive reading experience for fans of horror and suspense fiction."
//            };

//            var authorId = 14;
//            var requestUri = $"authors/{authorId}";
//            var content = new StringContent(
//                JsonConvert.SerializeObject(newAuthor),
//                Encoding.UTF8,
//                "application/json");

//            // Act
//            var response = await _client.PutAsync(requestUri, content);

//            // Assert
//            Assert.Equal(System.Net.HttpStatusCode.Conflict, response.StatusCode);

//        }

//        [Fact]
//        public async Task Test_Delete_Author_HasNoBooks_200()
//        {
//            // Arrange
//            var authorId = 33;
//            var requestUri = $"authors/{authorId}";

//            // Act
//            var response = await _client.DeleteAsync(requestUri);

//            // Assert
//            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);

//        }

//        [Fact]
//        public async Task Test_Delete_Author_HasBooks_403()
//        {
//            // Arrange
//            var authorId = 15;
//            var requestUri = $"authors/{authorId}";

//            // Act
//            var response = await _client.DeleteAsync(requestUri);

//            // Assert
//            Assert.Equal(System.Net.HttpStatusCode.Forbidden, response.StatusCode);

//        }

//        [Fact]
//        public async Task Test_Delete_Author_IDNotFound_404()
//        {
//            // Arrange
//            var authorId = 399;
//            var requestUri = $"authors/{authorId}";

//            // Act
//            var response = await _client.DeleteAsync(requestUri);

//            // Assert
//            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);

//        }
//    }
//}

