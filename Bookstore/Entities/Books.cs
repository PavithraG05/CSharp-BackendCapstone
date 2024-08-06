using Bookstore.Models;
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
        [Required]
        [ForeignKey("Author_Id")]
        public Authors? author {  get; set; }
        public int Author_Id {  get; set; }

        [Required]
        [ForeignKey("Genre_Id")]
        public Genres? genre {  get; set; }
        public int Genre_Id { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Publication date is required.")]
        public DateTime Publication_Date {  get; set; }

        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public string Created_By { get; set; }
        public string Updated_By { get; set; }

        [Required]
        public string Description { get; set; }


    }
}
