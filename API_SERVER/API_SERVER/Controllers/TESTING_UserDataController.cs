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

namespace API_SERVER.Controllers
{
    [Route("/")]
    [ApiController]
    public class TESTING_UserDataController : ControllerBase
    {
        private UsersDbContext usersDbContext { get; set; }
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

        [HttpGet]
        public async Task<string> Get()
        {
            return UserData.FirstOrDefault().UserID.ToString();
        }
    }
}
