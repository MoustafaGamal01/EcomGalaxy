namespace EcomGalaxy.ViewModel
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }  
		public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public double? ProductRating { get; set; }
        public List<string>? ProductImages { get; set; }
        public int? CategoryId { get; set; }
    }
}
