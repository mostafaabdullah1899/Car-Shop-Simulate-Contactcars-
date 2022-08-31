using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grade_Project_.Models
{
    public class Car_Model
    {
        [Key]
        public int Id { get; set; }
        public string Model_Name { get; set; }
       
        [ForeignKey("Car_Brand")]
        public int CarBrand_Id { get; set; }
        
        public virtual List<Car> Cars { get; set; }    
        public virtual Car_Brand Car_Brand { get; set; }
    }
}
