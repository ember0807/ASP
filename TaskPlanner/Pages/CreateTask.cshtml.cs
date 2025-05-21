using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskPlanner.Models;
using TaskPlanner.Services;
using System.Threading.Tasks;

namespace TaskPlanner.Pages
{
    public class CreateTaskModel : PageModel
    {
        private readonly ITaskService _taskService;

        [BindProperty]
        public TaskItem NewTask { get; set; } = new TaskItem();

        public CreateTaskModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public void OnGet()
        {
            // Можно инициализировать NewTask здесь, если нужно
            // NewTask.DueDate = DateTime.Today.AddDays(1);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _taskService.AddTaskAsync(NewTask);
            TempData["SuccessMessage"] = "Задача успешно создана!";
            return RedirectToPage("./Index");
        }
    }
}
