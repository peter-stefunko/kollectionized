using Microsoft.EntityFrameworkCore;
using Kollectionized.Api.Models;

namespace Kollectionized.Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(u => u.Id).HasColumnName("id");
            entity.Property(u => u.Username).HasColumnName("username");
            entity.Property(u => u.PasswordHash).HasColumnName("password_hash");
            entity.Property(u => u.CreatedAt).HasColumnName("created_at");
        });
    }

}