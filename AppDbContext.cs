using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PTMKTestTask.Entities;

namespace PTMKTestTask
{
    public class AppDbContext : DbContext
    {
      
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Employee>()
            //.HasIndex(e => e.FullName)
            //.IsUnique(false);
            //modelBuilder.Entity<Employee>()
            //.HasIndex(e => e.FullName)
            //.HasName("FullName_FullTextIndex");
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Employee> Employees { get; set; }

    }
}
