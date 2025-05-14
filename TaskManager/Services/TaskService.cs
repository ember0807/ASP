using TaskManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private List<UserTask> tasks = new List<UserTask>();

        public TaskService()
        {
            // Пример добавления задач
            tasks.Add(new UserTask { Title = "Задача 1", Description = "Описание задачи 1" });
            tasks.Add(new UserTask { Title = "Задача 2", Description = "Описание задачи 2" });
        }

        public IEnumerable<UserTask> GetAllTasks()
        {
            return tasks;
        }
    }
}
