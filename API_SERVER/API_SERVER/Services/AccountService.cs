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
    public class AccountService
    {

        public AccountService(DbContextOptions<UsersAuthorizationDbContext> userAuthorizationOptions, DbContextOptions<UserDataContext> userDataOptions)
        {
            AuthorizationDataContext = new UsersAuthorizationDbContext(userAuthorizationOptions);
            UserDataContext = new UserDataContext(userDataOptions);
        }
        private UsersAuthorizationDbContext AuthorizationDataContext { get; set; }
        private UserDataContext UserDataContext { get; set; }

        private DbSet<UserAuthorizationData> AuthorizationDb
        {
            get
            {
                return AuthorizationDataContext.UserAuthorizationDataDb;
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
            if (AuthorizationDb.Where(t => t.UserID == loginSubmit.userID).Count() != 0)
            {
                UserAuthorizationData user = AuthorizationDb.Single<UserAuthorizationData>(t => t.UserID == loginSubmit.userID);
                //密码正确
                if (user.Password == loginSubmit.Password)
                {
                    //若个人信息库中无此用户
                    if (UserDataDb.Where(t => t.userID == user.UserID).Count() == 0)
                    {
                        UserData userData = new UserData();
                        userData.userID = user.UserID;
                        UserDataDb.Add(userData);
                        UserDataContext.SaveChanges();
                    }
                    UserData personalData = UserDataDb.Single<UserData>(t => t.userID == user.UserID);
                    string response = JsonSerializer.Serialize<UserData>(personalData);
                    return new Tuple<bool, string>(true, response);
                }
            }
            return new Tuple<bool, string>(false, "");
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
                    var user = UserDataDb.Single<UserData>(i => i.userID == Submit.userID);
                    UserDataDb.Remove(user);
                    UserDataContext.SaveChanges();
                }
                UserAuthorizationData newuser = new UserAuthorizationData();
                newuser.UserID = Submit.userID;
                newuser.Password = Submit.Password;
                AuthorizationDb.Add(newuser);
                AuthorizationDataContext.SaveChanges();
                return (int)Values.RegisterCode.Success;
            }
        }

        //TODO:(Service)更改密码
        public int ChangePassword(string submitData)
        {
            return 0;
        }



        //TODO:(Service)更改信息
        public int UpdateInfo()
        {
            return 0;
        }

        //TODO:(Service)接受头像图像
        //public int ReceiveImg()

        //TODO (Service)用户存在检查
        public int UserExistanceCheck(string username)
        {
            if (AuthorizationDb.Where(t => t.UserID == username).Count() != 0)
            {
                return (int)Values.UserExistance.Exist;
            }
            return (int)Values.UserExistance.NotExist;
        }
    }
}
