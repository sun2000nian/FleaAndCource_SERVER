using API_SERVER.Data;
using API_SERVER.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Services
{
    public class UpdateService
    {
        private AppUpdateContext _context { get; set; }
        private DbSet<AppUpdateInfoModel> updateInfoDb
        {
            get
            {
                return _context.updateInfo;
            }
        }

        UpdateService(AppUpdateContext updateContext)
        {
            this._context = updateContext;
        }

        public string downloadPage()
        {
            var updateResponse = updateInfoDb.OrderByDescending(i => i.versionCode).FirstOrDefault();
            string page = "http://ip2.shiningball.cn:5000/download?filename=" + updateResponse.url;
            //string page = "<a href=\"http://ip2.shiningball.cn:5000/download?filename=" + updateResponse.url + "\"> 下载 </a>";

            return page;
        }
    }
}
