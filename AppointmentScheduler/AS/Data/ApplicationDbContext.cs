//using CommonBase.Models;
//using Microsoft.EntityFrameworkCore;

//namespace AS.Data
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<Appointment> Appointments { get; set; }

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            base.OnModelCreating(builder);

//            builder.Entity<Appointment>()
//                .HasOne(a => a.Service)
//                .WithMany()
//                .HasForeignKey(a => a.ServiceId);

//            builder.Entity<Appointment>()
//                .HasOne(a => a.Customer)
//                .WithMany()
//                .HasForeignKey(a => a.CustomerId);

//            // ... other configurations (e.g., indexes, unique constraints)
//        }
//    }
//}
