﻿// <auto-generated />
using System;
using API_SERVER.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API_SERVER.Migrations.UserData
{
    [DbContext(typeof(UserDataContext))]
    [Migration("20201214140128_UserData")]
    partial class UserData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("API_SERVER.Models.Datas.CourceModel", b =>
                {
                    b.Property<int>("orderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("closeTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("createTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("displayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isClosed")
                        .HasColumnType("bit");

                    b.Property<string>("receiveruserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("sponsoruserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("orderID");

                    b.HasIndex("receiveruserID");

                    b.HasIndex("sponsoruserID");

                    b.ToTable("courceObjectsDb");
                });

            modelBuilder.Entity("API_SERVER.Models.Datas.FleaObjectModel", b =>
                {
                    b.Property<int>("orderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("closeTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("createTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("displayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isClosed")
                        .HasColumnType("bit");

                    b.Property<string>("receiveruserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("sponsoruserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("orderID");

                    b.HasIndex("receiveruserID");

                    b.HasIndex("sponsoruserID");

                    b.ToTable("fleaObjectsDb");
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

            modelBuilder.Entity("CourceModelPersonalData", b =>
                {
                    b.Property<int>("courceObjects_LikedorderID")
                        .HasColumnType("int");

                    b.Property<string>("likedUserIDuserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("courceObjects_LikedorderID", "likedUserIDuserID");

                    b.HasIndex("likedUserIDuserID");

                    b.ToTable("CourceModelPersonalData");
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

            modelBuilder.Entity("API_SERVER.Models.Datas.CourceModel", b =>
                {
                    b.HasOne("API_SERVER.Models.Users.PersonalData", "receiver")
                        .WithMany("courceObjects_Received")
                        .HasForeignKey("receiveruserID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("API_SERVER.Models.Users.PersonalData", "sponsor")
                        .WithMany("courceObjects_Launched")
                        .HasForeignKey("sponsoruserID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("receiver");

                    b.Navigation("sponsor");
                });

            modelBuilder.Entity("API_SERVER.Models.Datas.FleaObjectModel", b =>
                {
                    b.HasOne("API_SERVER.Models.Users.PersonalData", "receiver")
                        .WithMany("fleaObjects_Received")
                        .HasForeignKey("receiveruserID");

                    b.HasOne("API_SERVER.Models.Users.PersonalData", "sponsor")
                        .WithMany("fleaObjects_Launched")
                        .HasForeignKey("sponsoruserID");

                    b.Navigation("receiver");

                    b.Navigation("sponsor");
                });

            modelBuilder.Entity("CourceModelPersonalData", b =>
                {
                    b.HasOne("API_SERVER.Models.Datas.CourceModel", null)
                        .WithMany()
                        .HasForeignKey("courceObjects_LikedorderID")
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
                    b.HasOne("API_SERVER.Models.Datas.FleaObjectModel", null)
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

            modelBuilder.Entity("API_SERVER.Models.Users.PersonalData", b =>
                {
                    b.Navigation("courceObjects_Launched");

                    b.Navigation("courceObjects_Received");

                    b.Navigation("fleaObjects_Launched");

                    b.Navigation("fleaObjects_Received");
                });
#pragma warning restore 612, 618
        }
    }
}