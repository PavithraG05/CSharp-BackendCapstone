using Bookstore.DbContexts;
using Microsoft.EntityFrameworkCore;
using Bookstore.Entities;
using System.Runtime.CompilerServices;

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
            return await _context.Books.Where(c => c.Id == id).Include(c => c.author).Include(c => c.genre).FirstOrDefaultAsync();

        }

        public async Task<Books?> GetAuthorBookAsync(int id)
        {
            return await _context.Books.Where(c => c.Author_Id == id).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Books>> GetBooksAsync()
        {
            return await _context.Books.Include(c=>c.author).Include(c=>c.genre).ToListAsync();

        }

        public async Task<IEnumerable<Books>> GetBooksbyBooknameAsync(string name)
        {
            return await _context.Books.Where(a => a.Title.Contains(name)).Include(c => c.author).Include(c => c.genre).ToListAsync();
        }

        public async Task<IEnumerable<Authors>> GetAuthorsbyAuthornameAsync(string name)
        {
            return await _context.Authors.Where(a => a.Author_Name.Contains(name)).Include(c => c.Books).ToListAsync();
        }

        public async Task<IEnumerable<Genres>> GetGenresbyGenrenameAsync(string name)
        {
            return await _context.Genres.Where(a => a.Genre_Name.Contains(name)).Include(c => c.Books).ToListAsync();
        }
        //public async Task<IEnumerable<Books>> GetBooksbyGenreAsync(int genreid)
        //{
        //    return await _context.Books.Where(c => c.Genre_Id == genreid).ToListAsync();
        //}

        public async Task<IEnumerable<Genres>> GetGenresAsync()
        {
            return await _context.Genres.Include(c=>c.Books).ToListAsync();

        }

        public async Task<Genres?> GetGenreAsync(int id)
        {
            return await _context.Genres.Where(c => c.Genre_Id == id).Include(c => c.Books).FirstOrDefaultAsync();

        }

        public async Task<Genres?> GetGenrebyNameAsync(string name)
        {
            return await _context.Genres.Where(c => c.Genre_Name.Equals(name)).Include(c => c.Books).FirstOrDefaultAsync();

        }

        //public async Task<Genres?> GetGenrebyNameAsync(string? genre)
        //{

        //    genre = genre.Trim();
        //    return await _context.Genres.Where(c => c.Genre_Name == genre).Include(c => c.Books).FirstOrDefaultAsync();
        //}
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

        public async Task UpdateAuthorAsync(int id, Authors author)
        {
            _context.Update(author);
            await _context.SaveChangesAsync();

        }
        public async Task UpdateGenreAsync(int id, Genres genre)
        {
            _context.Update(genre);
            await _context.SaveChangesAsync();

        }
        public async Task UpdateBookAsync(int id, Books book)
        {
            _context.Update(book);
            await _context.SaveChangesAsync();

        }
        public async Task DeleteBookAsync(int id, Books book)
        {
            _context.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(int id, Authors author)
        {
            _context.Remove(author);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        
        
    }
}
