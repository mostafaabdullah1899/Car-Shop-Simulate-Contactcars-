using Grade_Project_.DTO;
using Grade_Project_.Models;
using Microsoft.EntityFrameworkCore;

namespace Grade_Project_.Repository
{
    public class Car_BrandRepository : ICar_Brand
    {
        private readonly Cars_Entity _context;

        public Car_BrandRepository(Cars_Entity context)
        {
            _context = context;
        }
        public List<CarBrand_DTO> GetAll()
        {
            var b = _context.Cars_Brands.Select(e => new CarBrand_DTO()
            {
                BrandName = e.Brand_Name,
                BrandLogo=e.Brand_Logo,
                Id =e.Id
            }).ToList();
            return b;
        }

        public Car_Brand GetById(int id)
        {
            return _context.Cars_Brands.Include(x=>x.Car_Models).FirstOrDefault(e => e.Id == id);
            
        }

        public void Insert(CarBrand_DTO car_brand)
        {
            Car_Brand car = new Car_Brand();
            car.Brand_Name = car_brand.BrandName;
            car.Brand_Logo = car_brand.BrandLogo;

            _context.Cars_Brands.Add(car);
            _context.SaveChanges();
        }

        public void Edit(int id, CarBrand_DTO car_brand)
        {
            Car_Brand car  = GetById(id);
            car.Brand_Name = car_brand.BrandName;
            car.Brand_Logo = car_brand.BrandLogo;


            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Car_Brand result = GetById(id);
            _context.Cars_Brands.Remove(result);
            _context.SaveChanges();
        }




    }
}
