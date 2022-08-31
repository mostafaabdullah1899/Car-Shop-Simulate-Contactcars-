using Grade_Project_.DTO;
using Grade_Project_.Models;

namespace Grade_Project_.Repository
{
    public interface ICar_Brand
    {
        List<CarBrand_DTO> GetAll();
        Car_Brand GetById(int id);
        void Insert(CarBrand_DTO car);
        void Edit(int id, CarBrand_DTO car);
        void Delete(int id);
    }
}
