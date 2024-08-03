using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Entities
{
    public class Genres
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Genre_Id { get; set; }
        [Required]
        public string Genre_Name { get; set; }

        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public string Created_By { get; set; }
        public string Updated_By { get; set; }
        public ICollection<Books> Books { get; set; } = new List<Books>();
    }
}
