using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grade_Project_.Models
{
    public class Reports
    {
        [Key]
        public int Id { get; set; }
        public string Report { get; set; }
        [ForeignKey("Car")]
        public int ReportsCar_Id { get; set; }
        public virtual Car Car { get; set; }

    }
}
