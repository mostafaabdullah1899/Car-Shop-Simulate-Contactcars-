using Grade_Project_.DTO;
using Grade_Project_.Models;

namespace Grade_Project_.Repository
{
    public interface ICar
    {
        Car_Brand getBrandID(string brand);
        Car_Model getModelID(string model);
        List<Car> GetAllforUser(int id);
        List<Car> GetAllforUserTosell();
        List<Car> GetAllforAdmin();
        List<string> GetImagesById(int id);
        Car GetById(int id);
        int Insert(AddCar car);
        int InsertByAdmin(AddCar car);
        void Edit(int id, CarWithBrandAndModelDataDto car);
        void userCarEdit(int id, userCarEdirDto car);

        void approveUserCar(int id);
        void Delete(int id);
        List<Car> GetUsedCars();
        List<Car> GetNewCars();
        List<Car> GetCarsByBrand(string brand);
        List<Car> GetCarsByModel(string model);
        List<Car> GetCarsByMadeYear(int year);
        List<Car> GetCarsByTransmission(string type);
        List<CarWithBrandAndModelDataDto> customizedCars(List<Car> cars);
        

    }
}
