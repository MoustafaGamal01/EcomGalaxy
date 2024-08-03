using EcomGalaxy.Models;

namespace EcomGalaxy.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MyContext _context;

        public ReviewRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<bool?> AddReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool?> DeleteReviewAsync(int reviewId)
        {
            var existingReview = await GetReviewByIdAsync(reviewId);
            if (existingReview == null)
            {
                throw new InvalidOperationException($"Review with ID {reviewId} not found.");
            }
            _context.Reviews.Remove(existingReview);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            return await _context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);   
        }

        public async Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId)
        {
            return await _context.Reviews
                .Include(r => r.Product)
                .Where(r => r.ProductId == productId)
                .ToListAsync();
        }
    }
}
