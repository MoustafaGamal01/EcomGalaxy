namespace EcomGalaxy.ViewModel.Review
{
    public class AddReviewViewModel
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
