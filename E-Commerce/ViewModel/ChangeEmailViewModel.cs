namespace EcomGalaxy.ViewModel
{
	public class ChangeEmailViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
    }
}
