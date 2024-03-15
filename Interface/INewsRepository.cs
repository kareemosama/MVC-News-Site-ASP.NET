using NewsApp.Models;
using System.Diagnostics;

namespace NewsApp.Interface
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllAsync();
        Task<IEnumerable<News>> GetByCategoryIdAsync(int categoryId);

        Task<News> GetByIdAsync(int id);

        Task AddAsync(News news);

        Task UpdateAsync(News news);

        Task DeleteAsync(News news);

    }
}
