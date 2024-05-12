using System.ComponentModel.DataAnnotations;

namespace Models;

public class WalletModel
{
    [Key]
    public Guid Id { get; set; }

    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; }

    [Required]
    public Guid UserId { get; set; }

    public UserModel? User { get; set; }

    public WalletModel(decimal balance, Guid userId)
    {
        Id = Guid.NewGuid();
        Balance = balance;
        CreatedAt = DateTime.UtcNow;
        UserId = userId;
    }

}
