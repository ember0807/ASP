using TaskManager.Models;
using System.Collections.Generic;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        //метод для получения всех задач
        IEnumerable<UserTask> GetAllTasks();
    }
}
