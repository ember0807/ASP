using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using TaskManager.Models;
using TaskManager.Services;

public class CreateTaskModel : PageModel
{
    private readonly ITaskService _taskService;

    public CreateTaskModel(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [BindProperty]
    public string Title { get; set; }

    [BindProperty]
    public string Description { get; set; }

    public bool Success { get; set; }

    public void OnGet()
    {
        // Обрабатываем GET запрос, если необходимо
    }

    public IActionResult OnPost()
    {
        // Проверяем, валидны ли данные
        if (ModelState.IsValid)
        {
            // Создаем новую задачу
            var newTask = new UserTask
            {
                Title = Title,
                Description = Description,
                Status = TaskManager.Models.TaskStatus.Pending, // Устанавливаем статус по умолчанию
                CreatedAt = DateTime.Now
            };

            _taskService.CreateTask(newTask); // Сохраняем задачу через сервис
            Success = true; // Успешно создана задача
            return Page(); // Возвращаем страницу со статусом успеха
        }

        return Page(); // В случае ошибки возвращаем ту же страницу с сообщениями об ошибках
    }
}
