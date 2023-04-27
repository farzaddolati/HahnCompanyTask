using Microsoft.EntityFrameworkCore;
using FarzadsTask.Domain;
namespace FarzadsTask.Data
{
    public class MyApiDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public MyApiDbContext(DbContextOptions<MyApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasKey(e => e.Id);
            modelBuilder.Entity<Customer>().Property(e => e.FirstName).IsRequired();
            modelBuilder.Entity<Customer>().Property(e => e.LastName).IsRequired();
            modelBuilder.Entity<Customer>().Property(e => e.Email).IsRequired();
        }
    }
}
