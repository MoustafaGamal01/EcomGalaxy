using System.ComponentModel.DataAnnotations;

namespace EcomGalaxy.Models
{
    public class Category
    { 
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string? CategoryImage { get; set; }
    }
}
