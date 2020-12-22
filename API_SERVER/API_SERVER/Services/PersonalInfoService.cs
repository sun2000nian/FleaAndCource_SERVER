using API_SERVER.Data;
using API_SERVER.Models.Datas;
using API_SERVER.Models.Datas.CourseData;
using API_SERVER.Models.Datas.FleaData;
using API_SERVER.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_SERVER.Services
{
    public class PersonalInfoService
    {
        private UserDataContext dataContext { get; set; }

        public PersonalInfoService(DbContextOptions<UserDataContext> userDataOptions)
        {
            dataContext = new UserDataContext(userDataOptions);
        }

        private DbSet<PersonalData> UserDataDb { get { return dataContext.UserDataDb; } }
        private DbSet<CourseModel> courseObjectsDb { get { return dataContext.courseObjectsDb; } }
        private DbSet<FleaObjectModel> fleaObjectsDb { get { return dataContext.fleaObjectsDb; } }

        public async Task<List<CourseModel>> GetRandomCourse()
        {
            return courseObjectsDb.Include(p => p.likedUserID).Include(p => p.sponsorData).Include(p => p.receiverData).Where(p => p.receiver == null).OrderBy(r => r.createTime).Take(10).ToList();
        }

        //TODO 代课单——发布
        public async Task ReleaseCourse(string userID, string courseData)
        {
            try
            {
                CourseModel obj = JsonSerializer.Deserialize<CourseModel>(courseData);
                TimeSpan timeSpan = new TimeSpan(08, 00, 00);
                TimeZoneInfo timeZone = TimeZoneInfo.CreateCustomTimeZone("China",
                                                                           timeSpan,
                                                                           "China", null);
                obj.createTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
                PersonalData user = UserDataDb.Include(p => p.courseObjects_Launched).Single<PersonalData>(p => p.userID == userID);
                //obj.sponsor = user;
                user.courseObjects_Launched.Add(obj);

                //fleaObjectsDb.Add(obj);
                UserDataDb.Update(user);
                dataContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //TODO 代课单——收藏
        public async Task LikeCourse(string userID, int courseID)
        {
            try
            {
                PersonalData user = UserDataDb.Include(p => p.courseObjects_Liked).Single(p => p.userID == userID);
                if (user == null) return;
                //dataContext.Entry(user).State = EntityState.Detached;
                CourseModel course = courseObjectsDb.Single(p => p.orderID == courseID);
                if (course == null) return;
                user.courseObjects_Liked.Add(course);
                UserDataDb.Update(user);
                await dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task<List<CourseModel>> GetMyPublishedCourse(string userID)
        {
            return courseObjectsDb.Include(p => p.sponsorData).Where(p => p.sponsorData.userID == userID).ToList();
        }

        public async Task<List<CourseModel>> GetMyReceivedCourse(string userID)
        {
            return courseObjectsDb.Include(p => p.receiverData).Where(p => p.receiverData.userID == userID).ToList();
        }
        public async Task CancelCourseLiked(string userID, int courseID)
        {
            try
            {
                PersonalData user = UserDataDb.Include(p => p.courseObjects_Liked).Single(p => p.userID == userID);
                if (user == null) return;
                //dataContext.Entry(user).State = EntityState.Detached;
                CourseModel fleaObject = user.courseObjects_Liked.Single(p => p.orderID == courseID);
                if (fleaObject == null) return;
                user.courseObjects_Liked.Remove(fleaObject);
                UserDataDb.Update(user);
                await dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //TODO 代课单——接收
        public async Task ReceiveCourse(string userID, int objectID)
        {
            if (courseObjectsDb.Where(p => p.orderID == objectID).Count() != 0&& UserDataDb.Where(i => i.userID == userID).Count() != 0)
            {
                var course = courseObjectsDb.Single(i => i.orderID == objectID);
                var user = UserDataDb.Include(p=>p.courseObjects_Received).Single(i => i.userID == userID);
                dataContext.Entry(user).State = EntityState.Detached;
                user.courseObjects_Received.Add(course);

                UserDataDb.Update(user);

                await dataContext.SaveChangesAsync();
            }
        }

        //返回随机二手物品
        public async Task<List<FleaObjectModel>> GetRandomFleaOBJ()
        {
            //返回10个未完成的订单
            return fleaObjectsDb.Include(p => p.likedUserID).Include(p => p.sponsorData).Include(p => p.receiverData).Where(p => p.receiver == null).OrderBy(r => r.createTime).Take(10).ToList();
        }

        //TODO 二手物品——发布
        public async Task ReleaseFleaOBJ(string userID, string ObjData)
        {
            try
            {
                FleaObjectModel obj = JsonSerializer.Deserialize<FleaObjectModel>(ObjData);
                TimeSpan timeSpan = new TimeSpan(08, 00, 00);
                TimeZoneInfo timeZone = TimeZoneInfo.CreateCustomTimeZone("China",
                                                                           timeSpan,
                                                                           "China", null);
                obj.createTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
                PersonalData user = UserDataDb.Include(p => p.fleaObjects_Launched).Single<PersonalData>(p => p.userID == userID);
                //obj.sponsor = user;
                user.fleaObjects_Launched.Add(obj);

                //fleaObjectsDb.Add(obj);
                UserDataDb.Update(user);
                dataContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //TODO 二手物品——收藏
        public async Task LikeFleaOBJ(string userID, int objectID)
        {
            try
            {
                PersonalData user = UserDataDb.Include(p => p.fleaObjects_Liked).Single(p => p.userID == userID);
                if (user == null) return;
                FleaObjectModel fleaObject = fleaObjectsDb.Single(p => p.orderID == objectID);
                if (fleaObject == null) return;
                user.fleaObjects_Liked.Add(fleaObject);
                UserDataDb.Update(user);
                dataContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //
        public async Task CancleFleaLiked(string userID, int objectID)
        {
            try
            {
                PersonalData user = UserDataDb.Include(p => p.fleaObjects_Liked).Single(p => p.userID == userID);
                if (user == null) return;
                FleaObjectModel fleaObject = user.fleaObjects_Liked.Single(p => p.orderID == objectID);
                if (fleaObject == null) return;
                user.fleaObjects_Liked.Remove(fleaObject);
                UserDataDb.Update(user);
                await dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //TODO 二手物品——购买
        public async Task PerchaseFleaOBJ(string userID, int objectID)
        {
            try
            {
                FleaObjectModel obj = fleaObjectsDb.Single(p => p.orderID == objectID);
                PersonalData user = UserDataDb.Single(p => p.userID == userID);
                if (obj != null && user != null)
                {

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
