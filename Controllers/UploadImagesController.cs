using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;


namespace Grade_Project_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment hosting;
        public UploadImagesController(IWebHostEnvironment hosting)
        {
            this.hosting = hosting;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("uploadCarImage")]
        public async Task<IActionResult> uploadCarImage(string folderName)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
               
                var files = formCollection.Files.ToList();

                var pathToSave = Path.Combine(hosting.WebRootPath, "images");

                pathToSave = Path.Combine(pathToSave, folderName);


                if (files.Count > 0)
                {
                    string dbPath = "";
                    foreach (var file in files)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        dbPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        [Route("serveFile")]
        public FileContentResult serveFile(string fileName,string folderName)
        {
            FileContentResult file;
            var fullPath = Path.Combine(hosting.WebRootPath,"images");
             fullPath = Path.Combine(fullPath, folderName);
             var pathToGetFile = Path.Combine(fullPath, fileName);
            byte[] myfile;
            myfile = System.IO.File.ReadAllBytes(pathToGetFile);
            file= new FileContentResult(myfile, "image/png");
            return file;



        }

    }
}
