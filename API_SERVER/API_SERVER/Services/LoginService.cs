using API_SERVER.Data;
using API_SERVER.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace API_SERVER.Services
{
    public class LoginService
    {

        public LoginService(DbContextOptions<UsersAuthorizationDbContext> userAuthorizationOptions, DbContextOptions<UserDataContext> userDataOptions)
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

        public Tuple<bool, string> LoginCheck(string submitedData)//true为正确，false为错误
        {
            LoginSubmit loginSubmit = JsonSerializer.Deserialize<LoginSubmit>(submitedData);
            if (AuthorizationDb.Where(t => t.UserID == loginSubmit.UserID).Count() != 0)
            {
                UserAuthorizationData user = AuthorizationDb.Single<UserAuthorizationData>(t => t.UserID == loginSubmit.UserID);
                //密码正确
                if (user.Password == loginSubmit.Password)
                {
                    //若个人信息库中无此用户
                    if (UserDataDb.Where(t => t.UserID == user.UserID).Count() == 0)
                    {
                        UserData userData = new UserData();
                        userData.UserID = user.UserID;
                        UserDataDb.Add(userData);
                        UserDataContext.SaveChanges();
                    }
                    UserData personalData = UserDataDb.Single<UserData>(t => t.UserID == user.UserID);
                    string response = JsonSerializer.Serialize<UserData>(personalData);
                    return new Tuple<bool, string>(true, response);
                }
            }
            return new Tuple<bool, string>(false, "");
        }
    }
}
