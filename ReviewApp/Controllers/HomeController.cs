using Microsoft.AspNetCore.Mvc;
using ReviewApp.Services;
using ReviewApp.Models;

namespace ReviewApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReviewService _reviewService;

        public HomeController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public IActionResult Index()
        {
            var reviews = _reviewService.GetLatestReviews(20); // Получаем последние 20 отзывов
            ViewBag.Reviews = reviews; // Передаем отзывы в представление
            return View();
        }

        [HttpPost]
        public IActionResult Create(Review review)
        {
            if (ModelState.IsValid)
            {
                //если данные из формы валидны
                _reviewService.AddReview(review);
                return RedirectToAction("Index"); 
            }
            
            return View(review); // Вернуть представление с ошибками валидации
        }

    }
}
