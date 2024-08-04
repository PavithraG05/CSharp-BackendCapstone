namespace Bookstore.Models
{
    public class AuthorsDTO
    {
        public int Author_Id { get; set; }
        public string Author_Name { get; set; }
        public string Biography { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public string? Created_By { get; set; }
        public string? Updated_By { get; set; }
        public ICollection<BooksDTO> books { get; set; } = new List<BooksDTO>();
    }
}
