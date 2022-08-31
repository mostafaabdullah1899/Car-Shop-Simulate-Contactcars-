using System.ComponentModel.DataAnnotations;

namespace Grade_Project_.DTO
{
    public class registerUserDto
    {
        [Required]
        public string Full_Name { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Compare("PassWord")]
        public string ConfirmPassWord { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [MinLength(11)]
        [MaxLength(11)]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Address { get; set; }


    }
}
