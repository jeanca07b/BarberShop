using Microsoft.EntityFrameworkCore;
using BarberShop.Domain.Entities;
using BarberShop.Domain.ValueObjects;

namespace BarberShop.Infrastructure.Persistence
{
    public class BarberShopDbContext : DbContext
    {
        public BarberShopDbContext(DbContextOptions<BarberShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(builder =>
            {
                builder.OwnsOne(c => c.Email, email =>
                {
                    email.Property(e => e.Value)
                         .HasColumnName("Email")
                         .IsRequired();
                });
            });
        }
    }
}
