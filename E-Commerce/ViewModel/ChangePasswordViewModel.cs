namespace EcomGalaxy.ViewModel
{
	public class ChangePasswordViewModel
	{
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "Password and confirmation password do not match.")]
		[Display(Name = "Confirm New Passowrd")]
		public string ConfirmNewPassword { get; set; }
	}
}
