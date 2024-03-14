﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SERWalks.API.Data;

#nullable disable

namespace SERWalks.API.Migrations
{
    [DbContext(typeof(SERWalksDbContext))]
    partial class SERWalksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SERWalks.API.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7734507a-1505-46bd-8416-989cbff2fe19"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("819bbef6-a149-4390-9e99-589b9dcacd49"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("9cc663aa-3ff3-439a-a5f3-0e733272b1b7"),
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("SERWalks.API.Models.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("757ec18b-c924-408e-9456-5587dcaab8a4"),
                            Code = "SUM",
                            Name = "Sumadija",
                            RegionImageUrl = "sumadija.jpg"
                        },
                        new
                        {
                            Id = new Guid("38d42c0c-aeac-425b-a1f7-f942708ef693"),
                            Code = "VOJ",
                            Name = "Vojvodina",
                            RegionImageUrl = "vojvodina.jpg"
                        },
                        new
                        {
                            Id = new Guid("309b7bc9-339d-4855-aa85-573cef4ceb50"),
                            Code = "POM",
                            Name = "Pomoravlje",
                            RegionImageUrl = "pomoravlje.jpg"
                        },
                        new
                        {
                            Id = new Guid("c41ed9d3-5dc7-4a94-84cc-0be563500b61"),
                            Code = "BEO",
                            Name = "Beograd",
                            RegionImageUrl = "beograd.jpg"
                        });
                });

            modelBuilder.Entity("SERWalks.API.Models.Domain.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LengthInKm")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("SERWalks.API.Models.Domain.Walk", b =>
                {
                    b.HasOne("SERWalks.API.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SERWalks.API.Models.Domain.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
