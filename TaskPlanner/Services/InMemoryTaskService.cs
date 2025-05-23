using System.Text.Json;
using TaskPlanner.Models; // Импортируем модели задач

// Эта реализация будет хранить задачи в списке в памяти.
// Для реального приложения здесь была бы логика для работы с базой данных.

namespace TaskPlanner.Services
{
    // Класс InMemoryTaskService реализует интерфейс ITaskService
    public class InMemoryTaskService : ITaskService
    {
        // Список для хранения задач в памяти
        private readonly List<TaskItem> _tasks = new List<TaskItem>();
        // Переменная для генерации уникальных идентификаторов задач
        private int _nextId = 1;
        // Путь к файлу для сохранения и загрузки задач
        private readonly string _filePath = "tasks.json";

        // Конструктор класса
        public InMemoryTaskService()
        {
            // Загружаем задачи из файла при инициализации сервиса
            LoadTasksFromFile();
        }

        // Асинхронный метод для добавления новой задачи
        public Task AddTaskAsync(TaskItem task)
        {
            // Устанавливаем уникальный идентификатор для новой задачи
            task.Id = _nextId++;
            // Добавляем задачу в список
            _tasks.Add(task);
            // Сохраняем все задачи в файл после добавления
            SaveTasksToFile();
            return Task.CompletedTask; // Завершаем задачу
        }

        // Асинхронный метод для получения всех задач
        public Task<List<TaskItem>> GetAllTasksAsync()
        {
            // Возвращаем список задач, отсортированный по дате выполнения и статусу завершения
            return Task.FromResult(_tasks.OrderBy(t => t.DueDate).ThenBy(t => t.IsCompleted).ToList());
        }

        // Асинхронный метод для получения задачи по идентификатору
        public Task<TaskItem> GetTaskByIdAsync(int id)
        {
            // Ищем задачу с заданным идентификатором и возвращаем её
            return Task.FromResult(_tasks.FirstOrDefault(t => t.Id == id));
        }

        // Асинхронный метод для обновления существующей задачи
        public Task UpdateTaskAsync(TaskItem updatedTask)
        {
            // Ищем задачу, которую необходимо обновить
            var existingTask = _tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
            if (existingTask != null)
            {
                // Обновляем поля существующей задачи
                existingTask.Title = updatedTask.Title;
                existingTask.Description = updatedTask.Description;
                existingTask.DueDate = updatedTask.DueDate;
                existingTask.IsCompleted = updatedTask.IsCompleted;
                // Сохраняем все задачи в файл после обновления
                SaveTasksToFile();
            }
            return Task.CompletedTask; // Завершаем задачу
        }

        // Асинхронный метод для удаления задачи по идентификатору
        public Task DeleteTaskAsync(int id)
        {
            // Ищем задачу, которую необходимо удалить
            var taskToRemove = _tasks.FirstOrDefault(t => t.Id == id);
            if (taskToRemove != null)
            {
                // Удаляем задачу из списка
                _tasks.Remove(taskToRemove);
                // Сохраняем все задачи в файл после удаления
                SaveTasksToFile();
            }
            return Task.CompletedTask; // Завершаем задачу
        }

        // Приватный метод для сохранения списка задач в JSON файл
        private void SaveTasksToFile()
        {
            // Сериализуем список задач в строку JSON
            var jsonString = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
            // Записываем JSON строку в файл
            File.WriteAllText(_filePath, jsonString);
        }

        // Приватный метод для загрузки задач из JSON файла
        private void LoadTasksFromFile()
        {
            // Проверяем, существует ли файл
            if (File.Exists(_filePath))
            {
                // Читаем содержимое файла
                var jsonString = File.ReadAllText(_filePath);
                // Десериализуем JSON строку в список задач
                var loadedTasks = JsonSerializer.Deserialize<List<TaskItem>>(jsonString);
                if (loadedTasks != null)
                {
                    // Очищаем текущий список задач
                    _tasks.Clear();
                    // Добавляем загруженные задачи в текущий список
                    _tasks.AddRange(loadedTasks);
                    // Обновляем счетчик следующего идентификатора задач
                    _nextId = _tasks.Any() ? _tasks.Max(t => t.Id) + 1 : 1;
                }
            }
        }

        // Асинхронный метод для импорта задач из JSON потока
        public async Task ImportTasksFromJsonAsync(Stream jsonStream)
        {
            using (var reader = new StreamReader(jsonStream))
            {
                // Читаем входной поток как строку JSON
                var jsonString = await reader.ReadToEndAsync();
                // Десериализуем строку JSON в список задач
                var importedTasks = JsonSerializer.Deserialize<List<TaskItem>>(jsonString);

                if (importedTasks != null)
                {
                    // Сохраняем задачи в текущий список с новыми уникальными ID
                    foreach (var task in importedTasks)
                    {
                        task.Id = _nextId++; // Присваиваем новые ID во избежание конфликтов
                        _tasks.Add(task);
                    }
                    // Сохраняем обновленный список задач в файл
                    SaveTasksToFile();
                }
            }
        }

        // Асинхронный метод для экспорта задач в JSON строку
        public async Task<string> ExportTasksToJsonAsync()
        {
            // Возвращаем сериализованный список задач в виде JSON строки
            return await Task.FromResult(JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
