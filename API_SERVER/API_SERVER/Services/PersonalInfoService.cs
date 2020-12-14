using API_SERVER.Data;
using API_SERVER.Models.Datas;
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
        private DbSet<CourceModel> courceObjectsDb { get { return dataContext.courceObjectsDb; } }
        private DbSet<FleaObjectModel> fleaObjectsDb { get { return dataContext.fleaObjectsDb; } }

        public async Task<List<CourceModel>> GetRandomCource()
        {
            return courceObjectsDb.OrderBy(r => r.createTime).Take(10).ToList();
        }

        //TODO 代课单——发布
        public async Task ReleaseCource(string userID, string submitData)
        {
            try
            {
                CourceModel cource = JsonSerializer.Deserialize<CourceModel>(submitData);
                courceObjectsDb.Add(cource);
                dataContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //TODO 代课单——收藏
        public async Task LikeCource(string submitData)
        {

        }

        //TODO 代课单——接收
        public async Task ReceiveCource(string submitData)
        {

        }

        //返回随机二手物品
        public async Task<List<FleaObjectModel>> GetRandomFleaOBJ()
        {
            //返回10个未完成的订单
            return fleaObjectsDb.Include(p => p.receiver == null).OrderBy(r => r.createTime).Take(10).ToList();
        }

        //TODO 二手物品——发布
        public async Task ReleaseFleaOBJ(string userID, string ObjData)
        {
            try
            {
                FleaObjectModel obj = JsonSerializer.Deserialize<FleaObjectModel>(ObjData);
                obj.createTime = DateTime.Now;
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
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //
        public async Task CancleLiked(string userID,int objectID)
        {
            try
            {
                PersonalData user = UserDataDb.Include(p => p.fleaObjects_Liked).Single(p => p.userID == userID);
                if (user == null) return;
                FleaObjectModel fleaObject = user.fleaObjects_Liked.Single(p => p.orderID == objectID);
                if (fleaObject == null) return;
                user.fleaObjects_Liked.Remove(fleaObject);
                UserDataDb.Update(user);
                dataContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //TODO 二手物品——购买
        public async Task ReceiveFleaOBJ(string userID, int objectID)
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
