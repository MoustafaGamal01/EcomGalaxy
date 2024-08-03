namespace EcomGalaxy.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [MaxLength(200)]
        public string? Street { get; set; }
    }
}