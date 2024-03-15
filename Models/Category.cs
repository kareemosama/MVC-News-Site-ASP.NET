using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        public string Name { get; set; }
    }
}
