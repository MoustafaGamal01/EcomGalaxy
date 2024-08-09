using EcomGalaxy.ViewModel.Review;

namespace EcomGalaxy.ViewModel.Product
{
    public class ProductDetailsFormViewModel
    {
        public int ProductId { get; set; }
        public double? Rate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<string> Images { get; set; }
        public IEnumerable<ShowReviewViewModel> Reviews { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
    }
}
