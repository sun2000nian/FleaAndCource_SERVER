using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using API_SERVER.Data;
using Microsoft.EntityFrameworkCore;
using API_SERVER.Models;
using Microsoft.AspNetCore.StaticFiles;
using MimeTypes;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using API_SERVER.Models.AppUpdate;
using System.Text;
using System.Text.Encodings.Web;

namespace API_SERVER.Controllers
{

    [Route("/appupdate/")]
    [ApiController]
    public class AppUpdateController : ControllerBase
    {
        private DbSet<AppUpdateInfoModel> updateInfoDb
        {
            get
            {
                return _context.updateInfo;
            }
        }
        private AppUpdateContext _context { get; set; }
        private readonly HttpClient _httpClient;
        public AppUpdateController(AppUpdateContext updateContext, IHttpClientFactory httpClient)
        {
            _context = updateContext;
            _httpClient = httpClient.CreateClient();
        }
        [HttpGet]
        public async Task<IActionResult> getUpdate()
        {
            var updateResponse = updateInfoDb.OrderByDescending(i => i.versionCode).FirstOrDefault();
            if (updateResponse == null) return NoContent();
            return Ok(JsonSerializer.Serialize(updateResponse));
        }

        [HttpPost]
        public async Task<IActionResult> submitUpdate(
            [FromForm] string content,
            [FromForm] IFormFile metadata,
            [FromForm] IFormFile apkfile)
        {
            output_metadata metafile;
            StreamReader metareader = new StreamReader(metadata.OpenReadStream(),Encoding.UTF8);
            var metafileString = metareader.ReadToEnd();
            metafile = JsonSerializer.Deserialize<output_metadata>(metafileString);

            if (updateInfoDb.Where(p => p.versionCode >= metafile.elements[0].versionCode).Count()> 0)
            {
                var newest = updateInfoDb.OrderByDescending(i => i.versionCode).FirstOrDefault();
                return Ok("该版本或更高版本已存在，目前最高：\n"+JsonSerializer.Serialize(newest));
            }

            string filename = metafile.elements[0].versionName.Replace(".", "_") + apkfile.FileName.Substring(apkfile.FileName.IndexOf("."));
            
            //提交-最高版本信息
            AppUpdateInfoModel newUpdate = new AppUpdateInfoModel
            {
                versionCode=metafile.elements[0].versionCode,
                versionName = metafile.elements[0].versionName,
                content = content,
                url = filename
            };

            var stream = apkfile.OpenReadStream();
            MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
            multipartFormDataContent.Add(new StreamContent(stream), "file", "file");

            
            HttpResponseMessage response = await _httpClient.PostAsync("http://ip2.shiningball.cn:5000/upload?filename=" + filename, multipartFormDataContent);

            updateInfoDb.Add(newUpdate);
            _context.SaveChanges();
            
            return Ok(JsonSerializer.Serialize(newUpdate));
        }
        [HttpGet("downloadLatestVersion")]
        public async Task<IActionResult> downloadPage()
        {
            var updateResponse = updateInfoDb.OrderByDescending(i => i.versionCode).FirstOrDefault();
            //string page = "<html>\n<head>\n</head>\n<body>\n<a href=\"http://ip2.shiningball.cn:5000/download?filename=" + updateResponse.url + "\"> 下载 </a>\n</body>\n</html>";
            string page = "http://ip2.shiningball.cn:5000/download?filename=" + updateResponse.url;
            return StatusCode(200, page);
        }
    }
}
