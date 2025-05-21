using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Models;
using TaskManager.Services;


namespace TaskManager.Pages.Tasks
{
    public class NewTaskModel : PageModel
    {
        private readonly ITaskService _taskService;
        // Атрибут, указывающий на привязку к свойствам из формы
        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Description { get; set; }

        public bool Success { get; private set; }
        // Конструктор, в который внедряется ITaskService
       public NewTaskModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public void OnGet()
        {
            // Обработка GET-запроса
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Создание новой задачи с заголовком и описанием, полученными из формы
                var newTask = new Models.UserTask
                {
                    Title = Title,
                    Description = Description
                };
                // Вызов метода для создания новой задачи
                _taskService.CreateTask(newTask); // Вызов метода CreateTask
                Success = true; // Успешно добавили задачу
            }
            return Page(); // Возвращаем ту же страницу
        }

    }
}
