﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<OvertimeLimit> OvertimeLimits { get; set; }
        public DbSet<OvertimeBonus> OvertimeBonuses { get; set; }
        public DbSet<OvertimeRequest> OvertimesRequests { get; set; }
        public DbSet<ManagerApproval> ManagerApprovals { get; set; }
        public DbSet<FinanceValidation> FinanceValidations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to Many
            modelBuilder.Entity<Department>()
               .HasMany(d => d.Employees)
               .WithOne(e => e.Department);

            //One to One
            modelBuilder.Entity<Account>()
               .HasOne(a => a.Employee)
               .WithOne(e => e.Account)
               .HasForeignKey<Account>(a => a.NIK);

            //One to One
            modelBuilder.Entity<Employee>()
               .HasOne(e => e.Manager)
               .WithMany(m => m.Employees);

            //One to Many
            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Account)
                .WithMany(a => a.AccountRoles);

            //One to Many
            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Role)
                .WithMany(r => r.AccountRoles);

            //One to Many
            modelBuilder.Entity<OvertimeRequest>()
                .HasOne(os => os.Employee)
                .WithMany(e => e.OvertimeSchedules);

            //One to Many
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.OvertimeLimit)
                .WithMany(o => o.Employees);

            modelBuilder.Entity<FinanceValidation>()
                .HasOne(fv => fv.OvertimeRequest)
                .WithOne(or => or.FinanceValidation)
                .HasForeignKey<FinanceValidation>(fv=> fv.OvertimeRequestID);

            modelBuilder.Entity<ManagerApproval>()
                .HasOne(ma=>ma.OvertimeRequest)
                .WithOne(or => or.ManagerApproval)
                .HasForeignKey<ManagerApproval>(ma=>ma.OvertimeRequestID);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
