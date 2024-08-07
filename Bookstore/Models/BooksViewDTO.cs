namespace Bookstore.Models
{
    public class BooksViewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Author_Id { get; set; }
        public int Genre_Id { get; set; }
        public double Price { get; set; }
        public DateTime Publication_Date { get; set; }
        public string Description { get; set; }

        public AuthorsViewDTO? Author { get; set; }
        public GenresViewDTO? Genre { get; set; }

        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public string? Created_By { get; set; }
        public string? Updated_By { get; set; }
    }
}
