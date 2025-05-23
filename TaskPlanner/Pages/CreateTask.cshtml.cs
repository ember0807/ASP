using Microsoft.AspNetCore.Mvc; // ����������� ������� ������ ��� ���������� ���������� MVC
using Microsoft.AspNetCore.Mvc.RazorPages; // ����������� ��������� Razor Pages
using TaskPlanner.Models; // ����������� ������ �����
using TaskPlanner.Services; // ����������� ������� ��� ������ � ��������
using System.Threading.Tasks; // ����������� ������������ ���� ��� ������ � ��������

namespace TaskPlanner.Pages
{
    // ����� CreateTaskModel ������������ �������� ��� �������� ����� ������
    public class CreateTaskModel : PageModel
    {
        // ���� ��� �������� ������� �����, ������� ����� �������������� ��� ������ � ��������
        private readonly ITaskService _taskService;

        // �������� ��� �������� ������ ������ �� �����
        [BindProperty]
        public TaskItem NewTask { get; set; } = new TaskItem();

        // ����������� ������, ������� ��������� ������ ����� ����� Dependency Injection
        public CreateTaskModel(ITaskService taskService)
        {
            _taskService = taskService; // ��������� ������ �� ���������� ������ �����
        }

        // �����, �������������� HTTP GET-������� ��� ���� ��������
        public void OnGet()
        {
            // ����� ���������������� NewTask �����, ���� ����� (��������, ���������� ���� ����������)
            // NewTask.DueDate = DateTime.Today.AddDays(1); // ������ ��������� ���� ���������� �� ������
        }

        // ����������� �����, �������������� HTTP POST-������� ��� �������� �����
        public async Task<IActionResult> OnPostAsync()
        {
            // ���������, �������� �� ������ �������� (��� ������������ ���� ��������� ���������)
            if (!ModelState.IsValid)
            {
                return Page(); // ���� ������ �� �������, ���������� �� �� �������� ��� ����������� ������
            }

            // ��������� ����� ������ � ������� ������� �����
            await _taskService.AddTaskAsync(NewTask);

            // ��������� ��������� �� �������� �������� ������ � TempData ��� ����������� �� ��������� ��������
            TempData["SuccessMessage"] = "������ ������� �������!";

            // �������������� ������������ �� �������� ������� (������ �����)
            return RedirectToPage("./Index");
        }
    }
}
