using API_SERVER.Models;
using API_SERVER.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Controllers
{
    [Route("/")]
    [ApiController]
    public class PersonalInfoController : Controller
    {
        private AccountService service { get; set; }

        public PersonalInfoController(AccountService accountService)
        {
            service = accountService;
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


        //TODO 代课单——发布
        public async Task<IActionResult> ReleaseCource(string submitData)
        {
            return Ok();
        }

        //TODO 代课单——收藏
        public async Task<IActionResult> LikeCource(string submitData)
        {
            return Ok();
        }

        //TODO 代课单——接收
        public async Task<IActionResult> ReceiveCource(string submitData)
        {
            return Ok();
        }

        //TODO 二手物品——发布
        public async Task<IActionResult> ReleaseFleaOBJ(string submitData)
        {
            return Ok();
        }

        //TODO 二手物品——收藏
        public async Task<IActionResult> LikeFleaOBJ(string submitData)
        {
            return Ok();
        }

        //TODO 二手物品——购买
        public async Task<IActionResult> ReceiveFleaOBJ(string submitData)
        {
            return Ok();
        }
    }
}
