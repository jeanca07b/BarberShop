using Microsoft.EntityFrameworkCore;
using BarberShop.Domain.Entities;

namespace BarberShop.Infrastructure.Persistence
{
    public class BarberShopDbContext : DbContext
    {
        public BarberShopDbContext(DbContextOptions<BarberShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Business> Businesses { get; set; }

        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Service> Services { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<BarberSchedule> BarberSchedules { get; set; }
        public DbSet<BarberBreak> BarberBreaks { get; set; }

        public DbSet<BusinessBarber> BusinessBarbers { get; set; }

        public DbSet<BusinessRequest> BusinessRequests { get; set; }

        public DbSet<User> Users { get; set; }

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

                builder.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.OwnsOne(c => c.PhoneNumber, phone =>
                {
                    phone.Property(p => p.Value)
                         .HasColumnName("PhoneNumber")
                         .IsRequired()
                         .HasMaxLength(20);
                });

            });

            modelBuilder.Entity<Business>(builder =>
            {
                builder.OwnsOne(b => b.Address, address =>
                {
                    address.Property(a => a.Street).HasColumnName("Street").IsRequired();
                    address.Property(a => a.City).HasColumnName("City").IsRequired();
                    address.Property(a => a.State).HasColumnName("State").IsRequired();
                    address.Property(a => a.Country).HasColumnName("Country").IsRequired();
                    address.Property(a => a.Latitude).HasColumnName("Latitude").IsRequired();
                    address.Property(a => a.Longitude).HasColumnName("Longitude").IsRequired();
                    address.Property(a => a.PlaceId).HasColumnName("PlaceId");
                });

                builder.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(b => b.OwnerUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Barber>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(b => b.FullName)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(b => b.Phone)
                      .HasMaxLength(20);

                entity.Property(b => b.Description)
                      .HasMaxLength(500);
            });




            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.HasOne<Business>()
                      .WithMany()
                      .HasForeignKey(s => s.BusinessId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(s => s.Price)
                    .HasColumnType("decimal(10,2)");

                entity.Property(s => s.Description)
                    .HasMaxLength(500);

                entity.Property(s => s.DurationInMinutes)
                    .IsRequired();
            });


            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Date)
                      .IsRequired();

                entity.Property(a => a.StartTime)
                      .IsRequired();

                entity.Property(a => a.EndTime)
                      .IsRequired();

                entity.Property(a => a.Status)
                      .IsRequired()
                      .HasConversion<int>();

                entity.Property(a => a.ReservationFee)
                        .HasColumnType("decimal(10,2)");

                entity.Property(a => a.CreatedAt)
                      .IsRequired();

                entity.HasOne(a => a.Barber)
                      .WithMany()
                      .HasForeignKey(a => a.BarberId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Customer)
                      .WithMany()
                      .HasForeignKey(a => a.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Service)
                      .WithMany()
                      .HasForeignKey(a => a.ServiceId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BusinessBarber>(entity =>
            {
                entity.HasKey(bb => bb.Id);

                entity.HasIndex(bb => new { bb.BusinessId, bb.BarberId })
                      .IsUnique();

                entity.HasOne(bb => bb.Barber)
                      .WithMany(b => b.BusinessBarbers)
                      .HasForeignKey(bb => bb.BarberId);

                entity.HasOne(bb => bb.Business)
                      .WithMany(b => b.BusinessBarbers)
                      .HasForeignKey(bb => bb.BusinessId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(200);
                
                entity.HasIndex(u => u.Email)
                    .IsUnique();

                entity.Property(u => u.PasswordHash)
                      .IsRequired();

                entity.Property(u => u.Role)
                      .IsRequired()
                      .HasConversion<int>();

                entity.Property(u => u.IsActive)
                      .IsRequired();
            });

        }


    }
}
