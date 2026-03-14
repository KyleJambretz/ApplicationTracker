using ApplicationTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationTracker.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JobApplication>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.CompanyName)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.JobTitle)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.JobUrl)
                  .HasMaxLength(500);

            // Store enum as its string name so the SQLite file is
            // human-readable and survives reordering enum members.
            entity.Property(e => e.Status)
                  .HasConversion<string>();
        });
    }
}
