using API_SERVER.Models.Datas;
using API_SERVER.Models.Datas.CourseData;
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
        public DbSet<CourseModel> courseObjectsDb { get; set; }
        public DbSet<FleaObjectModel> fleaObjectsDb { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalData>();
            modelBuilder.Entity<CourseModel>(entity =>
            {
                entity.HasOne(p => p.sponsorData)
                .WithMany(p => p.courseObjects_Launched)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(p => p.sponsor);

                entity.HasOne(p => p.receiverData)
                .WithMany(p => p.courseObjects_Received)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(p => p.receiver);

                entity.HasMany(p => p.likedUserID)
                .WithMany(p => p.courseObjects_Liked);
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
