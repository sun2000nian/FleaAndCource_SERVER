using API_SERVER.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Data
{
    public class AppUpdateContext : DbContext
    {
        public DbSet<AppUpdateInfoModel> updateInfo { get; set; }
        public AppUpdateContext(DbContextOptions<AppUpdateContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUpdateInfoModel>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
