using TaskPlanner.Models;

//Этот интерфейс определяет операции, которые мы можем выполнять с задачами.
// Services/ITaskService.cs
using TaskPlanner.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskPlanner.Services
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllTasksAsync();
        Task AddTaskAsync(TaskItem task);
        Task<TaskItem> GetTaskByIdAsync(int id); // Может понадобиться в будущем
        // Можно добавить методы UpdateTaskAsync, DeleteTaskAsync и т.д.
    }
}
