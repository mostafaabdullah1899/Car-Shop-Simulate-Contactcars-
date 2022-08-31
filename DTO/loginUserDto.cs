using System.ComponentModel.DataAnnotations;

namespace Grade_Project_.DTO
{
    public class loginUserDto
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string  Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
       // public bool Remeber_Me { get; set; }
    }
}
