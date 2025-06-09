using ReviewApp.Models;

namespace ReviewApp.Services
{
    public class ReviewService
    {
        private static readonly List<Review> _reviews = new List<Review>();
        private const int MaxReviews = 20; // N отзывов

        public IEnumerable<Review> GetLatestReviews()
        {
            // Возвращаем отзывы в том порядке, в котором они были добавлены
            return _reviews.Take(MaxReviews).ToList();
        }

        public void AddReview(Review review)
        {
            if (review == null)
            {
                throw new ArgumentNullException(nameof(review));
            }

            // Убеждаемся, что у отзыва есть дата создания
            review.CreatedDate = DateTime.Now;
            _reviews.Insert(0, review); // Добавляем в начало, чтобы сохранить порядок

            //  удаляем старые отзывы, если превышаем MaxReviews
            while (_reviews.Count > MaxReviews)
            {
                _reviews.RemoveAt(_reviews.Count - 1);
            }
        }
    }
}
