using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_SERVER.Data;
using API_SERVER.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_SERVER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private UsersDbContext context { get; set; }

        private DbSet<User> UserData
        {
            get
            {
                return context.UserData;
            }
        }

        [HttpGet]
        public async Task<string> Get(string submitData)
        {
            return "NULL";
        }
    }
}
