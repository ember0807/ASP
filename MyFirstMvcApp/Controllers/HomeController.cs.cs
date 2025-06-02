using Microsoft.AspNetCore.Mvc;
using MyFirstMvcApp.Services;
using System; // Добавьте using для DateTime

namespace MyFirstMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGreetingService _greetingService;

        public HomeController(IGreetingService greetingService)
        {
            _greetingService = greetingService;
        }

        public string Index()
        {
            return "Hello, world!";
        }

        [HttpGet]
        public string Welcome()
        {
            string message = _greetingService.GetWelcomeMessage();
            return message;
        }

        // GET: /Home/Time
        public string Time()
        {
            return $"Текущее время: {DateTime.Now.ToString("HH:mm:ss")}";
            // Или так для даты и времени: DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")
            // Или так для только даты: DateTime.Now.ToString("dd.MM.yyyy")
        }
    }
}