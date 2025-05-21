using TaskPlanner.Models;

//Эта реализация будет хранить задачи в списке в памяти. Для реального приложения здесь была бы логика для работы с базой данных.
// Services/InMemoryTaskService.cs
using TaskPlanner.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskPlanner.Services
{
    public class InMemoryTaskService : ITaskService
    {
        private readonly List<TaskItem> _tasks = new List<TaskItem>();
        private int _nextId = 1;

        public Task AddTaskAsync(TaskItem task)
        {
            task.Id = _nextId++;
            _tasks.Add(task);
            return Task.CompletedTask;
        }

        public Task<List<TaskItem>> GetAllTasksAsync()
        {
            return Task.FromResult(_tasks.OrderBy(t => t.DueDate).ThenBy(t => !t.IsCompleted).ToList());
        }

        public Task<TaskItem> GetTaskByIdAsync(int id)
        {
            return Task.FromResult(_tasks.FirstOrDefault(t => t.Id == id));
        }
    }
}
