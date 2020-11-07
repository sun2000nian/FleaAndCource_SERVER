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

namespace API_SERVER.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService service { get; set; }
        public LoginController(LoginService loginService)
        {
            service = loginService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(
            [FromQuery] string submitData)
        {
            var result = service.LoginCheck(submitData);
            if (result.Item1 == true)
            {
                return Ok(result.Item2);
            }
            else
            {
                return Unauthorized(result.Item2);
            }
        }
    }
}
