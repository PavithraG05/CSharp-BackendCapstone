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
            return await _context.Authors.Include(c => c.Books).ToListAsync();
        }

        public async Task<Authors?> GetAuthorAsync(int id)
        {
            return await _context.Authors.Where(c => c.Author_Id == id).Include(c => c.Books).FirstOrDefaultAsync();
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
            return await _context.Genres.Include(c=>c.Books).ToListAsync();

        }

        public async Task<Genres?> GetGenreAsync(int id)
        {
            return await _context.Genres.Where(c => c.Genre_Id == id).Include(c => c.Books).FirstOrDefaultAsync();

        }

        public async Task<Users?> ValidateUser(string userName, string password)
        {
            return await _context.Users.Where(c=> c.Username == userName).FirstOrDefaultAsync();
        }

        public async Task<Users?> GetUserAsync(string username)
        {
            return await _context.Users.Where(c => c.Username == username).FirstOrDefaultAsync();
        }

        public async Task AddAuthorAsync(Authors author)
        {
            _context.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task AddGenreAsync(Genres genre)
        {
            _context.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task AddBookAsync(Books book)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
