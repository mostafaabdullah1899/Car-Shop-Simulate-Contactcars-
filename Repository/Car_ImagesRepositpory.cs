using Grade_Project_.Models;

namespace Grade_Project_.Repository
{
    public class Car_ImagesRepositpory : ICar_Images
    {
        Cars_Entity context;
        public Car_ImagesRepositpory(Cars_Entity _context)
        {
            
            context = _context;
        }

        public List<Car_Images> GetAll()
        {
            return context.Cars_Images.ToList();
        }

        public Car_Images GetById(int id)
        {
            return context.Cars_Images.FirstOrDefault(x => x.ID == id);
        }

        public void Insert(Car_Images Car_Images)
        {
            context.Cars_Images.Add(Car_Images);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Car_Images oldCar_Images = GetById(id);
            context.Cars_Images.Remove(oldCar_Images);
            context.SaveChanges();
        }

        public void Edit(int id, Car_Images Car_Images)
        {
           // List<Car_Images> car_Images = new List<car_Images>();
            Car_Images oldCar_Images = GetById(id);
            oldCar_Images.Car_Image = Car_Images.Car_Image;
            context.SaveChanges();
        }

        public List<string> GetImagesByCarId( int carId)
        {
         return context.Cars_Images.Where(e => e.Car_Id == carId).Select(e => e.Car_Image).ToList();
        }
    }
}
