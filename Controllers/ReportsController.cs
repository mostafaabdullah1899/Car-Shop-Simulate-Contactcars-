using Grade_Project_.DTO;
using Grade_Project_.Models;
using Grade_Project_.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grade_Project_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReports reports;

        public ReportsController(IReports reports )
        {
            this.reports = reports;
        }

        [HttpGet("Get_All")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAll_Reports()
        {
            List<Report_DTO> AllReport = reports.GetAllReport();
            return Ok(AllReport);
        }

        [HttpGet("GetById")]
        public IActionResult GetByID(int id)
        {
            Reports report =reports.GetById(id);
            Report_DTO _DTO = new Report_DTO();
            _DTO.Id = report.Id;
            _DTO.Report = report.Report;
            return Ok(_DTO);
        }
        [HttpPost]
        public IActionResult MakeReport(PostreportDTO report)
        {
            if (ModelState.IsValid)
            {

                reports.Insert(report);
                return Ok(report);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Reports report = reports.GetById(id);
            if (report != null)
            {
                reports.Delete(id);
                return StatusCode(StatusCodes.Status204NoContent, "Remove Done");

            }
            return BadRequest("Data Not Valid ");
        }
    }
}
