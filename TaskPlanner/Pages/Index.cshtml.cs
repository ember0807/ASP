using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskPlanner.Models;
using TaskPlanner.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskPlanner.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITaskService _taskService;

        public List<TaskItem> Tasks { get; set; }

        public IndexModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task OnGetAsync()
        {
            Tasks = await _taskService.GetAllTasksAsync();
        }

        //  обработчик для отметки выполнения 
        
        public async Task<IActionResult> OnPostToggleCompleteAsync(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                // Если бы это была база данных, здесь был бы вызов Update
            }
            return RedirectToPage();
        }
        
    }
}
