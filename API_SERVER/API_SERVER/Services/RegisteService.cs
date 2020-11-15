using API_SERVER.Data;
using API_SERVER.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_SERVER.Services
{
    public class RegisteService
    {
        public RegisteService(DbContextOptions<UsersAuthorizationDbContext> userAuthorizationOptions, DbContextOptions<UserDataContext> userDataOptions)
        {
            AuthorizationData = new UsersAuthorizationDbContext(userAuthorizationOptions);
            UserDataContext = new UserDataContext(userDataOptions);
        }

        private UsersAuthorizationDbContext AuthorizationData { get; set; }
        private UserDataContext UserDataContext { get; set; }

        private DbSet<UserAuthorizationData> AuthorizationDb
        {
            get
            {
                return AuthorizationData.UserAuthorizationDataDb;
            }
        }

        private DbSet<UserData> UserDataDb
        {
            get
            {
                return UserDataContext.UserDataDb;
            }
        }

        public int Register(string submitedData)
        {
            LoginSubmit Submit = JsonSerializer.Deserialize<LoginSubmit>(submitedData);
            if (AuthorizationDb.Where(t => t.UserID == Submit.userID).Count() != 0)
            {
                return (int)Values.RegisterCode.UserExist;
            }
            else
            {
                //若个人信息库中有此用户
                if (UserDataDb.Where(t => t.userID == Submit.userID).Count() != 0)
                {
                    var user = UserDataDb.Where(t => t.userID == Submit.userID);
                    UserDataDb.Remove((UserData)user);
                }
                UserAuthorizationData newuser = new UserAuthorizationData();
                newuser.UserID = Submit.userID;
                newuser.Password = Submit.Password;
                AuthorizationDb.Add(newuser);
                return (int)Values.RegisterCode.Success;
            }
        }
    }
}
