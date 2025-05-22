using TaskPlanner.Models;

//Эта реализация будет хранить задачи в списке в памяти. Для реального приложения здесь была бы логика для работы с базой данных.
// Services/InMemoryTaskService.cs
using TaskPlanner.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace TaskPlanner.Services
{
    public class InMemoryTaskService : ITaskService
    {
        private readonly List<TaskItem> _tasks = new List<TaskItem>();
        private int _nextId = 1;
        private readonly string _filePath = "tasks.json"; // Путь к файлу для сохранения/загрузки

        public InMemoryTaskService()
        {
            LoadTasksFromFile(); // Загружаем задачи при инициализации
        }

        public Task AddTaskAsync(TaskItem task)
        {
            task.Id = _nextId++;
            _tasks.Add(task);
            SaveTasksToFile(); // Сохраняем после добавления
            return Task.CompletedTask;
        }

        public Task<List<TaskItem>> GetAllTasksAsync()
        {
            return Task.FromResult(_tasks.OrderBy(t => t.DueDate).ThenBy(t => t.IsCompleted).ToList());
        }

        public Task<TaskItem> GetTaskByIdAsync(int id)
        {
            return Task.FromResult(_tasks.FirstOrDefault(t => t.Id == id));
        }

        public Task UpdateTaskAsync(TaskItem updatedTask)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
            if (existingTask != null)
            {
                existingTask.Title = updatedTask.Title;
                existingTask.Description = updatedTask.Description;
                existingTask.DueDate = updatedTask.DueDate;
                existingTask.IsCompleted = updatedTask.IsCompleted;
                SaveTasksToFile(); // Сохраняем после обновления
            }
            return Task.CompletedTask;
        }

        public Task DeleteTaskAsync(int id)
        {
            var taskToRemove = _tasks.FirstOrDefault(t => t.Id == id);
            if (taskToRemove != null)
            {
                _tasks.Remove(taskToRemove);
                SaveTasksToFile(); // Сохраняем после удаления
            }
            return Task.CompletedTask;
        }

        // Приватные методы для работы с JSON файлом
        private void SaveTasksToFile()
        {
            var jsonString = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, jsonString);
        }

        private void LoadTasksFromFile()
        {
            if (File.Exists(_filePath))
            {
                var jsonString = File.ReadAllText(_filePath);
                var loadedTasks = JsonSerializer.Deserialize<List<TaskItem>>(jsonString);
                if (loadedTasks != null)
                {
                    _tasks.Clear();
                    _tasks.AddRange(loadedTasks);
                    _nextId = _tasks.Any() ? _tasks.Max(t => t.Id) + 1 : 1; // Обновляем следующий ID
                }
            }
        }

        // Методы для импорта/экспорта через API (будут вызываться из PageModel)
        public async Task ImportTasksFromJsonAsync(Stream jsonStream)
        {
            using (var reader = new StreamReader(jsonStream))
            {
                var jsonString = await reader.ReadToEndAsync();
                var importedTasks = JsonSerializer.Deserialize<List<TaskItem>>(jsonString);

                if (importedTasks != null)
                {
                    foreach (var task in importedTasks)
                    {
                        task.Id = _nextId++; // Присваиваем новые ID во избежание конфликтов
                        _tasks.Add(task);
                    }
                    SaveTasksToFile();
                }
            }
        }

        public async Task<string> ExportTasksToJsonAsync()
        {
            return await Task.FromResult(JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
