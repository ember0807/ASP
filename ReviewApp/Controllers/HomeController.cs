using Microsoft.AspNetCore.Mvc;
using ReviewApp.Models;
using ReviewApp.Services;

namespace ReviewApp.Controllers
{
    [Route("[controller]")] // Базовый маршрут для контроллера, например, /Home
    public class HomeController : Controller
    {
        private readonly ReviewService _reviewService;

        public HomeController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("Index")] // Обрабатывает GET для /Home/Index
        [HttpGet("/")] // Также обрабатывает корневой URL для приложения
        public IActionResult Index()
        {
            var latestReviews = _reviewService.GetLatestReviews();
            return View(latestReviews);
        }

        [HttpPost("AddReview")] // Обрабатывает POST для /Home/AddReview
        public IActionResult AddReview(Review newReview)
        {
            if (string.IsNullOrWhiteSpace(newReview.AuthorName) ||
                string.IsNullOrWhiteSpace(newReview.ReviewText) ||
                newReview.Rating < 1 || newReview.Rating > 5)
            {
                return BadRequest("Пожалуйста, предоставьте всю необходимую информацию: Имя автора, Текст отзыва и Оценку от 1 до 5.");
            }

            _reviewService.AddReview(newReview);
            return RedirectToAction("Index");
        }
    }
}