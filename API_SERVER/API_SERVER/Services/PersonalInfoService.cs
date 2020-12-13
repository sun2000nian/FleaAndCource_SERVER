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
            return courceObjectsDb.OrderBy(r => r.creaTime).Take(10).ToList();
        }

        //TODO 代课单——发布
        public async Task ReleaseCource(string userID,string submitData)
        {
            try
            {
                CourceModel cource = JsonSerializer.Deserialize<CourceModel>(submitData);
                courceObjectsDb.Add(cource);
                dataContext.SaveChanges();
            }
            catch(Exception e)
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

        public async Task<List<FleaObjectModel>> GetRandomFleaOBJ()
        {
            return fleaObjectsDb.OrderBy(r => r.creaTime).Take(10).ToList();
        }

        //TODO 二手物品——发布
        public async Task ReleaseFleaOBJ(string userID,string ObjData)
        {
            try
            {
                FleaObjectModel obj = JsonSerializer.Deserialize<FleaObjectModel>(ObjData);
                obj.creaTime = DateTime.Now;
                PersonalData user = UserDataDb.Include(p=>p.fleaObjects_Launched).Single<PersonalData>(p => p.userID == userID);
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
        public async Task LikeFleaOBJ(string submitData)
        {

        }

        //TODO 二手物品——购买
        public async Task ReceiveFleaOBJ(string submitData)
        {

        }
    }
}
