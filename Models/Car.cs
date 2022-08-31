using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grade_Project_.Models
{
    public class Car
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [RegularExpression("[0-9]{4,}")]
        public int Price { get; set; }
        [Required]
        public int Mileage { get; set; }
        //[Required]
        public string? Image { get; set; }
        [Required]
        public int Made_Year { get; set; }
        [Required]
        public int Engine_Capacity { get; set; }
        [Required]
        public string Transmission { get; set; }
        [Required]
        public string Car_Address { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Is_Used { get; set; }
        [Required]
        public bool Is_Approved { get; set; }
        [Required]
        public bool Is_Available { get; set; }
        [Required]
        public DateTime Added_Date { get; set; }
        [Required] 
        [ForeignKey("Car_Model")]
        public int Car_Model_Id { get; set; }
        [Required]
        [ForeignKey("Car_Brand")]
        public int Car_Brand_Id { get; set; }
        [Required]
        [ForeignKey("Users")]
        public int User_Id { get; set; }

        public virtual Car_Brand? Car_Brand { get; set; }
        public virtual Car_Model? Car_Model { get; set; }
        public virtual List<Car_Images>? Car_Images { get; set; }
        public virtual List<Reports>? Reports { get; set; }

        public virtual Users? Users { get; set; }
        
        public Car()
        {
            Is_Approved = false;
            Is_Available = true;
            Is_Used = true;
            
            
            Added_Date = DateTime.Now;
        }
    
    }
}
