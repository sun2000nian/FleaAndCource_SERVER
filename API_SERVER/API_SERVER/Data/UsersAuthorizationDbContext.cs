using API_SERVER.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Data
{
    public class UsersAuthorizationDbContext:DbContext
    {
        public UsersAuthorizationDbContext(DbContextOptions<UsersAuthorizationDbContext> options)
            : base(options)
        {

        }

        public DbSet<UserAuthorizationData> UserAuthorizationData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAuthorizationData>().ToTable("UserAuthorizationData");
            base.OnModelCreating(modelBuilder);
        }
    }
}
