﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web.Data;

namespace Web.Migrations
{
    [DbContext(typeof(WashingDbContext))]
    [Migration("20210613093046_20210613_002")]
    partial class _20210613_002
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Web.Models.Clothing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Action")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiscountAmount")
                        .HasColumnType("int");

                    b.Property<bool>("IsPickup")
                        .HasColumnType("bit");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<int>("PicNo")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PickupDt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReceiveDt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Seq")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("Clothing");
                });

            modelBuilder.Entity("Web.Models.ClothingAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ClothingActions");
                });

            modelBuilder.Entity("Web.Models.ClothingColor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Seq")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ClothingColors");
                });

            modelBuilder.Entity("Web.Models.ClothingStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ClothingStatus");
                });

            modelBuilder.Entity("Web.Models.ClothingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DryCleaningPrice")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Seq")
                        .HasColumnType("int");

                    b.Property<string>("Spec")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WashingPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ClothingTypes");
                });

            modelBuilder.Entity("Web.Models.Cst", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C10")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C11")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C12")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C13")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C14")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C15")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C16")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C17")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C18")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C19")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C20")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C21")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C9")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateTime1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateTime2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShopName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UniformNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cst");
                });

            modelBuilder.Entity("Web.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Act")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<int?>("ClothingId")
                        .HasColumnType("int");

                    b.Property<string>("Employee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LogDt")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Web.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LineId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UniformNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("Web.Models.Wid", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("C1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C9")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommodityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommodityNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateTime2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateTime3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateTime4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MethodName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MethodNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffName2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffName3")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Wid");
                });

            modelBuilder.Entity("Web.Models.Clothing", b =>
                {
                    b.HasOne("Web.Models.Member", null)
                        .WithMany("Enrollments")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
