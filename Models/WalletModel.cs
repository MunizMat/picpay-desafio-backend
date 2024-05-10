using System.ComponentModel.DataAnnotations;

namespace Models;

public class WalletModel
{
    [Key]
    public Guid Id { get; set; }

    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [Required]
    public Guid OwnerId { get; set; }

    public UserModel? Owner { get; set; }

    public WalletModel(decimal balance, Guid ownerId)
    {
        Id = Guid.NewGuid();
        Balance = balance;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        OwnerId = ownerId;
    }

}
