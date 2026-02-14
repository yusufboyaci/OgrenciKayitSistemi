using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Teacher> Teachers => Set<Teacher>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ── Student Configuration ──
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Ad)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(s => s.Soyad)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(s => s.TCNo)
                  .IsRequired()
                  .HasMaxLength(11)
                  .IsFixedLength();

            entity.HasIndex(s => s.TCNo)
                  .IsUnique();

            entity.HasOne(s => s.Department)
                  .WithMany(d => d.Students)
                  .HasForeignKey(s => s.DepartmentId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ── Department Configuration ──
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(d => d.Id);

            entity.Property(d => d.Ad)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.HasIndex(d => d.Ad)
                  .IsUnique();
        });

        // ── Teacher Configuration ──
        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Ad)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(t => t.Soyad)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(t => t.Unvan)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.HasOne(t => t.Department)
                  .WithMany(d => d.Teachers)
                  .HasForeignKey(t => t.DepartmentId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
