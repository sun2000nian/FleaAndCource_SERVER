using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;

namespace STORAGE_SERVER.Controllers
{
    [ApiController]
    [Route("/")]
    public class FileIOController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> create([FromQuery] string filename, [FromForm] IFormFile file)
        {
            string dataPath = Directory.GetCurrentDirectory() + "\\data";
            string filePath = dataPath + "\\" + filename;

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            return Ok();
        }

        [HttpGet("download")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> download([FromQuery] string filename)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(filename, out contentType))
            {
                contentType = "application/octet-stream";
            }

            string dataPath = Directory.GetCurrentDirectory() + "\\data\\";
            string filePath = dataPath + filename;

            if (System.IO.File.Exists(filePath))
            {
                byte[] buffer = System.IO.File.ReadAllBytes(filePath);
                return File(buffer, contentType, filename);
            }
            else
            {
                return NotFound();
            }
            //return File(buffer, "application/force-download", filename);
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> delete(string filename)
        {
            string dataPath = Directory.GetCurrentDirectory() + "\\data\\";
            string filePath = dataPath + filename;

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
