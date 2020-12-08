using API_SERVER.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Data
{
    public class UserDataContext:DbContext
    {
        public UserDataContext(DbContextOptions<UserDataContext> options)
            :base(options)
        {

        }

        public DbSet<PersonalData> UserDataDb { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalData>().ToTable("UserData");
            base.OnModelCreating(modelBuilder);
        }
    }
}
