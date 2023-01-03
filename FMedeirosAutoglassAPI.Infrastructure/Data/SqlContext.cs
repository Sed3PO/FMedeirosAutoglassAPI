using FMedeirosAutoglassAPI.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FMedeirosAutoglassAPI.Infrastructure.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext()
        { }

        public SqlContext(DbContextOptions<SqlContext> options)
            : base(options) { }

        public DbSet<Product> Product { get; set; }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Id") != null))
            {
                if (entry.State == EntityState.Unchanged)
                {
                    entry.Property("IsActive").CurrentValue = false;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}