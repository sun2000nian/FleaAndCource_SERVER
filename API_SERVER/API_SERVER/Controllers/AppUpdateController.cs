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

namespace API_SERVER.Controllers
{

    [Route("/appupdate")]
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
            var updateResponse = updateInfoDb.OrderBy(i => i.versionCode).FirstOrDefault();
            if (updateResponse == null) return NoContent();
            return Ok(JsonSerializer.Serialize(updateResponse));
        }

        [HttpPost]
        public async Task<IActionResult> submitUpdate(
            [FromForm] string versionName,
            [FromForm] string content,
            [FromForm] IFormFile apkfile)
        {
            AppUpdateInfoModel newUpdate = new AppUpdateInfoModel
            {
                versionName = versionName,
                content = content,
                url = apkfile.FileName
            };

            var stream = apkfile.OpenReadStream();
            MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
            multipartFormDataContent.Add(new StreamContent(stream), "file", "file");
            HttpResponseMessage response = await _httpClient.PostAsync("http://ip2.shiningball.cn:5000/upload?filename=" + apkfile.FileName, multipartFormDataContent);

            updateInfoDb.Add(newUpdate);
            _context.SaveChanges();
            return Ok();
        }
    }
}
