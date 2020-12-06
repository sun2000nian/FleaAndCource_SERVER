using API_SERVER.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Data
{
    public class ServerSideUserDataContext : DbContext
    {
        public ServerSideUserDataContext(DbContextOptions<ServerSideUserDataContext> options)
             : base(options)
        {

        }

        public DbSet<UserData_ServerSide> data_ServerSides { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData_ServerSide>().ToTable("ServerSide_UserData");
            base.OnModelCreating(modelBuilder);
        }
    }
}
