using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
namespace Grade_Project_.Models
{
    public class Users:IdentityUser<int>
    {
        public string Full_Name { get; set; }
        public string User_Address { get; set; }  
        public bool Is_Active { get; set; } 
        public bool Is_Admin { get; set; }
        
        public virtual List<Car> Cars { get; set; }
        
        public Users()
        {
            Is_Active = true;  
        }

    }
}
