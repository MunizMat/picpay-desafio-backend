namespace Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Cpf { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserType { get; set; }
}
