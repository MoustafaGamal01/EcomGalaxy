namespace EcomGalaxy.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
		
		[Required]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required]
		[Compare("NewPassword")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
