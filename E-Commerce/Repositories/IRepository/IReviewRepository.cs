using EcomGalaxy.Models.Product;

namespace EcomGalaxy.Repositories.IRepository
{
    public interface IReviewRepository
    {
        Task<bool?> AddReviewAsync(Review review);
        Task<Review> GetReviewByIdAsync(int reviewId);
        Task<bool?> DeleteReviewAsync(int reviewId);
        Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId);
    }
}
