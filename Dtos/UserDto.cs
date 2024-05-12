namespace Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string TaxIdentifier { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserType { get; set; }
}
