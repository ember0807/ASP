using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.RazorPages; 
using TaskPlanner.Models; 
using TaskPlanner.Services; 
using System.Threading.Tasks; 

namespace TaskPlanner.Pages
{
    // ����� EditTaskModel ������������ �������� ��� �������������� ������������ ������
    public class EditTaskModel : PageModel
    {
        // ���� ��� �������� ������� �����, ������������� ��� �������� � ��������
        private readonly ITaskService _taskService;

        // �������� ��� �������� ������ ������ �� �����
        [BindProperty]
        public TaskItem Task { get; set; }

        // ����������� ������, ����������� ������ ����� ����� Dependency Injection
        public EditTaskModel(ITaskService taskService)
        {
            _taskService = taskService; // ��������� ������ �� ���������� ������ �����
        }

        // ����������� �����, �������������� HTTP GET-������� ��� ��������� ���������� � ������ �� � ��������������
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // �������� ������ �� ��������������, ��������� ������ �����
            Task = await _taskService.GetTaskByIdAsync(id);

            // ���������, ���� �� ������ �������
            if (Task == null)
            {
                // ���� ������ �� �������, ���������� 404 Not Found
                return NotFound();
            }

            // ���� ������ �������, ���������� �������� � ������ ��� ��������������
            return Page();
        }

        // ����������� �����, �������������� HTTP POST-������� ��� �������� ����� ��������������
        public async Task<IActionResult> OnPostAsync()
        {
            // ���������, �������� �� ������ �������� (��� ������������ ���� ��������� ���������)
            if (!ModelState.IsValid)
            {
                // ���� ������ �� �������, ���������� �� �� �������� ��� ����������� ������
                return Page();
            }

            // ��������� ������ � ������� ������� �����
            await _taskService.UpdateTaskAsync(Task);

            // ��������� ��������� �� �������� ���������� ������ � TempData ��� ����������� �� ��������� ��������
            TempData["SuccessMessage"] = "������ ������� ���������!";

            // �������������� ������������ �� �������� ������� (������ �����)
            return RedirectToPage("./Index");
        }

        
        //public void OnGet()
        //{

        //}
    }
}
