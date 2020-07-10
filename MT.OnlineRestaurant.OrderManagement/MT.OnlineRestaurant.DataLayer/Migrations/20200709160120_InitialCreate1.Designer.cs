﻿// <auto-generated />
using System;
using MT.OnlineRestaurant.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MT.OnlineRestaurant.DataLayer.Migrations
{
    [DbContext(typeof(OrderManagementContext))]
    [Migration("20200709160120_InitialCreate1")]
    partial class InitialCreate1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.Context.LoggingInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActionName")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(250);

                    b.Property<string>("ControllerName")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(250);

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')");

                    b.Property<DateTime?>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("('')");

                    b.HasKey("Id");

                    b.ToTable("LoggingInfo");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.Context.TblFoodOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')");

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("TblCustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblCustomerID")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("TblOrderStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblOrderStatusID")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("TblPaymentTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblPaymentTypeID")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("TblRestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblRestaurantID")
                        .HasDefaultValueSql("((0))");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("UserCreated");

                    b.Property<int>("UserModified");

                    b.HasKey("Id");

                    b.HasIndex("TblOrderStatusId");

                    b.HasIndex("TblPaymentTypeId");

                    b.ToTable("tblFoodOrder");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.Context.TblFoodOrderMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<bool>("IsItemOutOfStock");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Quantity");

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("TblFoodOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblFoodOrderID")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("TblMenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblMenuID")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserCreated");

                    b.Property<int>("UserModified");

                    b.HasKey("Id");

                    b.HasIndex("TblFoodOrderId");

                    b.ToTable("tblFoodOrderMapping");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.Context.TblOrderPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('')");

                    b.Property<int>("TblCustomerId")
                        .HasColumnName("tblCustomerID");

                    b.Property<int>("TblFoodOrderId")
                        .HasColumnName("tblFoodOrderID");

                    b.Property<int>("TblPaymentStatusId")
                        .HasColumnName("tblPaymentStatusID");

                    b.Property<int>("TblPaymentTypeId")
                        .HasColumnName("tblPaymentTypeID");

                    b.Property<string>("TransactionId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TransactionID")
                        .HasDefaultValueSql("('0000000000')")
                        .HasMaxLength(20);

                    b.Property<int>("UserCreated");

                    b.Property<int>("UserModified");

                    b.HasKey("Id");

                    b.HasIndex("TblFoodOrderId");

                    b.HasIndex("TblPaymentStatusId");

                    b.HasIndex("TblPaymentTypeId");

                    b.ToTable("tblOrderPayment");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.Context.TblOrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("((0))")
                        .HasMaxLength(50);

                    b.Property<int>("UserCreated");

                    b.Property<int>("UserModified");

                    b.HasKey("Id");

                    b.ToTable("tblOrderStatus");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.Context.TblPaymentStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("((0))")
                        .HasMaxLength(50);

                    b.Property<int>("UserCreated");

                    b.Property<int>("UserModified");

                    b.HasKey("Id");

                    b.ToTable("tblPaymentStatus");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.Context.TblPaymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("((0))")
                        .HasMaxLength(50);

                    b.Property<int>("UserCreated");

                    b.Property<int>("UserModified");

                    b.HasKey("Id");

                    b.ToTable("tblPaymentType");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.Context.TblTableOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FromDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("TblCustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblCustomerID")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("TblOrderStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblOrderStatusID")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("TblPaymentTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblPaymentTypeID")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("TblRestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblRestaurantID")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("ToDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserCreated");

                    b.Property<int>("UserModified");

                    b.HasKey("Id");

                    b.HasIndex("TblOrderStatusId");

                    b.HasIndex("TblPaymentTypeId");

                    b.ToTable("tblTableOrder");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.Context.TblTableOrderMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("RecordTimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime>("RecordTimeStampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("TableNo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("TblTableOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tblTableOrderID")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("UserCreated");

                    b.Property<int>("UserModified");

                    b.HasKey("Id");

                    b.HasIndex("TblTableOrderId");

                    b.ToTable("tblTableOrderMapping");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.Context.TblFoodOrder", b =>
                {
                    b.HasOne("MT.OnlineRestaurant.DataLayer.Context.TblOrderStatus", "TblOrderStatus")
                        .WithMany("TblFoodOrder")
                        .HasForeignKey("TblOrderStatusId")
                        .HasConstraintName("FK_tblFoodOrder_tblOrderStatusID");

                    b.HasOne("MT.OnlineRestaurant.DataLayer.Context.TblPaymentType", "TblPaymentType")
                        .WithMany("TblFoodOrder")
                        .HasForeignKey("TblPaymentTypeId")
                        .HasConstraintName("FK_tblFoodOrder_tblPaymentTypeID");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.Context.TblFoodOrderMapping", b =>
                {
                    b.HasOne("MT.OnlineRestaurant.DataLayer.Context.TblFoodOrder", "TblFoodOrder")
                        .WithMany("TblFoodOrderMapping")
                        .HasForeignKey("TblFoodOrderId")
                        .HasConstraintName("FK_tblFoodOrderMapping_tblFoodOrderID");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.Context.TblOrderPayment", b =>
                {
                    b.HasOne("MT.OnlineRestaurant.DataLayer.Context.TblFoodOrder", "TblFoodOrder")
                        .WithMany("TblOrderPayment")
                        .HasForeignKey("TblFoodOrderId")
                        .HasConstraintName("FK_tblOrderPayment_tblFoodOrderID");

                    b.HasOne("MT.OnlineRestaurant.DataLayer.Context.TblPaymentStatus", "TblPaymentStatus")
                        .WithMany("TblOrderPayment")
                        .HasForeignKey("TblPaymentStatusId")
                        .HasConstraintName("FK_tblOrderPayment_tblPaymentStatusID");

                    b.HasOne("MT.OnlineRestaurant.DataLayer.Context.TblPaymentType", "TblPaymentType")
                        .WithMany("TblOrderPayment")
                        .HasForeignKey("TblPaymentTypeId")
                        .HasConstraintName("FK_tblOrderPayment_tblPaymentType");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.Context.TblTableOrder", b =>
                {
                    b.HasOne("MT.OnlineRestaurant.DataLayer.Context.TblOrderStatus", "TblOrderStatus")
                        .WithMany("TblTableOrder")
                        .HasForeignKey("TblOrderStatusId")
                        .HasConstraintName("FK_tblTableOrder_tblOrderStatusID");

                    b.HasOne("MT.OnlineRestaurant.DataLayer.Context.TblPaymentType", "TblPaymentType")
                        .WithMany("TblTableOrder")
                        .HasForeignKey("TblPaymentTypeId")
                        .HasConstraintName("FK_tblTableOrder_tblPaymentTypeID");
                });

            modelBuilder.Entity("MT.OnlineRestaurant.DataLayer.Context.TblTableOrderMapping", b =>
                {
                    b.HasOne("MT.OnlineRestaurant.DataLayer.Context.TblTableOrder", "TblTableOrder")
                        .WithMany("TblTableOrderMapping")
                        .HasForeignKey("TblTableOrderId")
                        .HasConstraintName("FK_tblTableOrderMapping_tblTableOrderID");
                });
#pragma warning restore 612, 618
        }
    }
}
