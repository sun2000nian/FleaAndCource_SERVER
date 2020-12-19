using API_SERVER.Models.Datas;
using API_SERVER.Models.Datas.CourceData;
using API_SERVER.Models.Datas.FleaData;
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
            modelBuilder.Entity<PersonalData>();
            modelBuilder.Entity<CourceModel>(entity =>
            {
                entity.HasOne(p => p.sponsorData)
                .WithMany(p => p.courceObjects_Launched)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(p => p.sponsor);

                entity.HasOne(p => p.receiverData)
                .WithMany(p => p.courceObjects_Received)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(p => p.receiver);

                entity.HasMany(p => p.likedUserID)
                .WithMany(p => p.courceObjects_Liked);
            });
            
            modelBuilder.Entity<FleaObjectModel>(entity =>
            {
                entity.HasOne(p => p.sponsorData)
                .WithMany(p => p.fleaObjects_Launched)
                .HasForeignKey(p => p.sponsor);

                entity.HasOne(p => p.receiverData)
                .WithMany(p => p.fleaObjects_Received)
                .HasForeignKey(p => p.receiver);

                entity.HasMany(p => p.likedUserID)
                .WithMany(p => p.fleaObjects_Liked);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
