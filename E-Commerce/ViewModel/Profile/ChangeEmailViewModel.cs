namespace EcomGalaxy.ViewModel.Profile
{
    public class ChangeEmailViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
