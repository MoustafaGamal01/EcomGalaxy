using EcomGalaxy.ApplicationLayer.Services.IServices;
using EcomGalaxy.DataAccess.Repositories.IRepository;
using EcomGalaxy.Domain.Models.Product;

namespace EcomGalaxy.ApplicationLayer.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<bool?> AddReviewAsync(Review review)
        {
            return await _reviewRepository.AddReviewAsync(review);
        }

        public async Task<bool?> DeleteReviewAsync(int reviewId)
        {
            return await _reviewRepository.DeleteReviewAsync(reviewId);
        }

        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            return await _reviewRepository.GetReviewByIdAsync(reviewId);
        }

        public async Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId)
        {
            return await _reviewRepository.GetReviewsByProductIdAsync(productId);
        }
    }
}
