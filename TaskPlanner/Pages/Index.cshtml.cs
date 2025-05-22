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
        public string UploadErrorMessage { get; set; } // ��� ��������� �� ������� ��� ��������

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
            TempData["SuccessMessage"] = "������ ������� �������!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostToggleCompleteAsync(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                await _taskService.UpdateTaskAsync(task); // ���������� ����� UpdateTaskAsync
                TempData["SuccessMessage"] = task.IsCompleted ? "������ �������� ��� �����������!" : "������ �������� ��� �������������!";
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
                UploadErrorMessage = "����������, �������� ���� ��� �������.";
                Tasks = await _taskService.GetAllTasksAsync(); // ������������� ������ �����
                return Page();
            }

            if (!uploadFile.ContentType.Equals("application/json", System.StringComparison.OrdinalIgnoreCase))
            {
                UploadErrorMessage = "����������, �������� ���� � ������� JSON.";
                Tasks = await _taskService.GetAllTasksAsync(); // ������������� ������ �����
                return Page();
            }

            try
            {
                using (var stream = uploadFile.OpenReadStream())
                {
                    await ((InMemoryTaskService)_taskService).ImportTasksFromJsonAsync(stream);
                }
                TempData["SuccessMessage"] = "������ ������� �������������!";
            }
            catch (JsonException)
            {
                UploadErrorMessage = "������ ��� ������ JSON �����. ���������, ��� ������ ����� ���������.";
            }
            catch (Exception ex)
            {
                UploadErrorMessage = $"��������� ������ ��� �������: {ex.Message}";
            }

            return RedirectToPage();
        }
    }
}
