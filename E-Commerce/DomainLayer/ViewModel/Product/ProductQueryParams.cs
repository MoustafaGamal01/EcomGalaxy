namespace EcomGalaxy.ViewModel.Product
{
    /// <summary>
    /// Bound from the query string on every Index / ProductsForSeller request.
    /// All fields are optional — null means "no filter applied".
    /// The controller passes this straight through to the service and repo.
    /// </summary>
    public class ProductQueryParams
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Search
        public string? Search { get; set; }

        public string? Sort { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public int? MinRating { get; set; }

        public string? SellerId { get; set; }
    }
}