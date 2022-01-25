﻿// <auto-generated />
using System;
using Dmu_Console.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dmu_Console.Migrations
{
    [DbContext(typeof(CommonContext))]
    [Migration("20220121113902_status")]
    partial class status
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Dmu_Console.Data.Models.DestinationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("SourceModelId")
                        .HasColumnType("int");

                    b.Property<int>("Sum")
                        .HasMaxLength(200)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SourceModelId")
                        .IsUnique();

                    b.ToTable("Destination");
                });

            modelBuilder.Entity("Dmu_Console.Data.Models.MigrationStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("ExecutionTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("From")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("To")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MigrationStatuses");
                });

            modelBuilder.Entity("Dmu_Console.Data.Models.SourceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("FirstNumber")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<int>("LastNumber")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Source");
                });

            modelBuilder.Entity("Dmu_Console.Data.Models.DestinationModel", b =>
                {
                    b.HasOne("Dmu_Console.Data.Models.SourceModel", "Source")
                        .WithOne("Destination")
                        .HasForeignKey("Dmu_Console.Data.Models.DestinationModel", "SourceModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Source");
                });

            modelBuilder.Entity("Dmu_Console.Data.Models.SourceModel", b =>
                {
                    b.Navigation("Destination");
                });
#pragma warning restore 612, 618
        }
    }
}
