using Bookstore.Entities;

namespace Bookstore.Services
{
    public interface IBookstoreRepository
    {
        Task<IEnumerable<Books>> GetBooksAsync();
        Task<Books?> GetBookAsync(int id);
        Task<Books?> GetAuthorBookAsync(int id);
        //Task<IEnumerable<Books>> GetBooksbyGenreAsync(int genreid);
        Task<IEnumerable<Books>> GetBooksbyBooknameAsync(string name);
        Task<IEnumerable<Authors>> GetAuthorsbyAuthornameAsync(string name);
        Task<IEnumerable<Genres>> GetGenresbyGenrenameAsync(string name);
        Task<IEnumerable<Authors>> GetAuthorsAsync();
        Task<Authors?> GetAuthorAsync(int id);
        Task<IEnumerable<Genres>> GetGenresAsync();
        Task<Genres?> GetGenreAsync(int id);
        Task<Genres?> GetGenrebyNameAsync(string name);
        //Task<Genres?> GetGenrebyNameAsync(string? name);
        Task<Users?> ValidateUser(string  username, string password);
        Task<Users?> GetUserAsync(string username);
        Task AddAuthorAsync(Authors author);
        Task AddGenreAsync(Genres genre);
        Task AddBookAsync(Books book);
        Task UpdateAuthorAsync(int id, Authors author);
        Task UpdateGenreAsync(int id, Genres genre);
        Task UpdateBookAsync(int id, Books book);
        Task DeleteBookAsync(int id, Books book);
        Task DeleteAuthorAsync(int id, Authors author);


        Task<bool> SaveChangesAsync();
    }
}
