﻿// <auto-generated />
using System;
using API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.Property<string>("AccountID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiredToken")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TokenOTP")
                        .HasColumnType("int");

                    b.HasKey("AccountID");

                    b.HasIndex("NIK")
                        .IsUnique()
                        .HasFilter("[NIK] IS NOT NULL");

                    b.ToTable("tb_m_account");
                });

            modelBuilder.Entity("API.Models.AccountRole", b =>
                {
                    b.Property<int>("AccountRoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AccountRoleID");

                    b.HasIndex("AccountID");

                    b.HasIndex("RoleID");

                    b.ToTable("tb_tr_account_role");
                });

            modelBuilder.Entity("API.Models.Department", b =>
                {
                    b.Property<string>("DepartmentID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentID");

                    b.ToTable("tb_m_department");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DepartmentID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ManagerID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OvertimeLimitID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Salary")
                        .HasColumnType("real");

                    b.Property<int>("WorkDayPerMonth")
                        .HasColumnType("int");

                    b.Property<int>("WorkHourPerDay")
                        .HasColumnType("int");

                    b.HasKey("NIK");

                    b.HasIndex("DepartmentID");

                    b.HasIndex("ManagerID");

                    b.HasIndex("OvertimeLimitID");

                    b.ToTable("tb_m_employee");
                });

            modelBuilder.Entity("API.Models.FinanceValidation", b =>
                {
                    b.Property<int>("FinanceValidationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OvertimeRequestID")
                        .HasColumnType("int");

                    b.Property<float>("TotalFee")
                        .HasColumnType("real");

                    b.HasKey("FinanceValidationID");

                    b.HasIndex("OvertimeRequestID")
                        .IsUnique();

                    b.ToTable("tb_m_finance_validation");
                });

            modelBuilder.Entity("API.Models.ManagerApproval", b =>
                {
                    b.Property<int>("ManagerApprovalID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ManagerApprovalStatus")
                        .HasColumnType("int");

                    b.Property<string>("ManagerNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OvertimeRequestID")
                        .HasColumnType("int");

                    b.HasKey("ManagerApprovalID");

                    b.HasIndex("OvertimeRequestID")
                        .IsUnique();

                    b.ToTable("tb_m_manager_approval");
                });

            modelBuilder.Entity("API.Models.OvertimeBonus", b =>
                {
                    b.Property<string>("OvertimeBonusID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("CommisionPct")
                        .HasColumnType("real");

                    b.Property<string>("Hour")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OvertimeBonusID");

                    b.ToTable("tb_m_overtime_bonus");
                });

            modelBuilder.Entity("API.Models.OvertimeLimit", b =>
                {
                    b.Property<string>("OvertimeLimitID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<TimeSpan>("MaxOvertime")
                        .HasColumnType("time");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OvertimeLimitID");

                    b.ToTable("tb_m_overtime_limit");
                });

            modelBuilder.Entity("API.Models.OvertimeRequest", b =>
                {
                    b.Property<int>("OvertimeRequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StartTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotalTime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OvertimeRequestID");

                    b.HasIndex("NIK");

                    b.ToTable("tb_m_overtime_request");
                });

            modelBuilder.Entity("API.Models.Role", b =>
                {
                    b.Property<string>("RoleID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("tb_m_role");
                });

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithOne("Account")
                        .HasForeignKey("API.Models.Account", "NIK");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("API.Models.AccountRole", b =>
                {
                    b.HasOne("API.Models.Account", "Account")
                        .WithMany("AccountRoles")
                        .HasForeignKey("AccountID");

                    b.HasOne("API.Models.Role", "Role")
                        .WithMany("AccountRoles")
                        .HasForeignKey("RoleID");

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.HasOne("API.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentID");

                    b.HasOne("API.Models.Employee", "Manager")
                        .WithMany("Employees")
                        .HasForeignKey("ManagerID");

                    b.HasOne("API.Models.OvertimeLimit", "OvertimeLimit")
                        .WithMany("Employees")
                        .HasForeignKey("OvertimeLimitID");

                    b.Navigation("Department");

                    b.Navigation("Manager");

                    b.Navigation("OvertimeLimit");
                });

            modelBuilder.Entity("API.Models.FinanceValidation", b =>
                {
                    b.HasOne("API.Models.OvertimeRequest", "OvertimeRequest")
                        .WithOne("FinanceValidation")
                        .HasForeignKey("API.Models.FinanceValidation", "OvertimeRequestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OvertimeRequest");
                });

            modelBuilder.Entity("API.Models.ManagerApproval", b =>
                {
                    b.HasOne("API.Models.OvertimeRequest", "OvertimeRequest")
                        .WithOne("ManagerApproval")
                        .HasForeignKey("API.Models.ManagerApproval", "OvertimeRequestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OvertimeRequest");
                });

            modelBuilder.Entity("API.Models.OvertimeRequest", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithMany("OvertimeSchedules")
                        .HasForeignKey("NIK");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.Navigation("AccountRoles");
                });

            modelBuilder.Entity("API.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Employees");

                    b.Navigation("OvertimeSchedules");
                });

            modelBuilder.Entity("API.Models.OvertimeLimit", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("API.Models.OvertimeRequest", b =>
                {
                    b.Navigation("FinanceValidation");

                    b.Navigation("ManagerApproval");
                });

            modelBuilder.Entity("API.Models.Role", b =>
                {
                    b.Navigation("AccountRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
