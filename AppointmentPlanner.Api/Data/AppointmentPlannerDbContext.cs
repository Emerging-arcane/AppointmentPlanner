using AppointmentPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentPlanner.Api.Data
{
    public class AppointmentPlannerDbContext : DbContext
    {
        public AppointmentPlannerDbContext(DbContextOptions<AppointmentPlannerDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppointmentRequest> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the AppointmentRequest entity
            modelBuilder.Entity<AppointmentRequest>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CellNumber)
                    .HasMaxLength(20);

                entity.Property(e => e.AppointmentDate)
                    .IsRequired();

                entity.Property(e => e.AppointmentTime)
                    .IsRequired();
            });
        }
    }
}