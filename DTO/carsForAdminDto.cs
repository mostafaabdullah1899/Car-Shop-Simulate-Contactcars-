namespace Grade_Project_.DTO
{
    public class carsForAdminDto
    {
        public int id { get; set; }
        public string username { get; set; }
        public int Price { get; set; }

        public int Mileage { get; set; }

        public int Made_Year { get; set; }

        public int Engine_Capacity { get; set; }

        public string Transmission { get; set; }

        public string Car_Address { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string Car_Brand_Name { get; set; }

        public string Car_Model_Name { get; set; }
        public string car_image { get; set; }
        public bool Is_Approved { get; set; }
    }
}
