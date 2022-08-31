using Grade_Project_.DTO;
using Grade_Project_.Models;
using Microsoft.EntityFrameworkCore;

namespace Grade_Project_.Repository
{
    public class Car_Repository : ICar
    {
        Cars_Entity context;

        public Car_Repository(Cars_Entity context)
        {
            this.context = context;
        }
        public List<Car> GetAllforUser(int id)
        {
            return context.Cars.Include(e => e.Car_Brand).Include(e => e.Car_Model)
                .Where(e => e.Is_Approved == true&&e.User_Id==id).ToList();

             
        }
        public List<Car> GetAllforUserTosell()
        {
            List<Car> cars= context.Cars.Include(e => e.Car_Brand).Include(e => e.Car_Model)
                .Where(e => e.Is_Approved == true && e.Is_Available == true).ToList();

            cars.Reverse();
            return cars;
        }
        public List<Car> GetAllforAdmin()
        {
            return context.Cars.Include(e => e.Car_Brand).Include(e => e.Car_Model)
                .Include(e=>e.Users)
                .Where(e => e.Is_Approved == false).ToList();
        }
        public List<string> GetImagesById(int id)
        {


            List<Car_Images> m = context.Cars_Images.Where(e => e.Car_Id == id).ToList();
            List<string> images = new List<string>();

            for (int i = 0; i < m.Count; i++)
            {
                images.Add(m[i].Car_Image);

            }

            return images;
        }
        public Car GetById(int id)
        {
            return context.Cars.Include(e => e.Car_Brand).Include(e => e.Car_Model).FirstOrDefault(x => x.ID == id);
        }
        public Car_Brand getBrandID(string brand)
        {
            return context.Cars_Brands.FirstOrDefault(x => x.Brand_Name == brand);
        }
        public Car_Model getModelID(string model)
        {
            return context.Car_Models.FirstOrDefault(x => x.Model_Name == model);
        }

        //Add Car By User
        public int Insert(AddCar car)
        {

            Car_Brand _Brand = getBrandID(car.Car_Brand_Name);
            Car_Model _Model = getModelID(car.Car_Model_Name);
            Car carModel = new Car();
            carModel.ID = car.Id;
            carModel.Price = car.Price;
            carModel.Mileage = car.Mileage;
            carModel.Made_Year = car.Made_Year;
            carModel.Engine_Capacity = car.Engine_Capacity;
            carModel.Car_Address = car.Car_Address;
            carModel.Description = car.Description;
            carModel.User_Id = car.UserId;
            carModel.Transmission = car.Transmission;
            carModel.Image = car.Poster;
            carModel.Car_Brand_Id = _Brand.Id;
            carModel.Car_Model_Id = _Model.Id;
             
            context.Add(carModel);
            context.SaveChanges();
            return carModel.ID;
        }
        //Add Car By Admin
        public int InsertByAdmin(AddCar car)
        {
            Car_Brand _Brand = getBrandID(car.Car_Brand_Name);
            Car_Model _Model = getModelID(car.Car_Model_Name);
            Car carModel = new Car();
            carModel.ID = car.Id;
            carModel.Price = car.Price;
            carModel.Mileage = car.Mileage;
            carModel.Made_Year = car.Made_Year;
            carModel.Engine_Capacity = car.Engine_Capacity;
            carModel.Car_Address = car.Car_Address;
            carModel.Description = car.Description;
            carModel.User_Id = car.UserId;
            carModel.Transmission = car.Transmission;
            carModel.Image = car.Poster;
            carModel.Car_Brand_Id = _Brand.Id;
            carModel.Car_Model_Id = _Model.Id;
            carModel.Is_Used = false;
            carModel.Is_Approved = true;
            context.Add(carModel);
            context.SaveChanges();
            return carModel.ID;
        }
        public void Delete(int id)
        {
            Car car = GetById(id);
            context.Remove(car);
            context.SaveChanges();
        }

        public void Edit(int id, CarWithBrandAndModelDataDto car)
        {
            Car updated_car = GetById(id);

             updated_car.Price = car.Price;
             updated_car.Mileage = car.Mileage;
             updated_car.Made_Year = car.Made_Year;
             updated_car.Engine_Capacity = car.Engine_Capacity;
             updated_car.Transmission = car.Transmission;
             updated_car.Car_Address = car.Car_Address;
             updated_car.Description = car.Description;
             updated_car.Car_Brand.Brand_Name=car.Car_Brand_Name;
             updated_car.Car_Model.Model_Name = car.Car_Model_Name;
            
            context.SaveChanges();
        }
        public void userCarEdit(int id,userCarEdirDto car)
        {
            Car updated_car = GetById(id);
            updated_car.Price = car.price;
            updated_car.Description=car.Description;
            updated_car.Is_Available = car.is_Available;
            updated_car.Car_Address = car.address;
            context.SaveChanges();
        }
        public void approveUserCar(int id)
        {
            Car ApprovedCar =GetById(id);
            ApprovedCar.Is_Approved = true;
            context.SaveChanges();
        }

        public List<Car> GetUsedCars()
        {
            return GetAllforUserTosell().Where(e => e.Is_Used == true).ToList();
        }

        public List<Car> GetNewCars()
        {
            return GetAllforUserTosell().Where(e => e.Is_Used == false).ToList();
        }

        public List<Car> GetCarsByBrand(string brand)
        {

            return GetAllforUserTosell(). Where(x=> x.Car_Brand.Brand_Name == brand).ToList();
        }

        public List<Car> GetCarsByModel(string model)
        {
            return GetAllforUserTosell().Where(x=> x.Car_Model.Model_Name == model).ToList();
        }

        public List<Car> GetCarsByMadeYear(int year)
        {
            return GetAllforUserTosell().Where(x => x.Made_Year == year).ToList();

        }

        public List<Car> GetCarsByTransmission(string type)
        {
            return GetAllforUserTosell().Where(x => x.Transmission == type).ToList();
        }

        public List<CarWithBrandAndModelDataDto> customizedCars(List<Car> cars)
        {
            List<CarWithBrandAndModelDataDto> customizeCars = new List<CarWithBrandAndModelDataDto>();
            for (int i = 0; i < cars.Count; i++)
            {
                CarWithBrandAndModelDataDto c1 = new CarWithBrandAndModelDataDto();
                c1.Id = cars[i].ID;
                c1.Poster = cars[i].Image;
                c1.Price = cars[i].Price;
                c1.Mileage = cars[i].Mileage;
                c1.Made_Year = cars[i].Made_Year;
                c1.Engine_Capacity = cars[i].Engine_Capacity;
                c1.Transmission = cars[i].Transmission;
                c1.Car_Address = cars[i].Car_Address;
                c1.Description = cars[i].Description;
                c1.Car_Brand_Name = cars[i].Car_Brand.Brand_Name;
                c1.Car_Model_Name = cars[i].Car_Model.Model_Name;
                c1.UserId = cars[i].User_Id;
                if (cars[i].Is_Used == true)
                {
                    c1.Status = "Used";   
                }
                else
                {
                    c1.Status = "New";
                }
                if(cars[i].Is_Available == true)
                {
                    c1.Availability = "Available";
                }
                else
                {
                    c1.Availability = "Sold";
                }
                customizeCars.Add(c1);
            }

            return customizeCars;
        }

        public List<Users> getAllUsers()
        {
            return context.Users.ToList();
        }
       


      
    }
}

