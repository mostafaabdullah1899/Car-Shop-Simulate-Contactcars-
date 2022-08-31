using System.ComponentModel.DataAnnotations;

namespace Grade_Project_.DTO
{
    public class CarBrand_DTO
    {
        public int Id { get; set; }

        [Required]
        public string BrandName { get; set; }
        [Required]
        public string BrandLogo { get; set; }

       
    }
}
