using System;
using System.ComponentModel.DataAnnotations;

namespace ReviewApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Имя автора обязательно.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Текст отзыва обязателен.")]
        public string Text { get; set; }

        [Range(1, 5, ErrorMessage = "Оценка должна быть от 1 до 5.")]
        public int Rating { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
