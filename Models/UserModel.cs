using System.ComponentModel.DataAnnotations;
using Enums;

namespace Models;

public class UserModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Cpf { get; set; }

    [Required]
    [StringLength(100)]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public UserType UserType { get; set; }

    public UserModel(string cpf, string fullName, string email, string password, UserType userType)
    {
        Id = Guid.NewGuid();
        Cpf = cpf;
        FullName = fullName;
        Email = email;
        Password = BCrypt.Net.BCrypt.HashPassword(password);
        CreatedAt = DateTime.UtcNow;
        UserType = userType;
    }

}
