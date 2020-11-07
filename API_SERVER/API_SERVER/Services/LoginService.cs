using API_SERVER.Data;
using API_SERVER.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Services
{
    public class LoginService
    {
        /*
        public LoginService(DbContextOptions<UsersAuthorizationDbContext> userAuthorizationOptions, DbContextOptions<UserDataContext> userDataOptions)
        {
            context = new UsersAuthorizationDbContext(userAuthorizationOptions);
            UserDataContext = new UserDataContext(userDataOptions);
        }
        */
        private UsersAuthorizationDbContext context { get; set; }
        private UserDataContext UserDataContext { get; set; }

        private DbSet<UserAuthorizationData> UserData
        {
            get
            {
                return context.UserAuthorizationData;
            }
        }

        private DbSet<UserData> UserDatas
        {
            get
            {
                return UserDataContext.UserDatas;
            }
        }

        public Tuple<bool,string> LoginCheck(string submitedData)
        {
            //LoginSubmit loginSubmit = JsonSerializer.Deserialize<LoginSubmit>(submitedData);
            return new Tuple<bool, string>(true, "TEST");
        }
    }
}
