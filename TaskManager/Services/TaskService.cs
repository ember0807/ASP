using TaskManager.Models;
using TaskManager.Services;
using System.Collections.Generic;
using System.Linq;
public class TaskService : ITaskService
{
    private List<UserTask> tasks = new List<UserTask>();
    private int nextId = 1; // Счетчик для уникальных ID задач

    public TaskService()
    {
        // Инициализация примеров задач с уникальными ID
        tasks.Add(new UserTask { Id = nextId++, Title = "Задача 1", Description = "Описание задачи 1", Status = TaskManager.Models.TaskStatus.Pending });
        tasks.Add(new UserTask { Id = nextId++, Title = "Задача 2", Description = "Описание задачи 2", Status = TaskManager.Models.TaskStatus.Pending });
    }

    public IEnumerable<UserTask> GetAllTasks()
    {
        return tasks; // Возврат всех задач
    }

    public void CreateTask(UserTask newTask)
    {
        newTask.Id = nextId++; // Присвоение нового уникального идентификатора
        newTask.CreatedAt = DateTime.Now; // Устанавливаем дату создания
        tasks.Add(newTask); // Добавление новой задачи в общий список
    }

    public UserTask GetTaskById(int id)
    {
        return tasks.FirstOrDefault(task => task.Id == id); // Поиск задачи по ID
    }

    public void UpdateTask(UserTask updatedTask)
    {
        var existingTask = GetTaskById(updatedTask.Id);
        if (existingTask != null)
        {
            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.Status = updatedTask.Status; // Обновление статуса, если есть
        }
    }

    public void DeleteTask(int id)
    {
        var taskToRemove = GetTaskById(id);
        if (taskToRemove != null)
        {
            tasks.Remove(taskToRemove); // Удаление задачи по ID
        }
    }

    public IEnumerable<UserTask> GetTasksByStatus(TaskManager.Models.TaskStatus status) 
    {
        return tasks.Where(task => task.Status == status); // Получение задач по статусу
    }
}
