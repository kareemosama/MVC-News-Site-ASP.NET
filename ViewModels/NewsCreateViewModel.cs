using System.ComponentModel.DataAnnotations;

namespace NewsApp.ViewModels
{
    public class NewCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "News Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "News Body is required")]
        public string Body { get; set; }
        public DateTime NewsDate { get; set; } = DateTime.Now;

        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }  // Property for the image file upload

      
        [Required(ErrorMessage = "News Category is required")]
        public int CategoryId { get; set; }
    }
}
