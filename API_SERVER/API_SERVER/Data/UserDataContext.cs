using API_SERVER.Models.Datas;
using API_SERVER.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace API_SERVER.Data
{
    public class UserDataContext : DbContext
    {
        public UserDataContext(DbContextOptions<UserDataContext> options)
            : base(options)
        {

        }

        public DbSet<PersonalData> UserDataDb { get; set; }
        public DbSet<CourceModel> courceObjectsDb { get; set; }
        public DbSet<FleaObjectModel> fleaObjectsDb { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalData>().ToTable("UserData");
            modelBuilder.Entity<CourceModel>(entity => {
                entity.HasOne(p => p.sponsorID)
                .WithMany(p => p.courceObjects);

                entity.HasOne(p => p.receiverID)
                .WithMany(p => p.courceObjects);
            });

            modelBuilder.Entity<FleaObjectModel>(entity => {
                entity.HasOne(p => p.sponsorID)
                .WithMany(p => p.fleaObjects);

                entity.HasOne(p => p.receiverID)
                .WithMany(p => p.fleaObjects);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
