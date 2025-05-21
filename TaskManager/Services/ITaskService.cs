using TaskManager.Models;
using System.Collections.Generic;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        
        IEnumerable<Models.UserTask> GetAllTasks(); // Метод для получения всех задач
        Models.UserTask GetTaskById(int id); // Получение задачи по ID
        void CreateTask(Models.UserTask newTask); // Метод для создания новой задачи
        void UpdateTask(Models.UserTask updatedTask); // Метод для обновления существующей задачи
        void DeleteTask(int id); // Метод для удаления задачи по ID
        IEnumerable<Models.UserTask> GetTasksByStatus(TaskManager.Models.TaskStatus status);
        // Получение задач по статусу
    }
}
