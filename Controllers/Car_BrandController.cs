using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Grade_Project_.Models;
using Grade_Project_.Repository;
using Grade_Project_.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Grade_Project_.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class Car_BrandController : ControllerBase
    {


        ICar_Brand _CarBrandRepository;

        public Car_BrandController(ICar_Brand CarBrandRepository)
        {
            _CarBrandRepository = CarBrandRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_CarBrandRepository.GetAll());
        }


        [HttpGet("{id:int}", Name = "FindByEmpolyee")]
        public IActionResult GetById(int id)
        {
            Car_Brand car = _CarBrandRepository.GetById(id);
            CarBrand_DTO brand_DTO = new CarBrand_DTO();
            brand_DTO.Id = car.Id;
            brand_DTO.BrandName = car.Brand_Name;
            brand_DTO.BrandLogo = car.Brand_Logo;
            return Ok(brand_DTO);

        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Insert([FromBody] CarBrand_DTO value)
        {
            if (ModelState.IsValid)
            {
                //Car_Brand car_Brand = new Car_Brand();
                //car_Brand.Brand_Logo = value.BrandLogo;
                //car_Brand.Brand_Name = value.BrandName;
                
                _CarBrandRepository.Insert(value);

                //  return StatusCode(StatusCodes.Status204NoContent, "Data saved");
                return Ok(value);

            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id}")]
        public IActionResult Edit([FromBody] CarBrand_DTO value, int id)
        {
            if (ModelState.IsValid)
            {
                _CarBrandRepository.Edit(id, value);
                return StatusCode(StatusCodes.Status204NoContent, "Data saved");
            }
            return BadRequest("Data Not Valid");
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _CarBrandRepository.Delete(id);
            return Ok(StatusCodes.Status200OK);
        }


    }
}
