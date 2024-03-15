using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsApp.Models
{
    public class News
    {
        
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "News Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "News Body is required")]
        public string Body { get; set; }
        public DateTime NewsDate { get; set; }= DateTime.Now;
        public string? Image { get; set; }

        [Required(ErrorMessage = "News Category is required")]
        public int CategoryId { get; set; }

        
        public Category Category { get; set; }
    }
}
