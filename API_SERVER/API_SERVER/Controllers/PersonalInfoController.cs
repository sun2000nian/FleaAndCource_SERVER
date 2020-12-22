using API_SERVER.Models;
using API_SERVER.Models.Datas;
using API_SERVER.Models.Datas.CourseData;
using API_SERVER.Models.Datas.FleaData;
using API_SERVER.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
//using Newtonsoft.Json;

namespace API_SERVER.Controllers
{
    [Route("/")]
    [ApiController]
    public class PersonalInfoController : Controller
    {
        private AccountService service { get; set; }
        private PersonalInfoService infoService { get; set; }

        public PersonalInfoController(AccountService accountService, PersonalInfoService personalInfoService)
        {
            service = accountService;
            infoService = personalInfoService;
        }

        //TODO:(Controller) 更新头像(待测试)
        [HttpPost("uploadAvatar")]
        [RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue, ValueLengthLimit = int.MaxValue, MemoryBufferThreshold = int.MaxValue)]
        public async Task<IActionResult> UpdateAvatar(
            [FromForm] string userID,
            [FromForm] IFormFile file)
        {
            await service.AvatarUpdate(userID, file);
            return Ok();
        }

        //TODO (Controller) 下载头像
        [HttpPost("downloadAvatar")]
        public async Task<IActionResult> DownloadAvatar(
            [FromForm] string userID)
        {
            var tuple = await service.GetAvatarAsync(userID);
            if (tuple.Item1 == Values.GetAvatarResult.Succeed)
            {
                var stream = tuple.Item2;
                return File(stream, tuple.Item3.ToString(), tuple.Item4);
            }
            else if (tuple.Item1 == Values.GetAvatarResult.UsingDefault)
            {
                var stream = tuple.Item2;
                return File(stream, tuple.Item3.ToString(), tuple.Item4);
            }
            return Ok();
        }


        //随机获取代课单
        [HttpGet("getCourse")]
        public async Task<IActionResult> GetRandomCourse()
        {
            List<CourseModel> courses = await infoService.GetRandomCourse();
            return Ok(JsonSerializer.Serialize(courses));
            /*
            return Ok(JsonConvert.SerializeObject(courses,new JsonSerializerSettings { 
                MaxDepth=2,
                ReferenceLoopHandling=ReferenceLoopHandling.Ignore
            }));*/
        }

        [HttpPost("getpublishedcourse")]
        public async Task<IActionResult> GetPublishedCourse(
            [FromForm] string userID)
        {
            var courses = await infoService.GetMyPublishedCourse(userID);
            return Ok();
        }

        [HttpPost("GetReceivedCourse")]
        public async Task<IActionResult> GetReceivedCourse(
            [FromForm] string userID)
        {
            return Ok();
        }

        [HttpPost("releaseCourse")]
        //TODO 代课单——发布
        public async Task<IActionResult> ReleaseCourse(
            [FromForm] string userID,
            [FromForm] string CourseData)
        {
            await infoService.ReleaseCourse(userID, CourseData);
            return Ok();
        }

        //TODO 代课单——收藏
        [HttpPost("likeCourse")]
        public async Task<IActionResult> LikeCourse(
            [FromForm] string userID,
            [FromForm] int courseID)
        {
            await infoService.LikeCourse(userID, courseID);
            return Ok();
        }

        //TODO 代课单——接收
        [HttpPost("receiveCourse")]
        public async Task<IActionResult> ReceiveCourse(
            [FromForm] string userID,
            [FromForm] int objectID)
        {
            await infoService.ReceiveCourse(userID, objectID);
            return Ok();
        }

        //TEST 随机获取二手商品
        [HttpGet("getFleaOBJ")]
        public async Task<IActionResult> GetRandomFleaOBJ()
        {
            List<FleaObjectModel> objs = await infoService.GetRandomFleaOBJ();
            return Ok(JsonSerializer.Serialize(objs));
            /*
            return Ok(JsonConvert.SerializeObject(objs, new JsonSerializerSettings
            {
                MaxDepth = 1,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
            */
        }
        [HttpPost("releaseFleaOBJ")]
        //TEST 二手物品——发布
        public async Task<IActionResult> ReleaseFleaOBJ(
            [FromForm] string userID,
            [FromForm] string FleaObjData)//收图，收信息
        {
            await infoService.ReleaseFleaOBJ(userID, FleaObjData);
            return Ok();
        }

        //TODO 二手物品——收藏
        [HttpPost("likeFleaOBJ")]
        public async Task<IActionResult> LikeFleaOBJ(
            [FromForm] string userID,
            [FromForm] int objectID)
        {
            await infoService.LikeFleaOBJ(userID, objectID);
            return Ok();
        }

        [HttpPost("likeFleaOBJcancle")]
        public async Task<IActionResult> LikeFleaOBJ_Cancle(
            [FromForm] string userID,
            [FromForm] int objectID)
        {
            await infoService.CancleFleaLiked(userID, objectID);
            return Ok();
        }

        //TODO 二手物品——购买
        [HttpPost("perchaseFleaOBJ")]
        public async Task<IActionResult> ReceiveFleaOBJ(
            [FromForm] string userID,
            [FromForm] int objectID)
        {
            await infoService.PerchaseFleaOBJ(userID, objectID);
            return Ok();
        }
    }
}
