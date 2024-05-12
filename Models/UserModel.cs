using System.ComponentModel.DataAnnotations;
using Enums;

namespace Models;

public class UserModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string TaxIdentifier { get; set; }

    [Required]
    [StringLength(60)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(60)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public UserType UserType { get; set; }

    public decimal AccountBalance { get; set; }

    public UserModel(string taxIdentifier, string firstName, string lastName, string email, string password, UserType userType, decimal accountBalance)
    {
        Id = Guid.NewGuid();
        TaxIdentifier = taxIdentifier;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = BCrypt.Net.BCrypt.HashPassword(password);
        CreatedAt = DateTime.UtcNow;
        UserType = userType;
        AccountBalance = accountBalance;
    }

}
