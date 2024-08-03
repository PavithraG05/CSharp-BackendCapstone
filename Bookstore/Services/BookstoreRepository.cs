using Bookstore.DbContexts;
using Microsoft.EntityFrameworkCore;
using Bookstore.Entities;

namespace Bookstore.Services
{
    public class BookstoreRepository : IBookstoreRepository
    {
        private readonly BookstoreContext _context;

        public BookstoreRepository(BookstoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Authors>> GetAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Authors?> GetAuthorAsync(int id)
        {
            return await _context.Authors.Where(c => c.Author_Id == id).FirstOrDefaultAsync();
        }

        public async Task<Books?> GetBookAsync(int id)
        {
            return await _context.Books.Where(c => c.Id == id).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Books>> GetBooksAsync()
        {
            return await _context.Books.ToListAsync();

        }

        public async Task<IEnumerable<Genres>> GetGenresAsync()
        {
            return await _context.Genres.ToListAsync();

        }

        public async Task<Genres?> GetGenreAsync(int id)
        {
            return await _context.Genres.Where(c => c.Genre_Id == id).FirstOrDefaultAsync();

        }
    }
}
