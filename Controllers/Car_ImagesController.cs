using Grade_Project_.DTO;
using Grade_Project_.Models;
using Grade_Project_.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grade_Project_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Car_ImagesController : ControllerBase
    {
        ICar_Images car_ImagesRepository;
        public Car_ImagesController(ICar_Images car_ImagesRepository)
        {
            this.car_ImagesRepository = car_ImagesRepository;
        }

        [HttpGet]
        public IActionResult Get_Images()
        {
            return Ok(car_ImagesRepository.GetAll());
        }
        [HttpGet("/id/{id:int}",Name ="FindbyId")]
        public IActionResult GetById(int id)
        {
            return Ok(car_ImagesRepository.GetById(id));
        }
        [HttpPost]
        public IActionResult Add(Car_ImagesDTO Images)
        {
            int[] imagesIDS = new int[Images.Car_Images.Length];
            if (ModelState.IsValid)
            {
                if (Images == null)
                    return BadRequest();
                for (int i = 0; i < Images.Car_Images.Length; i++)
                {
                    Car_Images carImage = new Car_Images();
                    carImage.Car_Id = Images.Car_ID;
                    carImage.Car_Image = Images.Car_Images[i];
                    car_ImagesRepository.Insert(carImage);
                    imagesIDS[i] = carImage.ID;         ///Created(Url.Link("FindbyId", new { id = carImage.ID }), carImage);






                }
                return Ok(imagesIDS);




            }
            return BadRequest("Data Not Valid");

        }
        [HttpPut]
        public IActionResult Update(int id , Car_Images _Images)
        {
            if (ModelState.IsValid) {
                car_ImagesRepository.Edit(id, _Images);
            return StatusCode(StatusCodes.Status204NoContent, "Data saved");
            }
            return BadRequest("Data Not Valid");

        }
        [HttpDelete]
        public IActionResult Remove_Image(int id)
        {
            car_ImagesRepository.Delete(id);
            return Ok(StatusCodes.Status200OK);

        }
        [HttpGet("ImagesById")]
       
        public IActionResult GetImagesByCarId(int carId)
        {
            if(carId==null)
            {
                return Ok("CarId is Empty");
            }
            List<string> carImages = car_ImagesRepository.GetImagesByCarId(carId);
            if (carImages == null)
            {
                return Ok("Car_Images is Empty ");

            }
            return Ok(carImages);
        }
    }
}
