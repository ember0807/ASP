using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager2.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название задачи обязательно.")]
        [StringLength(100, ErrorMessage = "Название задачи не может превышать 100 символов.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Описание задачи не может превышать 500 символов.")]
        public string Description { get; set; }

        [Display(Name = "Время создания")]
        [DataType(DataType.DateTime)]
        public DateTime CreationTime { get; set; } = DateTime.Now; // Устанавливаем текущее время по умолчанию
    }
}
