using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;
using MimeTypes;
using System.Security.Cryptography;

namespace STORAGE_SERVER.Controllers
{
    [ApiController]
    [Route("/")]
    public class FileIOController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return Ok();
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
                string ContentType;
                string ExtensionName;
                new FileExtensionContentTypeProvider().TryGetContentType(filename, out ContentType);
                if (ContentType == null) ExtensionName = filename.Substring(filename.IndexOf("."));
                else ExtensionName = MimeTypeMap.GetExtension(ContentType);
                MD5 md5 = MD5.Create();
                var str = DateTime.UtcNow.ToString();
                var byteArray = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(str));
                var newfilename = BitConverter.ToString(byteArray).Replace("-", "") + ExtensionName;

                byte[] buffer = System.IO.File.ReadAllBytes(filePath);
                return File(buffer, contentType, newfilename);
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
