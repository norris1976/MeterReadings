// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimNorris.MeterReadings.LocalDb.Context;

#nullable disable

namespace TimNorris.MeterReadings.LocalDb.Migrations
{
    [DbContext(typeof(LocalDbContext))]
    [Migration("20220821070616_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("TimNorris.MeterReadings.Domain.Models.CustomerAccount", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.HasKey("AccountId");

                    b.ToTable("CustomerAccounts");
                });

            modelBuilder.Entity("TimNorris.MeterReadings.Domain.Models.MeterReading", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MeterReadValue")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("MeterReadingDateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MeterReadings");
                });
#pragma warning restore 612, 618
        }
    }
}
