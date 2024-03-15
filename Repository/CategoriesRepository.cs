using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Interface;
using NewsApp.Models;

namespace NewsApp.Repository
{
    public class CategoriesRepository:ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoriesRepository(ApplicationDbContext context) { _context = context; }

        public async Task AddAsync(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByCategoryAsync(Category category)
        {

            return await _context.Categories.FirstOrDefaultAsync(c=> c.Name == category.Name);
        }

        public async Task<Category> GetByIdAsync(int id)
        {

            return await _context.Categories.FindAsync(id);
        }




    }
}
