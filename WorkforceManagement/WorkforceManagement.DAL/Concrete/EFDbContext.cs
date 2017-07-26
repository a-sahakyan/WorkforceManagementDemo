using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorkforceManagement.Domain.Entities;

namespace WorkforceManagement.DAL.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {
        }

        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<AuthDataModel> AuthDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeModel>().ToTable("Employees");
            modelBuilder.Entity<AuthDataModel>().ToTable("AuthDatas");
        }
    }
}
