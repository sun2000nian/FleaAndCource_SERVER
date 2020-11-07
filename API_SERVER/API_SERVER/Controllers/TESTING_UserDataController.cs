using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_SERVER.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_SERVER.Controllers;
using Microsoft.EntityFrameworkCore;
using API_SERVER.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API_SERVER.Controllers
{
    [Route("/test")]
    [ApiController]
    public class TESTING_UserDataController : ControllerBase
    {
        private UsersDbContext context { get; set; }

        private DbSet<User> UserData
        {
            get
            {
                return context.UserData;
            }
        }

        public TESTING_UserDataController(DbContextOptions<UsersDbContext> options)
        {
            context = new UsersDbContext(options);
        }

        [HttpGet("{upload}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get(string upload)
        {

            return Unauthorized(JsonSerializer.Serialize(UserData.FirstOrDefault()));
        }
    }
}
