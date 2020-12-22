﻿// <auto-generated />
using System;
using API_SERVER.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API_SERVER.Migrations.UserData
{
    [DbContext(typeof(UserDataContext))]
    partial class UserDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("API_SERVER.Models.Datas.CourseData.CourseModel", b =>
                {
                    b.Property<int>("orderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("closeTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("createTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("displayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("isClosed")
                        .HasColumnType("bit");

                    b.Property<string>("receiver")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("sponsor")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("orderID");

                    b.HasIndex("receiver");

                    b.HasIndex("sponsor");

                    b.ToTable("courseObjectsDb");
                });

            modelBuilder.Entity("API_SERVER.Models.Datas.FleaData.FleaObjectModel", b =>
                {
                    b.Property<int>("orderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("closeTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("createTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("displayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isClosed")
                        .HasColumnType("bit");

                    b.Property<string>("receiver")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("sponsor")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("orderID");

                    b.HasIndex("receiver");

                    b.HasIndex("sponsor");

                    b.ToTable("fleaObjectsDb");
                });

            modelBuilder.Entity("API_SERVER.Models.Datas.FleaData.Picture", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("FleaObjectModelorderID")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("FleaObjectModelorderID");

                    b.ToTable("Picture");
                });

            modelBuilder.Entity("API_SERVER.Models.Users.PersonalData", b =>
                {
                    b.Property<string>("userID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("displayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("gender")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("qq")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("studentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userID");

                    b.ToTable("UserDataDb");
                });

            modelBuilder.Entity("CourseModelPersonalData", b =>
                {
                    b.Property<int>("courseObjects_LikedorderID")
                        .HasColumnType("int");

                    b.Property<string>("likedUserIDuserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("courseObjects_LikedorderID", "likedUserIDuserID");

                    b.HasIndex("likedUserIDuserID");

                    b.ToTable("CourseModelPersonalData");
                });

            modelBuilder.Entity("FleaObjectModelPersonalData", b =>
                {
                    b.Property<int>("fleaObjects_LikedorderID")
                        .HasColumnType("int");

                    b.Property<string>("likedUserIDuserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("fleaObjects_LikedorderID", "likedUserIDuserID");

                    b.HasIndex("likedUserIDuserID");

                    b.ToTable("FleaObjectModelPersonalData");
                });

            modelBuilder.Entity("API_SERVER.Models.Datas.CourseData.CourseModel", b =>
                {
                    b.HasOne("API_SERVER.Models.Users.PersonalData", "receiverData")
                        .WithMany("courseObjects_Received")
                        .HasForeignKey("receiver")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("API_SERVER.Models.Users.PersonalData", "sponsorData")
                        .WithMany("courseObjects_Launched")
                        .HasForeignKey("sponsor")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("receiverData");

                    b.Navigation("sponsorData");
                });

            modelBuilder.Entity("API_SERVER.Models.Datas.FleaData.FleaObjectModel", b =>
                {
                    b.HasOne("API_SERVER.Models.Users.PersonalData", "receiverData")
                        .WithMany("fleaObjects_Received")
                        .HasForeignKey("receiver");

                    b.HasOne("API_SERVER.Models.Users.PersonalData", "sponsorData")
                        .WithMany("fleaObjects_Launched")
                        .HasForeignKey("sponsor");

                    b.Navigation("receiverData");

                    b.Navigation("sponsorData");
                });

            modelBuilder.Entity("API_SERVER.Models.Datas.FleaData.Picture", b =>
                {
                    b.HasOne("API_SERVER.Models.Datas.FleaData.FleaObjectModel", null)
                        .WithMany("pictures")
                        .HasForeignKey("FleaObjectModelorderID");
                });

            modelBuilder.Entity("CourseModelPersonalData", b =>
                {
                    b.HasOne("API_SERVER.Models.Datas.CourseData.CourseModel", null)
                        .WithMany()
                        .HasForeignKey("courseObjects_LikedorderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_SERVER.Models.Users.PersonalData", null)
                        .WithMany()
                        .HasForeignKey("likedUserIDuserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FleaObjectModelPersonalData", b =>
                {
                    b.HasOne("API_SERVER.Models.Datas.FleaData.FleaObjectModel", null)
                        .WithMany()
                        .HasForeignKey("fleaObjects_LikedorderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_SERVER.Models.Users.PersonalData", null)
                        .WithMany()
                        .HasForeignKey("likedUserIDuserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API_SERVER.Models.Datas.FleaData.FleaObjectModel", b =>
                {
                    b.Navigation("pictures");
                });

            modelBuilder.Entity("API_SERVER.Models.Users.PersonalData", b =>
                {
                    b.Navigation("courseObjects_Launched");

                    b.Navigation("courseObjects_Received");

                    b.Navigation("fleaObjects_Launched");

                    b.Navigation("fleaObjects_Received");
                });
#pragma warning restore 612, 618
        }
    }
}
