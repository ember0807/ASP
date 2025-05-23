using Microsoft.AspNetCore.Mvc; // Импортируем базовые классы для управления действиями MVC
using Microsoft.AspNetCore.Mvc.RazorPages; // Импортируем поддержку Razor Pages
using TaskPlanner.Models; // Импортируем модели задач
using TaskPlanner.Services; // Импортируем сервисы для работы с задачами
using System.Threading.Tasks; // Импортируем пространство имен для работы с задачами

namespace TaskPlanner.Pages
{
    // Класс CreateTaskModel представляет страницу для создания новой задачи
    public class CreateTaskModel : PageModel
    {
        // Поле для хранения сервиса задач, которое будет использоваться для работы с задачами
        private readonly ITaskService _taskService;

        // Свойство для привязки данных задачи из формы
        [BindProperty]
        public TaskItem NewTask { get; set; } = new TaskItem();

        // Конструктор класса, который принимает сервис задач через Dependency Injection
        public CreateTaskModel(ITaskService taskService)
        {
            _taskService = taskService; // Сохраняем ссылку на переданный сервис задач
        }

        // Метод, обрабатывающий HTTP GET-запросы для этой страницы
        public void OnGet()
        {
            // Можно инициализировать NewTask здесь, если нужно (например, установить дату выполнения)
            // NewTask.DueDate = DateTime.Today.AddDays(1); // Пример установки даты выполнения на завтра
        }

        // Асинхронный метод, обрабатывающий HTTP POST-запросы при отправке формы
        public async Task<IActionResult> OnPostAsync()
        {
            // Проверяем, является ли модель валидной (все обязательные поля заполнены корректно)
            if (!ModelState.IsValid)
            {
                return Page(); // Если модель не валидна, возвращаем ту же страницу для исправления ошибок
            }

            // Добавляем новую задачу с помощью сервиса задач
            await _taskService.AddTaskAsync(NewTask);

            // Сохраняем сообщение об успешном создании задачи в TempData для отображения на следующей странице
            TempData["SuccessMessage"] = "Задача успешно создана!";

            // Перенаправляем пользователя на страницу индекса (списка задач)
            return RedirectToPage("./Index");
        }
    }
}
