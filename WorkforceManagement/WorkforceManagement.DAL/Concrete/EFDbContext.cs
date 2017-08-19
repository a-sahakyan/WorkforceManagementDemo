using Microsoft.EntityFrameworkCore;
using WorkforceManagement.Domain.Entities;

namespace WorkforceManagement.DAL.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<AuthData> AuthDatas { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<AuthData>().ToTable("AuthDatas");
            modelBuilder.Entity<Skill>().ToTable("Skills");
        }
    }
}
