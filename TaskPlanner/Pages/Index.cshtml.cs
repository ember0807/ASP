using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TaskPlanner.Models;
using TaskPlanner.Services;

namespace TaskPlanner.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITaskService _taskService;

        public List<TaskItem> Tasks { get; set; }
        public string UploadErrorMessage { get; set; } // Для сообщений об ошибках при загрузке

        public IndexModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task OnGetAsync()
        {
            Tasks = await _taskService.GetAllTasksAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            TempData["SuccessMessage"] = "Задача успешно удалена!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostToggleCompleteAsync(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                await _taskService.UpdateTaskAsync(task); // Используем метод UpdateTaskAsync
                TempData["SuccessMessage"] = task.IsCompleted ? "Задача отмечена как выполненная!" : "Задача отмечена как невыполненная!";
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetExportAsync()
        {
            var jsonContent = await ((InMemoryTaskService)_taskService).ExportTasksToJsonAsync();
            return File(System.Text.Encoding.UTF8.GetBytes(jsonContent), "application/json", "tasks.json");
        }

        public async Task<IActionResult> OnPostImportAsync(IFormFile uploadFile)
        {
            if (uploadFile == null || uploadFile.Length == 0)
            {
                UploadErrorMessage = "Пожалуйста, выберите файл для импорта.";
                Tasks = await _taskService.GetAllTasksAsync(); // Перезагружаем список задач
                return Page();
            }

            if (!uploadFile.ContentType.Equals("application/json", System.StringComparison.OrdinalIgnoreCase))
            {
                UploadErrorMessage = "Пожалуйста, выберите файл в формате JSON.";
                Tasks = await _taskService.GetAllTasksAsync(); // Перезагружаем список задач
                return Page();
            }

            try
            {
                using (var stream = uploadFile.OpenReadStream())
                {
                    await ((InMemoryTaskService)_taskService).ImportTasksFromJsonAsync(stream);
                }
                TempData["SuccessMessage"] = "Задачи успешно импортированы!";
            }
            catch (JsonException)
            {
                UploadErrorMessage = "Ошибка при чтении JSON файла. Убедитесь, что формат файла корректен.";
            }
            catch (Exception ex)
            {
                UploadErrorMessage = $"Произошла ошибка при импорте: {ex.Message}";
            }

            return RedirectToPage();
        }
    }
}
