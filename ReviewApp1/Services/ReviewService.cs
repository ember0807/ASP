using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ReviewApp.Models;

namespace ReviewApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly List<Review> _reviews = new List<Review>();
        private readonly string _filePath = "reviews.json"; // Путь к файлу

        public ReviewService()
        {
            LoadReviews(); // Загружаем отзывы при создании экземпляра сервиса
        }

        public List<Review> GetLatestReviews(int count)
        {
            return _reviews.OrderByDescending(r => r.DateCreated).Take(count).ToList();
        }

        public void AddReview(Review review)
        {
            review.DateCreated = DateTime.Now;
            //  Устанавливаем уникальный Id, если его еще нет
            if (review.Id == 0)
            {
                review.Id = _reviews.Count > 0 ? _reviews.Max(r => r.Id) + 1 : 1;
            }
            _reviews.Add(review);
            SaveReviews(); // Сохраняем отзывы после добавления
        }

        private void LoadReviews()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    string jsonString = File.ReadAllText(_filePath);
                    if (!string.IsNullOrEmpty(jsonString)) // Проверка на пустой файл
                    {
                        List<Review> loadedReviews = JsonSerializer.Deserialize<List<Review>>(jsonString);
                        if (loadedReviews != null)
                        {
                            _reviews.AddRange(loadedReviews);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Обработка ошибок при чтении или десериализации файла
                    Console.WriteLine($"Ошибка при загрузке отзывов из файла: {ex.Message}");
                    
                }
            }
        }

        private void SaveReviews()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(_reviews);
                File.WriteAllText(_filePath, jsonString);
            }
            catch (Exception ex)
            {
                // Обработка ошибок при записи в файл
                Console.WriteLine($"Ошибка при сохранении отзывов в файл: {ex.Message}");
                
            }
        }
    }
}
