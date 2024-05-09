using Enums;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Contexts;

public class PostgreSqlContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserModel> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>()
            .HasIndex(u => u.Cpf)
            .IsUnique();

        modelBuilder.Entity<UserModel>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<UserModel>()
            .Property(u => u.UserType)
            .HasDefaultValue(UserType.Common)
            .HasConversion<string>();
    }
}
