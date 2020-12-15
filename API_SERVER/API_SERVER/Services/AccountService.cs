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
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using MimeTypes;
using System.Net.Http.Headers;
using API_SERVER.Models.Submits;
using API_SERVER.Models.Users;

namespace API_SERVER.Services
{
    public class AccountService
    {
        private readonly HttpClient _httpClient;
        public AccountService(
            DbContextOptions<UsersAuthorizationDbContext> userAuthorizationOptions,
            DbContextOptions<UserDataContext> userDataOptions,
            DbContextOptions<ServerSideUserDataContext> serverSideUserDataOptions,
            IHttpClientFactory httpClient)
        {
            AuthorizationDataContext = new UsersAuthorizationDbContext(userAuthorizationOptions);
            UserDataContext = new UserDataContext(userDataOptions);
            ServerSideUserDataContext = new ServerSideUserDataContext(serverSideUserDataOptions);
            _httpClient = httpClient.CreateClient();
        }
        private UsersAuthorizationDbContext AuthorizationDataContext { get; set; }
        private UserDataContext UserDataContext { get; set; }
        private ServerSideUserDataContext ServerSideUserDataContext { get; set; }

        private DbSet<UserAuthorizationData> AuthorizationDb
        {
            get
            {
                return AuthorizationDataContext.UserAuthorizationDataDb;
            }
        }

        private DbSet<PersonalData> UserDataDb
        {
            get
            {
                return UserDataContext.UserDataDb;
            }
        }

        private DbSet<UserData_ServerSide> userData_ServerSidesDb
        {
            get
            {
                return ServerSideUserDataContext.data_ServerSides;
            }
        }

        public Tuple<bool, string> LoginCheck(string submitedData)//true为正确，false为错误
        {
            LoginSubmit loginSubmit = JsonSerializer.Deserialize<LoginSubmit>(submitedData);
            if (AuthorizationDb.Where(t => t.UserID == loginSubmit.userID).Count() != 0)
            {
                UserAuthorizationData user = AuthorizationDb.Single<UserAuthorizationData>(t => t.UserID == loginSubmit.userID);
                //密码正确
                if (user.Password == loginSubmit.password)
                {
                    //若个人信息库中无此用户
                    if (UserDataDb.Where(t => t.userID == user.UserID).Count() == 0)
                    {
                        PersonalData userData = new PersonalData();
                        userData.userID = user.UserID;
                        UserDataDb.Add(userData);
                        UserDataContext.SaveChanges();
                    }
                    PersonalData personalData = UserDataDb.Single<PersonalData>(t => t.userID == user.UserID);
                    string response = JsonSerializer.Serialize<PersonalData>(personalData);
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
                    var user = UserDataDb.Single<PersonalData>(i => i.userID == Submit.userID);
                    UserDataDb.Remove(user);
                    UserDataContext.SaveChanges();
                }
                UserAuthorizationData newuser = new UserAuthorizationData();
                newuser.UserID = Submit.userID;
                newuser.Password = Submit.password;
                AuthorizationDb.Add(newuser);
                AuthorizationDataContext.SaveChanges();
                return (int)Values.RegisterCode.Success;
            }
        }

        public async Task<Values.ChangePasswordResult> ChangePassword(string submitData)
        {
            var passwordChanging = JsonSerializer.Deserialize<ChangePasswordSubmit>(submitData);
            //var passwordChanging = submitData;
            if (passwordChanging.userID != null && passwordChanging.newPassword != null)
            {
                var user = AuthorizationDb.Single<UserAuthorizationData>(user => user.UserID == passwordChanging.userID);
                AuthorizationDataContext.Entry(user).State = EntityState.Detached;
                if (user == null) return Values.ChangePasswordResult.NoUser;
                if (user.Password == passwordChanging.oldPassword)
                {
                    UserAuthorizationData newUser = new UserAuthorizationData
                    {
                        UserID = user.UserID,
                        Password = passwordChanging.newPassword
                    };

                    AuthorizationDb.Update(newUser);
                    AuthorizationDataContext.SaveChanges();
                    return Values.ChangePasswordResult.Success;
                }
                else return Values.ChangePasswordResult.PasswordWrong;
            }
            else return Values.ChangePasswordResult.DataInvalid;
        }



        //TODO:(Service)更改信息
        public Values.UserExistance UpdateInfo(string userData)
        {
            var newUserData = JsonSerializer.Deserialize<PersonalData>(userData);
            var userFind = UserDataDb.Where(p => p.userID == newUserData.userID).Count();
            if (userFind == 0) return Values.UserExistance.NotExist;
            //UserDataContext.Entry(oldUserData).State = EntityState.Detached;
            UserDataDb.Update(newUserData);
            UserDataContext.SaveChanges();
            return Values.UserExistance.Exist;
        }

