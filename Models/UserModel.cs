namespace Models;

public class UserModel
{
    public required string Id { get; set; }
    public required string Cpf { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required DateTime CreatedAt { get; set; }

}
