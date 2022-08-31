using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grade_Project_.Models
{
    public class Car_Images
    {
        [Key]
        public int ID { get; set; }
        public string Car_Image { get; set; }
        
        [ForeignKey("Car")]
        public int Car_Id { get; set; }

        public virtual Car Car { get; set; }
    }
}
