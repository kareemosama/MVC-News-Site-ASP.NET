using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Interface;
using NewsApp.Models;
using NewsApp.ViewModels;
using System.Data;

namespace NewsApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
              _categoriesRepository = categoriesRepository;
        }


        public async  Task<IActionResult> Index()
        {
            var Category = await _categoriesRepository.GetAllAsync();

            var CategoryViewModel  = new CategoryViewModel
            {
                Categories = await _categoriesRepository.GetAllAsync(), // Assuming you have a method to get all categories
                NewCategory = new Category()
            };
            return View(CategoryViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CategoryViewModel categoryiewModel) {

           if (categoryiewModel.NewCategory == null) { TempData["Error"] = "Please Insert Category"; RedirectToAction("Index", "Categories"); }

            var category = new Category
            {
                Id = categoryiewModel.NewCategory.Id,
                Name = categoryiewModel.NewCategory.Name,
            };

            

            if ( await _categoriesRepository.GetByCategoryAsync(category) != null) { TempData["Error"] = "Same Category Please try again"; return RedirectToAction("Index", "Categories"); }

            await _categoriesRepository.AddAsync(category);

            return RedirectToAction("Index", "Categories");

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoriesRepository.GetByIdAsync(id);
            if (category == null) { TempData["Error"] = "Not Found"; return RedirectToAction("Index", "Categories"); }

            await _categoriesRepository.DeleteAsync(category);
            return RedirectToAction(nameof(Index));


        }
    }
}
