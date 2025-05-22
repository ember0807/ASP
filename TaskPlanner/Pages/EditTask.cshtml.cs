using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskPlanner.Models;
using TaskPlanner.Services;
using System.Threading.Tasks;

namespace TaskPlanner.Pages
{
    public class EditTaskModel : PageModel
    {
        private readonly ITaskService _taskService;
        [BindProperty]

        public TaskItem Task { get; set; }
        public EditTaskModel(ITaskService taskService)
        { 
            _taskService = taskService;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Task = await _taskService.GetTaskByIdAsync(id);

            if (Task == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _taskService.UpdateTaskAsync(Task);
            TempData["SuccessMessage"] = "Задача успешно обновлена!";
            return RedirectToPage("./Index");
        }

        //public void OnGet()
        //{

        //}
    }
}
