using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Models;
using TaskManager.Services;


namespace TaskManager.Pages.Tasks
{
    public class NewTaskModel : PageModel
    {
        private readonly ITaskService _taskService;
        // �������, ����������� �� �������� � ��������� �� �����
        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Description { get; set; }

        public bool Success { get; private set; }
        // �����������, � ������� ���������� ITaskService
       public NewTaskModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public void OnGet()
        {
            // ��������� GET-�������
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // �������� ����� ������ � ���������� � ���������, ����������� �� �����
                var newTask = new Models.UserTask
                {
                    Title = Title,
                    Description = Description
                };
                // ����� ������ ��� �������� ����� ������
                _taskService.CreateTask(newTask); // ����� ������ CreateTask
                Success = true; // ������� �������� ������
            }
            return Page(); // ���������� �� �� ��������
        }

    }
}
