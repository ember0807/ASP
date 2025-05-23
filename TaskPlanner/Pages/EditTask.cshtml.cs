using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.RazorPages; 
using TaskPlanner.Models; 
using TaskPlanner.Services; 
using System.Threading.Tasks; 

namespace TaskPlanner.Pages
{
    //  ласс EditTaskModel представл€ет страницу дл€ редактировани€ существующей задачи
    public class EditTaskModel : PageModel
    {
        // ѕоле дл€ хранени€ сервиса задач, используемого дл€ операций с задачами
        private readonly ITaskService _taskService;

        // —войство дл€ прив€зки данных задачи из формы
        [BindProperty]
        public TaskItem Task { get; set; }

        //  онструктор класса, принимающий сервис задач через Dependency Injection
        public EditTaskModel(ITaskService taskService)
        {
            _taskService = taskService; // —охран€ем ссылку на переданный сервис задач
        }

        // јсинхронный метод, обрабатывающий HTTP GET-запросы дл€ получени€ информации о задаче по еЄ идентификатору
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // ѕолучаем задачу по идентификатору, использу€ сервис задач
            Task = await _taskService.GetTaskByIdAsync(id);

            // ѕровер€ем, была ли задача найдена
            if (Task == null)
            {
                // ≈сли задача не найдена, возвращаем 404 Not Found
                return NotFound();
            }

            // ≈сли задача найдена, возвращаем страницу с формой дл€ редактировани€
            return Page();
        }

        // јсинхронный метод, обрабатывающий HTTP POST-запросы при отправке формы редактировани€
        public async Task<IActionResult> OnPostAsync()
        {
            // ѕровер€ем, €вл€етс€ ли модель валидной (все об€зательные пол€ заполнены корректно)
            if (!ModelState.IsValid)
            {
                // ≈сли модель не валидна, возвращаем ту же страницу дл€ исправлени€ ошибок
                return Page();
            }

            // ќбновл€ем задачу с помощью сервиса задач
            await _taskService.UpdateTaskAsync(Task);

            // —охран€ем сообщение об успешном обновлении задачи в TempData дл€ отображени€ на следующей странице
            TempData["SuccessMessage"] = "«адача успешно обновлена!";

            // ѕеренаправл€ем пользовател€ на страницу индекса (списка задач)
            return RedirectToPage("./Index");
        }

        
        //public void OnGet()
        //{

        //}
    }
}
