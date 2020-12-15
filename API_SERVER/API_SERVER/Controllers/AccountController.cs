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
using System.Data;

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

        [HttpGet]
        public async Task<IActionResult> checkconnection()
        {
            return Ok();
        }

        //TODO (Controller)检查用户是否存在
        [HttpPost("usrchk")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        public async Task<IActionResult> UserExistanceCheck(
            [FromForm] string userID)
        {
            switch (service.UserExistanceCheck(userID))
            {
                case Values.UserExistance.Exist:return StatusCode(StatusCodes.Status302Found);
                case Values.UserExistance.NotExist:return Ok();
                default:return NoContent();
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(
            [FromForm] string submitData)
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

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status303SeeOther)]
        public async Task<IActionResult> Register(
            [FromForm] string submitData)
        {
            var result = service.Register(submitData);
            switch (result)
            {
                case (int)Values.RegisterCode.Success: return Ok();
                case (int)Values.RegisterCode.UserExist: return Unauthorized(result.ToString());
                default: return NotFound(result.ToString());
            }
        }

        [HttpPost("changepassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangePassword(
            [FromForm] string submitData)//Json格式的ChangePasswordModel
        {
            var result = service.ChangePassword(submitData);
            switch (result.Result)
            {
                case Values.ChangePasswordResult.Success:
                    return Ok();
                case Values.ChangePasswordResult.PasswordWrong:
                    return Unauthorized();
                case Values.ChangePasswordResult.DataInvalid:
                    return Conflict();
                case Values.ChangePasswordResult.NoUser:
                    return Unauthorized();
                default: return NoContent();
            }
        }

        [HttpPost("updateInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUserData(
            [FromForm] string userData)
        {
            service.UpdateInfo(userData);
            return Ok();
        }
    }
}
