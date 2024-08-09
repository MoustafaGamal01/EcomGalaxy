using EcomGalaxy.Models.Product;
using EcomGalaxy.ViewModel.Review;
using Microsoft.AspNetCore.Mvc;

namespace EcomGalaxy.Controllers.Cutomer
{
    [Authorize(Roles= "Customer")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IProductService _productService;

        public ReviewController(IReviewService reviewService, IProductService productService)
        {
            _reviewService = reviewService;
            _productService = productService;
        }

        [HttpGet]
        public IActionResult AddReviewForm(int productId, string userId)
        {
            AddReviewViewModel addReviewVM = new AddReviewViewModel
            {
                ProductId = productId,
                UserId = userId
            };
            return View(addReviewVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(AddReviewViewModel addReviewVM)
        {
            if (ModelState.IsValid)
            {
                Review review = new Review
                {
                    ProductId = addReviewVM.ProductId,
                    ApplicationUserId = addReviewVM.UserId,
                    Message = addReviewVM.Message,
                    Rating = addReviewVM.Rating
                };
                bool? ok = await _reviewService.AddReviewAsync(review);
                if (ok == true)
                {
                    var prd = await _productService.GetProductByIdAsync(addReviewVM.ProductId);
                    var reviews = await _reviewService.GetReviewsByProductIdAsync(prd.Id);
                    prd.AverageRating = reviews.Average(r => r.Rating);
                    await _productService.UpdateProductAsync(prd.Id, prd);
                    return RedirectToAction("ProductDetails", "Product", new { Id = review.ProductId });
                }
            }
            return View("AddReviewForm", addReviewVM);
        }
    }
}
