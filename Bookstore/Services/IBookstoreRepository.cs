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
        

    }
}
