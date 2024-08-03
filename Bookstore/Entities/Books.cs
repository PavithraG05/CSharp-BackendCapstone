using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Entities
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [ForeignKey("Author_Id")]
        public Authors? author {  get; set; }
        public int Author_Id {  get; set; }

        [ForeignKey("Genre_Id")]
        public Genres? genre {  get; set; }
        public int Genre_Id { get; set; }

        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime Publication_Date {  get; set; }
        [Required]
        public string Description {  get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public string Created_By { get; set; }
        public string Updated_By { get; set; }

    }
}
