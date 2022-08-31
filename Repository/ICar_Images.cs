using Grade_Project_.Models;

namespace Grade_Project_.Repository
{
    public interface ICar_Images
    {
        List<Car_Images> GetAll();
        Car_Images GetById(int id);
        void Insert(Car_Images Car_Images);
        void Edit(int id, Car_Images Car_Images);
        void Delete(int id);
        List<string> GetImagesByCarId(int carId);
    }
}
