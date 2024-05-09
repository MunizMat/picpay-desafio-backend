using Enums;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Contexts;

public class PostgreSqlContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<TransferModel> Transfers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User
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

        // Transfer
        modelBuilder.Entity<TransferModel>()
            .HasOne(t => t.Payer)
            .WithMany()
            .HasForeignKey(t => t.PayerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TransferModel>()
            .HasOne(t => t.Payee)
            .WithMany()
            .HasForeignKey(t => t.PayeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
