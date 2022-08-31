using System.ComponentModel.DataAnnotations;

namespace Grade_Project_.Models
{
    public class Car_Brand
    {
        [Key]
        public int Id { get; set; }
        public string Brand_Name { get; set; }
        public string Brand_Logo { get; set; }

        public virtual List<Car> Cars { get; set; }
        public virtual List<Car_Model> Car_Models { get; set; } 
    }
}
