using API_SERVER.Data;
using API_SERVER.Models.Datas;
using API_SERVER.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public DbSet<PersonalData> UserDataDb { get { return dataContext.UserDataDb; } }
        public DbSet<CourceModel> courceObjectsDb { get { return dataContext.courceObjectsDb; } }
        public DbSet<FleaObjectModel> fleaObjectsDb { get { return dataContext.fleaObjectsDb; } }

        //TODO 代课单——发布
        public async Task ReleaseCource(string submitData)
        {

        }

        //TODO 代课单——收藏
        public async Task LikeCource(string submitData)
        {

        }

        //TODO 代课单——接收
        public async Task ReceiveCource(string submitData)
        {

        }

        //TODO 二手物品——发布
        public async Task ReleaseFleaOBJ(string submitData)
        {

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
