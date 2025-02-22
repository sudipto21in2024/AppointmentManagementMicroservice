using CommonBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection",
                    x => x.MigrationsAssembly("UserManagementService")); // Ensure migration files go to the main project
            }
        }

        // Add DbSet properties for all your entities:
        public DbSet<User> Users { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        //public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Model configurations (Fluent API)
            // Example: Configure relationships, indexes, etc.

            // Configure relationships
            builder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a service if appointments exist

            builder.Entity<Appointment>()
                .HasOne(a => a.Customer)
                .WithMany()
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a customer if appointments exist

            builder.Entity<Provider>()
                .HasOne(p => p.User)
                .WithOne(u => u.Provider)
                .HasForeignKey<Provider>(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a user if a provider exists

            builder.Entity<Service>()
               .HasOne(s => s.Category)
               .WithMany()
               .HasForeignKey(s => s.CategoryId);

            builder.Entity<Service>()
        .Property(s => s.Price)
        .HasPrecision(18, 2); // Same effect as "decimal(18,2)"

            // Indexes
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(); // Ensure email uniqueness

            builder.Entity<Category>()
                .HasIndex(u => u.Name) // Ensure category name uniqueness
                .IsUnique();

            //builder.Entity<Appointment>()
            //   .HasOne(a => a.Service)
            //   .WithMany()
            //   .HasForeignKey(a => a.ServiceId);

            //builder.Entity<Appointment>()
            //    .HasOne(a => a.Customer)
            //    .WithMany()
            //    .HasForeignKey(a => a.CustomerId);

        }
    }
}
