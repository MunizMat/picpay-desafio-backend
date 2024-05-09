using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class TransferModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    [Required]
    public Guid PayerId { get; set; }

    [Required]
    public Guid PayeeId { get; set; }

    [ForeignKey("PayerId")]
    public UserModel? Payer { get; set; }

    [ForeignKey("PayeeId")]
    public UserModel? Payee { get; set; }

    public DateTime CreatedAt { get; set; }

    public TransferModel(decimal amount, Guid payerId, Guid payeeId)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        PayerId = payerId;
        PayeeId = payeeId;
        CreatedAt = DateTime.UtcNow;
    }
}
