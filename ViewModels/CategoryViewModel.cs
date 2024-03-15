using NewsApp.Models;

namespace NewsApp.ViewModels
{
    public class CategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Category NewCategory { get; set; }
    }
}
