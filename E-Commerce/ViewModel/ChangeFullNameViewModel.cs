namespace EcomGalaxy.ViewModel
{
    public class ChangeFullNameViewModel
    {
		[Required]
		[Display(Name = "New Full Name")]
		public string NewFullName { get; set; }
	}
}
