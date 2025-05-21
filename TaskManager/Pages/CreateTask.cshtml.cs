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
        // ������������ GET ������, ���� ����������
    }

    public IActionResult OnPost()
    {
        // ���������, ������� �� ������
        if (ModelState.IsValid)
        {
            // ������� ����� ������
            var newTask = new UserTask
            {
                Title = Title,
                Description = Description,
                Status = TaskManager.Models.TaskStatus.Pending, // ������������� ������ �� ���������
                CreatedAt = DateTime.Now
            };

            _taskService.CreateTask(newTask); // ��������� ������ ����� ������
            Success = true; // ������� ������� ������
            return Page(); // ���������� �������� �� �������� ������
        }

        return Page(); // � ������ ������ ���������� �� �� �������� � ����������� �� �������
    }
}
