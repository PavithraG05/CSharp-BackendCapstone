using Bookstore.Entities;

namespace Bookstore.Services
{
    public interface IBookstoreRepository
    {
        Task<IEnumerable<Books>> GetBooksAsync();
        Task<Books?> GetBookAsync(int id);
        Task<IEnumerable<Authors>> GetAuthorsAsync();
        Task<Authors?> GetAuthorAsync(int id);
        Task<IEnumerable<Genres>> GetGenresAsync();
        Task<Genres?> GetGenreAsync(int id);
        Task<Users?> ValidateUser(string  username, string password);
        Task<Users?> GetUserAsync(string username);
        Task AddAuthorAsync(Authors author);
        Task AddGenreAsync(Genres genre);
        Task AddBookAsync(Books book);
        Task<bool> SaveChangesAsync();
    }
}
