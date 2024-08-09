using EcomGalaxy.Domain.Models.Product;

namespace EcomGalaxy.ApplicationLayer.Services.IServices
{
    public interface IReviewService
    {
        Task<bool?> AddReviewAsync(Review review);
        Task<Review> GetReviewByIdAsync(int reviewId);
        Task<bool?> DeleteReviewAsync(int reviewId);
        Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId);
    }
}
