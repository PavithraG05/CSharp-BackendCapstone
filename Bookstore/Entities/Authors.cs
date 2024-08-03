using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Entities
{
    public class Authors
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Author_Id { get; set; }
        [Required]
        public string Author_Name { get; set; }
        [Required]
        public string Biography { get; set; }
        public DateTime Created_At {  get; set; }
        public DateTime Updated_At { get; set; }
        public string Created_By {  get; set; }
        public string Updated_By {  get; set; }
        public ICollection<Books> Books { get; set; } = new List<Books>();
    }
}
