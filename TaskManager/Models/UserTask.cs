namespace TaskManager.Models
{
    public class UserTask
    {
        public int Id { get; set; } // Уникальный идентификатор задачи
        public string Title { get; set; } // Заголовок задачи
        public string Description { get; set; } // Описание задачи
        public TaskManager.Models.TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } 
    }

    // Возможное перечисление для статуса задачи
    public enum TaskStatus
    {
        Pending,
        InProgress,
        Completed,
        OnHold
    }
}
