﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(ShipperContext))]
    partial class ShipperContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.ShipperInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("ShipperInfos");
                });

            modelBuilder.Entity("Domain.ShipperOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ShipperId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ShipperInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ShipperOrderDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ShipperInfoId");

                    b.ToTable("ShipperOrders");
                });

            modelBuilder.Entity("Domain.ShipperOrder", b =>
                {
                    b.HasOne("Domain.ShipperInfo", "ShipperInfo")
                        .WithMany("ShipperOrders")
                        .HasForeignKey("ShipperInfoId");

                    b.Navigation("ShipperInfo");
                });

            modelBuilder.Entity("Domain.ShipperInfo", b =>
                {
                    b.Navigation("ShipperOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
