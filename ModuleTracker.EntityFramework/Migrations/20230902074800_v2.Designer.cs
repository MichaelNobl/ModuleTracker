﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModuleTracker.EntityFramework;

#nullable disable

namespace ModuleTracker.EntityFramework.Migrations
{
    [DbContext(typeof(ModulesDbContext))]
    [Migration("20230902074800_v2")]
    partial class v2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("ModuleTracker.EntityFramework.DTOs.ExerciseDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ModuleId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("SheetDtoId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SheetId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SheetDtoId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("ModuleTracker.EntityFramework.DTOs.ModuleDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("ModuleTracker.EntityFramework.DTOs.SheetDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ModuleDtoId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ModuleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PdfFilePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SheetNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ModuleDtoId");

                    b.ToTable("Sheets");
                });

            modelBuilder.Entity("ModuleTracker.EntityFramework.DTOs.ExerciseDto", b =>
                {
                    b.HasOne("ModuleTracker.EntityFramework.DTOs.SheetDto", null)
                        .WithMany("Exercises")
                        .HasForeignKey("SheetDtoId");
                });

            modelBuilder.Entity("ModuleTracker.EntityFramework.DTOs.SheetDto", b =>
                {
                    b.HasOne("ModuleTracker.EntityFramework.DTOs.ModuleDto", null)
                        .WithMany("Sheets")
                        .HasForeignKey("ModuleDtoId");
                });

            modelBuilder.Entity("ModuleTracker.EntityFramework.DTOs.ModuleDto", b =>
                {
                    b.Navigation("Sheets");
                });

            modelBuilder.Entity("ModuleTracker.EntityFramework.DTOs.SheetDto", b =>
                {
                    b.Navigation("Exercises");
                });
#pragma warning restore 612, 618
        }
    }
}
