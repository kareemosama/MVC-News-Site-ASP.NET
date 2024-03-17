using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsApp.Interface;
using NewsApp.Models;
using NewsApp.ViewModels;
using System.Data;


namespace NewsApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        public NewsController(INewsRepository newsRepository, ICategoriesRepository categoriesRepository)
        {
            _newsRepository = newsRepository;
            _categoriesRepository = categoriesRepository;
        }

        
        public async Task<IActionResult> Index()
        {
            var News = await _newsRepository.GetAllAsync();

            List<NewsViewModel> newsViewModels = new List<NewsViewModel>();

            foreach (var news in News)
            {
                var categoryName = await _categoriesRepository.GetByIdAsync(news.CategoryId);

                var newsViewModel = new NewsViewModel
                {
                    Id = news.Id,
                    Title = news.Title,
                    Body = news.Body,
                    NewsDate = news.NewsDate,
                    Image = news.Image ?? "",
                    CategoryName = categoryName.Name
                };

               newsViewModels.Add(newsViewModel);
              
            }


            var category = await _categoriesRepository.GetAllAsync();
          
            ViewBag.Category = new SelectList(category, "Id", "Name");

            return View(newsViewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var newsDetails = await _newsRepository.GetByIdAsync(id);
            if (newsDetails == null) return RedirectToAction("Index");

            

            var categoryName = await _categoriesRepository.GetByIdAsync(newsDetails.CategoryId);

            var newsViewModel = new NewsViewModel
            {
                Id = newsDetails.Id,
                Title = newsDetails.Title,
                Body = newsDetails.Body,
                NewsDate = newsDetails.NewsDate,
                Image = newsDetails.Image ?? "",
                CategoryName = categoryName.Name
            };

            return View(newsViewModel);

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var newsDetails = await _newsRepository.GetByIdAsync(id);
            if (newsDetails == null) return RedirectToAction("Index");



            var categoryName = await _categoriesRepository.GetByIdAsync(newsDetails.CategoryId);

            var newsViewModel = new NewsViewModel
            {
                Id = newsDetails.Id,
                Title = newsDetails.Title,
                Body = newsDetails.Body,
                NewsDate = newsDetails.NewsDate,
                Image = newsDetails.Image ?? "",
                CategoryName = categoryName.Name
            };

            var category = await _categoriesRepository.GetAllAsync();
            ViewBag.Category = new SelectList(category, "Id", "Name");

            return View(newsViewModel);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(NewsViewModel newsViewModels)
        {


            if (!int.TryParse(newsViewModels.CategoryName, out int categoryId))
            {
                // Handle invalid input, such as setting a default category or displaying an error message
                // For now, let's assume a default category with ID 1
                categoryId = 11;
            }

            var news = new News
            {
                Id = newsViewModels.Id,
                Title = newsViewModels.Title,
                Body = newsViewModels.Body,
                NewsDate = newsViewModels.NewsDate,
                Image = newsViewModels.Image,
                CategoryId= categoryId

            };

            await _newsRepository.UpdateAsync(news);
            return RedirectToAction("Edit",new { id=news.Id});

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var data = await _categoriesRepository.GetAllAsync();
            ViewBag.Category = new SelectList(data, "Id","Name") ;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public  async Task<IActionResult> Create(NewCreateViewModel newsCreateViewModel)
        {
            

            if (!ModelState.IsValid) {
                var data = await _categoriesRepository.GetAllAsync();
                ViewBag.Category = new SelectList(data, "Id", "Name");
                return View(newsCreateViewModel); 
            }

            var image = newsCreateViewModel.ImageFile;
            var imageFileName = "";

            if (image != null && image.Length > 0)
            {
                image = newsCreateViewModel.ImageFile;
                var filePath = Path.Combine("wwwroot/images", image.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                imageFileName = "/images/" + image.FileName;

            }
         
            var news = new News
            {
                Id= newsCreateViewModel.Id,
                Title= newsCreateViewModel.Title,
                Body= newsCreateViewModel.Body,
                Image = imageFileName,
                CategoryId = newsCreateViewModel.CategoryId
            };


            _newsRepository.AddAsync(news);

            return RedirectToAction("Index");

        }



        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var NewsDetails = await _newsRepository.GetByIdAsync(id);
            if(NewsDetails == null) {return  RedirectToAction("Index"); }

            await _newsRepository.DeleteAsync(NewsDetails);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Filter(int categoryId)
        {


            var News = await _newsRepository.GetByCategoryIdAsync(categoryId);

            if (News != null)
            {
                List<NewsViewModel> newsViewModels = new List<NewsViewModel>();

                foreach (var news in News)
                {
                    var categoryName = await _categoriesRepository.GetByIdAsync(news.CategoryId);

                    var newsViewModel = new NewsViewModel
                    {
                        Id = news.Id,
                        Title = news.Title,
                        Body = news.Body,
                        NewsDate = news.NewsDate,
                        Image = news.Image ?? "",
                        CategoryName = categoryName.Name
                    };

                    newsViewModels.Add(newsViewModel);

                }


                var category = await _categoriesRepository.GetAllAsync();

                ViewBag.Category = new SelectList(category, "Id", "Name");

                return View("Index", newsViewModels);
            }

            return RedirectToAction("Index");
        }


    }
}
