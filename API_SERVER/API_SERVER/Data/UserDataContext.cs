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
            modelBuilder.Entity<PersonalData>();
            modelBuilder.Entity<CourceModel>(entity =>
            {
                entity.HasOne(p => p.sponsor)
                .WithMany(p => p.courceObjects_Launched)
                .OnDelete(DeleteBehavior.Restrict);
                //.HasForeignKey(p => p.sponsorID_FK);

                entity.HasOne(p => p.receiver)
                .WithMany(p => p.courceObjects_Received)
                .OnDelete(DeleteBehavior.Restrict);
                //.HasForeignKey(p => p.receiverID_FK);

                entity.HasMany(p => p.likedUserID)
                .WithMany(p => p.courceObjects_Liked);
            });
            
            modelBuilder.Entity<FleaObjectModel>(entity =>
            {
                entity.HasOne(p => p.sponsor)
                .WithMany(p => p.fleaObjects_Launched);
                //.HasForeignKey(p => p.sponsorID_FK);

                entity.HasOne(p => p.receiver)
                .WithMany(p => p.fleaObjects_Received);
                //.HasForeignKey(p => p.receiverID_FK);

                entity.HasMany(p => p.likedUserID)
                .WithMany(p => p.fleaObjects_Liked);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