        //TODO:(Service)接受头像图像(待完善)
        public async Task<int> AvatarUpdate(string userID, IFormFile file)
        {
            if (userData_ServerSidesDb.Where(t => t.userID == userID).Count() != 0)
            {
                //用户上传过头像
                UserData_ServerSide user = userData_ServerSidesDb.Single<UserData_ServerSide>(t => t.userID == userID);
                ServerSideUserDataContext.Entry(user).State = EntityState.Detached;

                if (user.AvatarFileName != null)
                {
                    HttpResponseMessage response0 = await _httpClient.DeleteAsync("http://ip2.shiningball.cn:5000/delete?filename=" + user.AvatarFileName);
                    //_httpClient.Dispose();
                }

                //获取新的文件名
                string ContentType;
                new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out ContentType);
                var ExtensionName = MimeTypeMap.GetExtension(ContentType);
                MD5 md5 = MD5.Create();
                var str = DateTime.UtcNow.ToString() + Path.GetRandomFileName() + ExtensionName;
                var byteArray = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(str));
                var filename = BitConverter.ToString(byteArray).Replace("-", "") + ExtensionName;

                //上传文件
                user.AvatarFileName = filename;
                var stream = file.OpenReadStream();
                MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
                multipartFormDataContent.Add(new StreamContent(stream), "file", "file");
                HttpResponseMessage response = await _httpClient.PostAsync("http://ip2.shiningball.cn:5000/upload?filename=" + filename, multipartFormDataContent);

                userData_ServerSidesDb.Update(user);
                ServerSideUserDataContext.SaveChanges();

                _httpClient.Dispose();
            }
            else
            {
                //用户未上传过头像
                if (AuthorizationDb.Where(t => t.UserID == userID).Count() != 0)
                {
                    string ContentType;
                    new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out ContentType);
                    var ExtensionName = MimeTypeMap.GetExtension(ContentType);
                    MD5 md5 = MD5.Create();
                    var str = DateTime.UtcNow.ToString() + Path.GetRandomFileName() + ExtensionName;
                    var byteArray = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(str));
                    var filename = BitConverter.ToString(byteArray).Replace("-", "") + ExtensionName;

                    UserData_ServerSide user = new UserData_ServerSide
                    {
                        userID = userID,
                        AvatarFileName = filename
                    };

                    var stream = file.OpenReadStream();
                    MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
                    multipartFormDataContent.Add(new StreamContent(stream), "file", "file");
                    HttpResponseMessage response = await _httpClient.PostAsync("http://ip2.shiningball.cn:5000/upload?filename=" + filename, multipartFormDataContent);

                    userData_ServerSidesDb.Add(user);
                    ServerSideUserDataContext.SaveChanges();

                    _httpClient.Dispose();
                }
            }
            return 0;
        }

        //TODO (Service)从服务器获取头像
        public async Task<Tuple<Values.GetAvatarResult, Stream, MediaTypeHeaderValue, string>> GetAvatarAsync(string userID)
        {
            /*
            string path = "http://ip2.shiningball.cn:5000/download?filename=114DB7F14B7A0E921272713F04ADC1F7.png";
            var httpResponse = await _httpClient.GetAsync(path);
            foreach (var item in httpResponse.Content.Headers.ToArray())
            {
                Console.WriteLine(item.Key + "//" + item.Value);
            }
            return new Tuple<Values.GetAvatarResult, Stream, MediaTypeHeaderValue, string>(
                Values.GetAvatarResult.Succeed,
                await httpResponse.Content.ReadAsStreamAsync(),
                httpResponse.Content.Headers.ContentType,
                httpResponse.Content.Headers.ContentDisposition.FileName);
            */
            if (AuthorizationDb.Where<UserAuthorizationData>(t => t.UserID == userID).Count() == 0)
            {
                return new Tuple<Values.GetAvatarResult, Stream, MediaTypeHeaderValue, string>(
                Values.GetAvatarResult.UserNotExist, null, null, null);
            }
            if (userData_ServerSidesDb.Where<UserData_ServerSide>(t => t.userID == userID).Count() == 0)
            {
                string path = Values.STORAGESERVER_ADDRESS + "download?filename=default.png";
                var httpResponse = await _httpClient.GetAsync(path);

                return new Tuple<Values.GetAvatarResult, Stream, MediaTypeHeaderValue, string>(
                Values.GetAvatarResult.UsingDefault,
                await httpResponse.Content.ReadAsStreamAsync(),
                httpResponse.Content.Headers.ContentType,
                httpResponse.Content.Headers.ContentDisposition.FileName);
            }
            else
            {
                var user = userData_ServerSidesDb.Single<UserData_ServerSide>(t => t.userID == userID);
                string path = Values.STORAGESERVER_ADDRESS + "download?filename=" + user.AvatarFileName;
                var httpResponse = await _httpClient.GetAsync(path);

                return new Tuple<Values.GetAvatarResult, Stream, MediaTypeHeaderValue, string>(
                Values.GetAvatarResult.Succeed,
                await httpResponse.Content.ReadAsStreamAsync(),
                httpResponse.Content.Headers.ContentType,
                httpResponse.Content.Headers.ContentDisposition.FileName);
            }
        }

        //TODO (Service)用户存在检查
        public Values.UserExistance UserExistanceCheck(string username)
        {
            if (AuthorizationDb.Where(t => t.UserID == username).Count() != 0)
            {
                return Values.UserExistance.Exist;
            }
            return Values.UserExistance.NotExist;
        }
    }
}
