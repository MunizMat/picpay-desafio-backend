using Enums;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Contexts;

public class PostgreSqlContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<TransferModel> Transfers { get; set; }
    public DbSet<WalletModel> Wallets { get; set; }

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

        // Wallet
        modelBuilder.Entity<WalletModel>()
            .HasOne(t => t.Owner)
            .WithOne()
            .HasForeignKey<WalletModel>(t => t.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Transfer
        modelBuilder.Entity<TransferModel>()
            .HasOne(t => t.Payee)
            .WithMany()
            .HasForeignKey(t => t.PayeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
