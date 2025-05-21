using Microsoft.AspNetCore.Mvc;
using TaskManager.Services; // Убедитесь, что ваш сервис доступен

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Этот атрибут будет автоматически использовать имя контроллера ('tasks')
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskModel task)
        {
            if (task == null)
            {
                return BadRequest("Неверные данные.");
            }

            // Ваша логика для создания задачи, например
            _taskService.AddTask(task); // Ваша логика добавления задачи

            return CreatedAtAction(nameof(CreateTask), new { id = task.Id }, task); // Предположим, у модели Task есть свойство Id
        }

        // Добавьте другие действия контроллера (например, получение задач и т. д.)
    }
}
