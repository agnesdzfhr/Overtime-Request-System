using System;
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
        public DbSet<EmployeeOvertimeSchedule> EmployeeOvertimeSchedules { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Overtime> Overtimes { get; set; }
        public DbSet<OvertimeBonus> OvertimeBonuses { get; set; }
        public DbSet<OvertimeSchedule> OvertimesSchedules { get; set; }


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
            modelBuilder.Entity<EmployeeOvertimeSchedule>()
                .HasOne(eo => eo.Employee)
                .WithMany(e => e.EmployeeOvertimeSchedules);

            //One to Many
            modelBuilder.Entity<EmployeeOvertimeSchedule>()
                .HasOne(eo => eo.OvertimeSchedule)
                .WithMany(os => os.EmployeeOvertimeSchedules);

            //One to Many
            modelBuilder.Entity<OvertimeSchedule>()
                .HasOne(os => os.Overtime)
                .WithMany(o => o.OvertimeSchedules);

            //One to Many
            modelBuilder.Entity<OvertimeSchedule>()
                .HasOne(os => os.OvertimeBonus)
                .WithMany(ob => ob.OvertimeSchedules);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
