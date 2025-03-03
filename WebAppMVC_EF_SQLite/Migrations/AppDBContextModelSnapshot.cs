﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAppMVC_EF_SQLite.Data;

#nullable disable

namespace WebAppMVC_EF_SQLite.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.12");

            modelBuilder.Entity("WebAppMVC_EF_SQLite.Models.Costumer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Costumers");
                });

            modelBuilder.Entity("WebAppMVC_EF_SQLite.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CostumerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("OrderFulfilled")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("OrderPlaced")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("CostumerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebAppMVC_EF_SQLite.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("WebAppMVC_EF_SQLite.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(6, 2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebAppMVC_EF_SQLite.Models.Order", b =>
                {
                    b.HasOne("WebAppMVC_EF_SQLite.Models.Costumer", "Costumer")
                        .WithMany("Orders")
                        .HasForeignKey("CostumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Costumer");
                });

            modelBuilder.Entity("WebAppMVC_EF_SQLite.Models.OrderDetail", b =>
                {
                    b.HasOne("WebAppMVC_EF_SQLite.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAppMVC_EF_SQLite.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebAppMVC_EF_SQLite.Models.Costumer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("WebAppMVC_EF_SQLite.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
