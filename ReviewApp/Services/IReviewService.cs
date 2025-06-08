using Microsoft.AspNetCore.Mvc.ViewEngines;
using ReviewApp.Models;
using System.Collections.Generic;

namespace ReviewApp.Services
{
    public interface IReviewService
    {
        List<ReviewApp.Models.Review> GetLatestReviews(int count);
        void AddReview(ReviewApp.Models.Review review);
    }
}

