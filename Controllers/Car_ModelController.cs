using System.ComponentModel.DataAnnotations;
using System.Linq;
using Grade_Project_.DTO;
using Grade_Project_.Models;
using Grade_Project_.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grade_Project_.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class Car_ModelController : ControllerBase
    {

        Car_ModelRepository Car_ModelRepository;
        public Car_ModelController()
        {


            Car_ModelRepository = new Car_ModelRepository();



        }
        [HttpGet]
        //[Authorize(Roles = "admin")]
        public IActionResult GetAllModel()
        {
            List<ModelWithBrandDTO> C = (List<ModelWithBrandDTO>)Car_ModelRepository.GetAll();


         
            return Ok(C);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            ModelWithBrandDTO modeles = Car_ModelRepository.GetById(id);
            if (modeles == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(modeles);
        }

        [HttpGet]
        [Route("{name}")]
        public IActionResult GetByName(string name)
        {
            ModelWithBrandDTO m = Car_ModelRepository.GetByName(name);
            if (m == null)
            {
                return BadRequest("Not Found");
            }

            return Ok(m);

        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Add(ModelWithBrandDTO modeles)
        {

            if (ModelState.IsValid)
            {

               
                Car_ModelRepository.Insert(modeles);
                return Ok(modeles);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id:int}")]

        public IActionResult Update([FromRoute] int id, [FromBody] Car_Model NewModel)
        {
            if (ModelState.IsValid)
            {
                Car_ModelRepository.Edit(id, NewModel);
                return StatusCode(StatusCodes.Status204NoContent, "Data saved");
            }
            return BadRequest(ModelState);

        }
        [HttpDelete("{id:int}")]

        public IActionResult Removebyid(int id)
        {

            Car_ModelRepository.Delete(id);
            return StatusCode(StatusCodes.Status204NoContent, "Data Deleted");



        }

        [HttpGet]
        [Route("GetByBrandId")]
        public IActionResult getByBrnadId(int brandId)
        {
            if (ModelState == null)
                return BadRequest();
            ModelWithBrand[] modelsBrand = Car_ModelRepository.getByBrandId(brandId);
            return Ok(modelsBrand);



        }
    }
}