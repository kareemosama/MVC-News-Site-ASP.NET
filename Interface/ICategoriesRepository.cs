using NewsApp.Models;
using System.Threading.Tasks;

namespace NewsApp.Interface
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category> GetByCategoryAsync(Category category);
        Task<Category> GetByIdAsync(int id);

        Task AddAsync(Category category);

       

        Task DeleteAsync(Category category);

       
    }
}
