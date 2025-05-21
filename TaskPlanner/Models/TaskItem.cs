using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlanner.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название задачи обязательно")]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; } = DateTime.Today;

        public bool IsCompleted { get; set; }
    }
}
