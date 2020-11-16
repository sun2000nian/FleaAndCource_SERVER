using API_SERVER.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Services
{
    public class PersonalInfoService
    {
        private UserDataContext UserDataContext { get; set; }

        public PersonalInfoService(DbContextOptions<UserDataContext> userDataOptions)
        {
            UserDataContext = new UserDataContext(userDataOptions);
        }
    }
}
