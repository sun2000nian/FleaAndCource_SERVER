using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_SERVER.Data;
using API_SERVER.Models;
using API_SERVER.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Abstractions;

namespace API_SERVER.Controllers
{
    [Route("/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private AccountService service { get; set; }
        public LoginController(AccountService accountService)
        {
            service = accountService;
        }

        [HttpGet("login")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(
            [FromQuery] string submitData)
        {
            var result = service.LoginCheck(submitData);
            if (result.Item1)
            {
                return Ok(result.Item2);
            }
            else
            {
                return Unauthorized(result.Item2);
            }
        }

        [HttpGet("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status303SeeOther)]
        public async Task<IActionResult> Register(
            [FromQuery] string submitData)
        {
            var result = service.Register(submitData);
            switch (result)
            {
                case (int)Values.RegisterCode.Success: return Ok();
                case (int)Values.RegisterCode.UserExist: return Unauthorized(result.ToString());
                default: return NotFound(result.ToString());
            }
        }

        //TODO:(Controller)密码更改
        [HttpPost]
        public async Task<IActionResult> ChangePassword()
        {
            return Ok();
        }

        //TODO:(Controller) 更新头像
        [HttpPost("img")]
        [RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue, ValueLengthLimit = int.MaxValue, MemoryBufferThreshold = int.MaxValue)]
        public async Task<IActionResult> UpdateAvatar(
            [FromQuery] string submitData,
            [FromForm] IFormFile file)
        {
            string fdbk = submitData;
            fdbk += '\n' + file.Name;
            fdbk += '\n' + file.FileName;
            fdbk += '\n' + file.ContentType;
            return Ok(fdbk);
        }

        //TODO (Controller)检查用户是否存在
        [HttpGet("usrchk")]
        public async Task<IActionResult> UserExistanceCheck()
        {
            return Ok();
        }
    }
}
