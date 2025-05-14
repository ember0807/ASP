using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Services;

namespace TaskManager.Pages
{
    public class TaskListModel : PageModel
    {
        public ITaskService TaskService { get; }

        public TaskListModel(ITaskService taskService)
        {
            TaskService = taskService;
        }

        public void OnGet()
        {
        }
    }
}
