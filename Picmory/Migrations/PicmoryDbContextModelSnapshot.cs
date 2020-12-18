﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Picmory.Models;

namespace Picmory.Migrations
{
    [DbContext(typeof(PicmoryDbContext))]
    partial class PicmoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Picmory.Models.Folder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Access")
                        .HasColumnType("int");

                    b.Property<string>("FolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FolderOwner")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FolderOwner");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("Picmory.Models.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Access")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FolderData")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PictureOwner")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("UploadDate")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("UploadDate");

                    b.HasKey("Id");

                    b.HasIndex("FolderData");

                    b.HasIndex("PictureOwner");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("Picmory.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ColorOne")
                        .HasColumnType("int");

                    b.Property<int>("ColorTwo")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProfPicture")
                        .HasColumnType("int");

                    b.Property<byte[]>("RegistrationTime")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("RegistrationDate");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProfPicture");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Picmory.Models.Folder", b =>
                {
                    b.HasOne("Picmory.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("FolderOwner");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Picmory.Models.Picture", b =>
                {
                    b.HasOne("Picmory.Models.Folder", "Folder")
                        .WithMany()
                        .HasForeignKey("FolderData")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Picmory.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("PictureOwner")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Folder");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Picmory.Models.User", b =>
                {
                    b.HasOne("Picmory.Models.Picture", "ProfilePicture")
                        .WithMany()
                        .HasForeignKey("ProfPicture");

                    b.Navigation("ProfilePicture");
                });
#pragma warning restore 612, 618
        }
    }
}
