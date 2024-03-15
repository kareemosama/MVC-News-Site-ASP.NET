using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Interface;
using NewsApp.Models;

namespace NewsApp.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _context;
        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(News news)
        {
             await _context.AddAsync(news);
            await  _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(News news)
        {
           
            _context.Remove(news);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<News> GetByIdAsync(int id)
        {
            return await _context.News.FindAsync(id);
        }

        public async Task<IEnumerable<News>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.News.Where(News => News.CategoryId == categoryId).ToListAsync();
        }


        public async Task UpdateAsync(News news)
        {
             _context.Update(news);
            await _context.SaveChangesAsync();
        }
    }
}
