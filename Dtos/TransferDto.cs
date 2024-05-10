namespace Dtos;

public class TransferDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public Guid PayerId { get; set; }
    public Guid PayeeId { get; set; }
    public DateTime CreatedAt { get; set; }
}
